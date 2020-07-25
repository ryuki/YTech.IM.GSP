﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Reporting.WebForms;
using SharpArch.Core;
using SharpArch.Web.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.RepositoryInterfaces;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.Accounting;
using YTech.IM.GSP.Core.Transaction.Inventory;
using YTech.IM.GSP.Data.Repository;
using YTech.IM.GSP.Enums;
using YTech.IM.GSP.Web.Controllers.ViewModel;

namespace YTech.IM.GSP.Web.Controllers.Transaction
{
    public class ReturPurchase : AbstractTransaction
    {
        #region Overrides of AbstractTransaction

        public override void SaveJournal(TTrans trans, decimal totalHPP)
        {
            //string desc = string.Format("Retur pembelian dari {0}", trans.TransBy);
            //string newVoucher = Helper.CommonHelper.GetVoucherNo(false);
            ////save header of journal
            //TJournal journal = SaveJournalHeader(newVoucher, trans, desc);
            //MAccountRef accountRef = null;

            //if (trans.TransPaymentMethod == EnumPaymentMethod.Tunai.ToString())
            //{
            //    //save cash
            //    SaveJournalDet(journal, newVoucher, Helper.AccountHelper.GetCashAccount(), EnumJournalStatus.D, trans.TransGrandTotal.Value, trans, desc);
            //}
            //else
            //{
            //    accountRef = AccountRefRepository.GetByRefTableId(EnumReferenceTable.Supplier, trans.TransBy);
            //    //save hutang
            //    SaveJournalDet(journal, newVoucher, accountRef.AccountId, EnumJournalStatus.D, trans.TransGrandTotal.Value, trans, desc);
            //}

            ////save retur pembelian
            //SaveJournalDet(journal, newVoucher, Helper.AccountHelper.GetReturPurchaseAccount(), EnumJournalStatus.K, trans.TransGrandTotal.Value, trans, desc);

            ////save ikhtiar LR
            //SaveJournalDet(journal, newVoucher, Helper.AccountHelper.GetIkhtiarLRAccount(), EnumJournalStatus.D, totalHPP, trans, desc);

            ////save persediaan
            //accountRef = AccountRefRepository.GetByRefTableId(EnumReferenceTable.Warehouse, trans.WarehouseId.Id);
            //SaveJournalDet(journal, newVoucher, accountRef.AccountId, EnumJournalStatus.K, totalHPP, trans, desc);

            //JournalRepository.Save(journal);

            ////save journal ref
            //SaveJournalRef(trans, journal);
        }

        #endregion
    }
}
