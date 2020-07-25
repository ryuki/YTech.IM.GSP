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
    public class ActionItemController : Controller
    {
        private readonly IMActionItemRepository _mActionItemRepository;
        private readonly IMActionRepository _mActionRepository;
        private readonly IMItemRepository _mItemRepository;
        public ActionItemController(IMActionItemRepository mActionItemRepository, IMActionRepository mActionRepository, IMItemRepository mItemRepository)
        {
            Check.Require(mActionItemRepository != null, "mActionItemRepository may not be null");
            Check.Require(mActionRepository != null, "mActionRepository may not be null");
            Check.Require(mItemRepository != null, "mItemRepository may not be null");
            this._mActionItemRepository = mActionItemRepository;
            this._mActionRepository = mActionRepository;
            this._mItemRepository = mItemRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [Transaction]
        public virtual ActionResult ListForSubGrid(string id)
        {
            var actionItems = _mActionItemRepository.GetByActionId(id);

            var jsonData = new
            {
                rows = (
                    from actionItem in actionItems
                    select new
                    {
                        i = actionItem.Id.ToString(),
                        cell = new string[] {
                           //itemCat.Id, 
                           //itemCat.PacketId != null ? itemCat.PacketId.Id : null, 
                           actionItem.ItemId  != null ? actionItem.ItemId.ItemName : null,
                            actionItem.ActionItemQty.HasValue ?  actionItem.ActionItemQty.Value.ToString(Helper.CommonHelper.NumberFormat) : null,
                           actionItem.ActionItemStatus,
                           actionItem.ActionItemDesc
                        }
                    }).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [Transaction]
        public virtual ActionResult List(string sidx, string sord, int page, int rows, string actionId)
        {
            int totalRecords = 10;
            var actionItems = _mActionItemRepository.GetByActionId(actionId);
            int pageSize = rows;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from actionItem in actionItems
                    select new
                    {
                        i = actionItem.Id.ToString(),
                        cell = new string[] {
                           //itemCat.Id, 
                           //itemCat.PacketId != null ? itemCat.PacketId.Id : null, 
                           actionItem.Id,
                           actionItem.ItemId  != null ? actionItem.ItemId.Id : null,
                           actionItem.ItemId  != null ? actionItem.ItemId.ItemName : null,
                            actionItem.ActionItemQty.HasValue ?  actionItem.ActionItemQty.Value.ToString(Helper.CommonHelper.NumberFormat) : null,
                           actionItem.ActionItemStatus,
                           actionItem.ActionItemDesc
                        }
                    }).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }



        public ActionResult AddActionItem()
        {
            return View();
        }

        [Transaction]
        public ActionResult Insert(MActionItem viewModel, FormCollection formCollection)
        {
            MActionItem actionItem = new MActionItem();
            actionItem.SetAssignedIdTo(Guid.NewGuid().ToString());
            TransferFormValuesTo(actionItem, viewModel);
            actionItem.ItemId = _mItemRepository.Get(formCollection["ItemId"]);
            actionItem.ActionId = _mActionRepository.Get(formCollection["ActionId"]);
            actionItem.CreatedDate = DateTime.Now;
            actionItem.CreatedBy = User.Identity.Name;
            actionItem.DataStatus = EnumDataStatus.New.ToString();
            _mActionItemRepository.Save(actionItem);

            try
            {
                _mActionItemRepository.DbContext.CommitChanges();
            }
            catch (Exception e)
            {

                _mActionItemRepository.DbContext.RollbackTransaction();

                //throw e.GetBaseException();
                return Content(e.GetBaseException().Message);
            }

            return Content("success");
        }

        [Transaction]
        public ActionResult Update(MActionItem viewModel, FormCollection formCollection)
        {
            MActionItem actionItem = _mActionItemRepository.Get(viewModel.Id);
            TransferFormValuesTo(actionItem, viewModel);
            actionItem.ItemId = _mItemRepository.Get(formCollection["ItemId"]);
            actionItem.ModifiedDate = DateTime.Now;
            actionItem.ModifiedBy = User.Identity.Name;
            actionItem.DataStatus = EnumDataStatus.Updated.ToString();
            _mActionItemRepository.Update(actionItem);

            try
            {
                _mActionItemRepository.DbContext.CommitTransaction();
            }
            catch (Exception e)
            {

                _mActionItemRepository.DbContext.RollbackTransaction();

                //throw e.GetBaseException();
                return Content(e.GetBaseException().Message);
            }

            return Content("success");
        }


        [Transaction]
        public ActionResult Delete(MActionItem viewModel)
        {
            MActionItem actionItem = _mActionItemRepository.Get(viewModel.Id);

            if (actionItem != null)
            {
                _mActionItemRepository.Delete(actionItem);
            }

            try
            {
                _mActionItemRepository.DbContext.CommitTransaction();
            }
            catch (Exception e)
            {

                _mActionItemRepository.DbContext.RollbackTransaction();

                return Content(e.GetBaseException().Message);
            }

            return Content("success");
        }

        private void TransferFormValuesTo(MActionItem actionItem, MActionItem viewModel)
        {
            actionItem.ActionItemStatus = viewModel.ActionItemStatus;
            actionItem.ActionItemDesc = viewModel.ActionItemDesc;
            actionItem.ActionItemQty = viewModel.ActionItemQty;
        }
    }
}
