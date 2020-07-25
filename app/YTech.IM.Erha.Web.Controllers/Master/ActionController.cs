using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using SharpArch.Core;
using SharpArch.Web.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.RepositoryInterfaces;
using YTech.IM.GSP.Data.Repository;
using YTech.IM.GSP.Enums;
using YTech.IM.GSP.Web.Controllers.ViewModel;

namespace YTech.IM.GSP.Web.Controllers.Master
{
    [HandleError]
    public class ActionController : Controller
    {
        private readonly IMActionCatRepository _mActionCatRepository;
        private readonly IMActionRepository _mActionRepository;
        public ActionController(IMActionCatRepository mActionCatRepository, IMActionRepository mActionRepository)
        {
            Check.Require(mActionCatRepository != null, "mActionCatRepository may not be null");
            Check.Require(mActionRepository != null, "mActionRepository may not be null");

            this._mActionCatRepository = mActionCatRepository;
            this._mActionRepository = mActionRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [Transaction]
        public virtual ActionResult List(string sidx, string sord, int page, int rows)
        {
            int totalRecords = 0;
            var actions = _mActionRepository.GetPagedActionList(sidx, sord, page, rows, ref totalRecords);
            int pageSize = rows;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from action in actions
                    select new
                    {
                        i = action.Id,
                        cell = new string[] {
                            string.Empty,
                            action.Id, 
                            action.ActionName,
                            action.ActionCatId != null ? action.ActionCatId.Id : null, 
                            action.ActionCatId != null ? action.ActionCatId.ActionCatName : null, 
                            action.ActionStatus,
                            action.ActionPrice.HasValue ? action.ActionPrice.Value.ToString(Helper.CommonHelper.NumberFormat):null,
                            action.ActionComponentTool.HasValue ? action.ActionComponentTool.Value.ToString(Helper.CommonHelper.NumberFormat):null,
                            action.ActionComponentMedician.HasValue ? action.ActionComponentMedician.Value.ToString(Helper.CommonHelper.NumberFormat):null,
                            action.ActionComponentDoctor.HasValue ? action.ActionComponentDoctor.Value.ToString(Helper.CommonHelper.NumberFormat):null,
                            action.ActionComponentTherapist.HasValue ? action.ActionComponentTherapist.Value.ToString(Helper.CommonHelper.NumberFormat):null, 
                          action.ActionDesc
                        }
                    }).ToArray()
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [Transaction]
        public virtual ActionResult ListSearch(string sidx, string sord, int page, int rows)
        {
            int totalRecords = 0;
            var actions = _mActionRepository.GetPagedActionListByStatus(sidx, sord, page, rows, ref totalRecords, "Aktif");
            int pageSize = rows;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from action in actions
                    select new
                    {
                        i = action.Id,
                        cell = new string[] {
                            action.Id, 
                            action.ActionName,
                            action.ActionCatId != null ? action.ActionCatId.Id : null, 
                            action.ActionCatId != null ? action.ActionCatId.ActionCatName : null, 
                            action.ActionPrice.HasValue ? action.ActionPrice.Value.ToString(Helper.CommonHelper.NumberFormat):null,
                          action.ActionDesc
                        }
                    }).ToArray()
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [Transaction]
        public ActionResult Insert(MAction viewModel, FormCollection formCollection)
        {
            UpdateNumericData(viewModel, formCollection);

            MAction mActionToInsert = new MAction();
            TransferFormValuesTo(mActionToInsert, viewModel, formCollection);
            mActionToInsert.SetAssignedIdTo(viewModel.Id);
            mActionToInsert.CreatedDate = DateTime.Now;
            mActionToInsert.CreatedBy = User.Identity.Name;
            mActionToInsert.DataStatus = EnumDataStatus.New.ToString();
            _mActionRepository.Save(mActionToInsert);

            try
            {
                _mActionRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mActionRepository.DbContext.RollbackTransaction();

                //throw e.GetBaseException();
                return Content(e.GetBaseException().Message);
            }

            return Content("success");
        }

        [Transaction]
        public ActionResult Delete(MAction viewModel, FormCollection formCollection)
        {
            MAction mActionToDelete = _mActionRepository.Get(viewModel.Id);

            if (mActionToDelete != null)
            {
                _mActionRepository.Delete(mActionToDelete);
            }

            try
            {
                _mActionRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mActionRepository.DbContext.RollbackTransaction();

                return Content(e.GetBaseException().Message);
            }

            return Content("success");
        }

        [Transaction]
        public ActionResult Update(MAction viewModel, FormCollection formCollection)
        {
            UpdateNumericData(viewModel, formCollection);
            MAction mActionToUpdate = _mActionRepository.Get(viewModel.Id);
            TransferFormValuesTo(mActionToUpdate, viewModel, formCollection);
            mActionToUpdate.ModifiedDate = DateTime.Now;
            mActionToUpdate.ModifiedBy = User.Identity.Name;
            mActionToUpdate.DataStatus = EnumDataStatus.Updated.ToString();
            _mActionRepository.Update(mActionToUpdate);

            try
            {
                _mActionRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mActionRepository.DbContext.RollbackTransaction();

                return Content(e.GetBaseException().Message);
            }

            return Content("success");
        }

        private void TransferFormValuesTo(MAction mActionToUpdate, MAction mActionFromForm, FormCollection formCollection)
        {
            mActionToUpdate.ActionName = mActionFromForm.ActionName;
            mActionToUpdate.ActionDesc = mActionFromForm.ActionDesc;
            mActionToUpdate.ActionPrice = mActionFromForm.ActionPrice;
            mActionToUpdate.ActionStatus = mActionFromForm.ActionStatus;
            mActionToUpdate.ActionComponentTool = mActionFromForm.ActionComponentTool;
            mActionToUpdate.ActionComponentMedician = mActionFromForm.ActionComponentMedician;
            mActionToUpdate.ActionComponentDoctor = mActionFromForm.ActionComponentDoctor;
            mActionToUpdate.ActionComponentTherapist = mActionFromForm.ActionComponentTherapist;

            mActionToUpdate.ActionCatId = _mActionCatRepository.Get(formCollection["ActionCatId"]);
        }

        private void UpdateNumericData(MAction viewModel, FormCollection formCollection)
        {
            if (!string.IsNullOrEmpty(formCollection["ActionPrice"]))
            {
                string ActionPrice = formCollection["ActionPrice"].Replace(",", "");
                decimal? dec = Convert.ToDecimal(ActionPrice);
                viewModel.ActionPrice = dec;
            }
            else
            {
                viewModel.ActionPrice = null;
            }

            if (!string.IsNullOrEmpty(formCollection["ActionComponentTool"]))
            {
                string ActionComponentTool = formCollection["ActionComponentTool"].Replace(",", "");
                decimal? dec = Convert.ToDecimal(ActionComponentTool);
                viewModel.ActionComponentTool = dec;
            }
            else
            {
                viewModel.ActionComponentTool = null;
            }

            if (!string.IsNullOrEmpty(formCollection["ActionComponentMedician"]))
            {
                string ActionComponentMedician = formCollection["ActionComponentMedician"].Replace(",", "");
                decimal? dec = Convert.ToDecimal(ActionComponentMedician);
                viewModel.ActionComponentMedician = dec;
            }
            else
            {
                viewModel.ActionComponentMedician = null;
            }

            if (!string.IsNullOrEmpty(formCollection["ActionComponentDoctor"]))
            {
                string ActionComponentDoctor = formCollection["ActionComponentDoctor"].Replace(",", "");
                decimal? dec = Convert.ToDecimal(ActionComponentDoctor);
                viewModel.ActionComponentDoctor = dec;
            }
            else
            {
                viewModel.ActionComponentDoctor = null;
            }

            if (!string.IsNullOrEmpty(formCollection["ActionComponentTherapist"]))
            {
                string ActionComponentTherapist = formCollection["ActionComponentTherapist"].Replace(",", "");
                decimal? dec = Convert.ToDecimal(ActionComponentTherapist);
                viewModel.ActionComponentTherapist = dec;
            }
            else
            {
                viewModel.ActionComponentTherapist = null;
            }
        }

        [Transaction]
        public virtual ActionResult GetList()
        {
            var actions = _mActionRepository.GetAll();
            StringBuilder sb = new StringBuilder();
            MAction act = new MAction();
            sb.AppendFormat("{0}:{1}", string.Empty, "-Pilih Tindakan-");
            for (int i = 0; i < actions.Count; i++)
            {
                act = actions[i];
                sb.AppendFormat(";{0}:{1}", act.Id, act.ActionName);
            }
            return Content(sb.ToString());
        }
    }
}
