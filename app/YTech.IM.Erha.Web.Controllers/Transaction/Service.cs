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
    public class Service : AbstractTransaction
    {
        #region Overrides of AbstractTransaction

        public override void SaveJournal(TTrans trans, decimal totalHPP)
        {
            string desc = string.Format("Penjualan paket jasa kepada {0}", trans.TransBy);
            string newVoucher = Helper.CommonHelper.GetVoucherNo(false);
            //delete journal first
            DeleteJournal(EnumReferenceTable.Transaction, trans.TransStatus, trans.Id);
            //save header of journal
            TJournal journal = SaveJournalHeader(trans.WarehouseId.CostCenterId, newVoucher, trans.TransBy, trans.TransDate, trans.TransFactur, desc);
            MAccountRef accountRef = null;

            if (trans.TransPaymentMethod == EnumPaymentMethod.Tunai.ToString())
            {
                //save cash
                SaveJournalDet(journal, newVoucher, Helper.AccountHelper.GetCashAccount(), EnumJournalStatus.D, trans.TransGrandTotal.Value, trans.TransFactur, desc);
            }
            else
            {
                accountRef = AccountRefRepository.GetByRefTableId(EnumReferenceTable.Customer, trans.TransBy);
                //save piutang
                SaveJournalDet(journal, newVoucher, accountRef.AccountId, EnumJournalStatus.D, trans.TransGrandTotal.Value, trans.TransFactur, desc);
            }
            //save penjualan
            SaveJournalDet(journal, newVoucher, Helper.AccountHelper.GetSalesAccount(), EnumJournalStatus.K, trans.TransGrandTotal.Value, trans.TransFactur, desc);

            //save ikhtiar LR
            SaveJournalDet(journal, newVoucher, Helper.AccountHelper.GetIkhtiarLRAccount(), EnumJournalStatus.D, totalHPP, trans.TransFactur, desc);

            //save persediaan
            accountRef = AccountRefRepository.GetByRefTableId(EnumReferenceTable.Warehouse, trans.WarehouseId.Id);
            SaveJournalDet(journal, newVoucher, accountRef.AccountId, EnumJournalStatus.K, totalHPP, trans.TransFactur, desc);

            JournalRepository.Save(journal);

            //save journal ref
            SaveJournalRef(journal, trans.Id, trans.TransStatus, trans.TransDesc);
        }

        #endregion
    }
}
