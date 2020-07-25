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
    public class ActionCatController : Controller
    { 
        private readonly IMActionCatRepository _mActionCatRepository;
        public ActionCatController(IMActionCatRepository mActionCatRepository)
        {
            Check.Require(mActionCatRepository != null, "mActionCatRepository may not be null");

            this._mActionCatRepository = mActionCatRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Transaction]
        public virtual ActionResult List(string sidx, string sord, int page, int rows)
        {
            int totalRecords = 0;
            var actionCats = _mActionCatRepository.GetPagedActionCatList(sidx, sord, page, rows, ref totalRecords);
            int pageSize = rows;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from actionCat in actionCats
                    select new
                    {
                        i = actionCat.Id,
                        cell = new string[] {
                            actionCat.Id, 
                            actionCat.ActionCatName, 
                          actionCat.ActionCatDesc
                        }
                    }).ToArray()
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [Transaction]
        public ActionResult Insert(MActionCat viewModel, FormCollection formCollection)
        {

            MActionCat mItemCatToInsert = new MActionCat();
            TransferFormValuesTo(mItemCatToInsert, viewModel);
            mItemCatToInsert.SetAssignedIdTo(viewModel.Id);
            mItemCatToInsert.CreatedDate = DateTime.Now;
            mItemCatToInsert.CreatedBy = User.Identity.Name;
            mItemCatToInsert.DataStatus = EnumDataStatus.New.ToString();
            _mActionCatRepository.Save(mItemCatToInsert);

            try
            {
                _mActionCatRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mActionCatRepository.DbContext.RollbackTransaction();

                //throw e.GetBaseException();
                return Content(e.GetBaseException().Message);
            }

            return Content("success");
        }

        [Transaction]
        public ActionResult Delete(MActionCat viewModel, FormCollection formCollection)
        {
            MActionCat mItemCatToDelete = _mActionCatRepository.Get(viewModel.Id);

            if (mItemCatToDelete != null)
            {
                _mActionCatRepository.Delete(mItemCatToDelete);
            }

            try
            {
                _mActionCatRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mActionCatRepository.DbContext.RollbackTransaction();

                return Content(e.GetBaseException().Message);
            }

            return Content("success");
        }

        [Transaction]
        public ActionResult Update(MActionCat viewModel, FormCollection formCollection)
        {
            MActionCat mItemCatToUpdate = _mActionCatRepository.Get(viewModel.Id);
            TransferFormValuesTo(mItemCatToUpdate, viewModel);
            mItemCatToUpdate.ModifiedDate = DateTime.Now;
            mItemCatToUpdate.ModifiedBy = User.Identity.Name;
            mItemCatToUpdate.DataStatus = EnumDataStatus.Updated.ToString();
            _mActionCatRepository.Update(mItemCatToUpdate);

            try
            {
                _mActionCatRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mActionCatRepository.DbContext.RollbackTransaction();

                return Content(e.GetBaseException().Message);
            }

            return Content("success");
        }

        private void TransferFormValuesTo(MActionCat mItemCatToUpdate, MActionCat mItemCatFromForm)
        {
            mItemCatToUpdate.ActionCatName = mItemCatFromForm.ActionCatName;
            mItemCatToUpdate.ActionCatDesc = mItemCatFromForm.ActionCatDesc;
        }

        [Transaction]
        public virtual ActionResult GetList()
        {
            var actionCats = _mActionCatRepository.GetAll();
            StringBuilder sb = new StringBuilder();
            MActionCat actionCat;
            sb.AppendFormat("{0}:{1};", string.Empty, "-Pilih Kategori Tindakan-");
            for (int i = 0; i < actionCats.Count; i++)
            {
                actionCat = actionCats[i];
                sb.AppendFormat("{0}:{1}", actionCat.Id, actionCat.ActionCatName);
                if (i < actionCats.Count - 1)
                    sb.Append(";");
            }
            return Content(sb.ToString());
        }
    }

}
