﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using SharpArch.Core;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Web.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.RepositoryInterfaces;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.Accounting;
using YTech.IM.GSP.Enums;

namespace YTech.IM.GSP.Web.Controllers.ViewModel
{
    public class ClosingViewModel
    {
        public static ClosingViewModel CreateClosingViewModel(ITRecPeriodRepository tRecPeriodRepository, ITJournalRepository tJournalRepository)
        {
            ClosingViewModel viewModel = new ClosingViewModel();
            DateTime? dt = tRecPeriodRepository.GetLastDateClosing();
            if (dt.HasValue)
            {
                dt = dt.Value.AddDays(1);
            }
            else
            {
                dt = tJournalRepository.GetMinDateJournal();
            }
            viewModel.DateFrom = dt;

            return viewModel;
        }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
