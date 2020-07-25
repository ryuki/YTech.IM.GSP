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
    public class ActionCommController : Controller
    { 
        private readonly IMActionCommRepository _mActionCommRepository;
        private readonly IMActionRepository _mActionRepository;
        private readonly IMEmployeeRepository _mEmployeeRepository;
        public ActionCommController(IMActionCommRepository mActionCommRepository, IMActionRepository mActionRepository, IMEmployeeRepository mEmployeeRepository)
        {
            Check.Require(mActionCommRepository != null, "mActionCommRepository may not be null");
            Check.Require(mActionRepository != null, "mActionRepository may not be null");
            Check.Require(mEmployeeRepository != null, "mEmployeeRepository may not be null");
            
            this._mActionCommRepository = mActionCommRepository;
            this._mActionRepository = mActionRepository;
            this._mEmployeeRepository = mEmployeeRepository;
        }

        [Transaction]
        public virtual ActionResult GetListForSubGrid(string id)
        {
            var packets = _mActionCommRepository.GetByEmployeeId(id);

            var jsonData = new
            {
                rows = (
                    from packetComm in packets
                    select new
                    {
                        i = packetComm.Id.ToString(),
                        cell = new string[] {
                           packetComm.ActionId != null ? packetComm.ActionId.Id : null, 
                           packetComm.ActionId != null ? packetComm.ActionId.ActionName : null, 
                           packetComm.ActionCommType,
                            packetComm.ActionCommVal.HasValue ?  packetComm.ActionCommVal.Value.ToString(Helper.CommonHelper.NumberFormat) : null,
                           packetComm.ActionCommDesc
                        }
                    }).ToArray()
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        #region popup
   public virtual ActionResult PopupAdd()
        {
            return View();
        }

        [Transaction]
        public virtual ActionResult PopupList(string sidx, string sord, int page, int rows, string employeeId)
        {
            int totalRecords = 0;
            var packetComms = _mActionCommRepository.GetPagedActionCommList(sidx, sord, page, rows, ref totalRecords, employeeId);
            int pageSize = rows;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from packetComm in packetComms
                    select new
                    {
                        i = packetComm.Id,
                        cell = new string[] {
                            packetComm.Id,
                             packetComm.ActionId != null ? packetComm.ActionId.Id : null, 
                             packetComm.ActionId != null ? packetComm.ActionId.ActionName : null, 
                           packetComm.ActionCommType,
                            packetComm.ActionCommVal.HasValue ?  packetComm.ActionCommVal.Value.ToString(Helper.CommonHelper.NumberFormat) : null,
                           packetComm.ActionCommDesc
                        }
                    }).ToArray()
            };


            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [Transaction]
        public ActionResult PopupInsert(MActionComm viewModel, FormCollection formCollection, string EmployeeId)
        {
            UpdateNumericData(viewModel, formCollection);
            MActionComm packetComm = new MActionComm();
            TransferFormValuesTo(packetComm, viewModel, formCollection, EmployeeId);

            packetComm.SetAssignedIdTo(Guid.NewGuid().ToString());
            packetComm.CreatedDate = DateTime.Now;
            packetComm.CreatedBy = User.Identity.Name;
            packetComm.DataStatus = EnumDataStatus.New.ToString();

            //IList<MItemUom> listItemUom = new List<MItemUom>();

            //mItemToInsert.ItemUoms = listItemUom;

            _mActionCommRepository.Save(packetComm);

            try
            {
                _mActionCommRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mActionCommRepository.DbContext.RollbackTransaction();

                //throw e.GetBaseException();
                return Content(e.GetBaseException().Message);
            }

            return Content("Data komisi Tindakan berhasil disimpan");
        }

        [Transaction]
        public ActionResult PopupUpdate(MActionComm viewModel, FormCollection formCollection, string EmployeeId)
        {
            UpdateNumericData(viewModel, formCollection);
            MActionComm packetComm = _mActionCommRepository.Get(viewModel.Id);
            TransferFormValuesTo(packetComm, viewModel, formCollection, EmployeeId);

            packetComm.ModifiedDate = DateTime.Now;
            packetComm.ModifiedBy = User.Identity.Name;
            packetComm.DataStatus = EnumDataStatus.Updated.ToString();

            //IList<MItemUom> listItemUom = new List<MItemUom>();

            //mItemToInsert.ItemUoms = listItemUom;

            _mActionCommRepository.Update(packetComm);

            try
            {
                _mActionCommRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mActionCommRepository.DbContext.RollbackTransaction();

                //throw e.GetBaseException();
                return Content(e.GetBaseException().Message);
            }

            return Content("Data komisi Tindakan berhasil disimpan");
        }

        [Transaction]
        public ActionResult PopupDelete(MActionComm viewModel, FormCollection formCollection)
        {
            MActionComm packetComm = _mActionCommRepository.Get(viewModel.Id);

            if (packetComm != null)
            {
                _mActionCommRepository.Delete(packetComm);
            }

            try
            {
                _mActionCommRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mActionCommRepository.DbContext.RollbackTransaction();

                return Content(e.GetBaseException().Message);
            }

            return Content("Data komisi Tindakan berhasil dihapus");
        }

        private void TransferFormValuesTo(MActionComm packetComm, MActionComm viewModel, FormCollection formCollection, string EmployeeId)
        {
            packetComm.ActionId = _mActionRepository.Get(formCollection["ActionId"]);
            packetComm.EmployeeId = _mEmployeeRepository.Get(EmployeeId);

            packetComm.ActionCommVal = viewModel.ActionCommVal;
            packetComm.ActionCommType = viewModel.ActionCommType;
            packetComm.ActionCommStatus = viewModel.ActionCommStatus;
            packetComm.ActionCommDesc = viewModel.ActionCommDesc;
        }

        private void UpdateNumericData(MActionComm viewModel, FormCollection formCollection)
        {
            if (!string.IsNullOrEmpty(formCollection["ActionCommVal"]))
            {
                string ActionCommVal = formCollection["ActionCommVal"].Replace(",", "");
                viewModel.ActionCommVal = Convert.ToDecimal(ActionCommVal);
            }
            else
            {
                viewModel.ActionCommVal = null;
            }
        }
        #endregion

     

    }
}
