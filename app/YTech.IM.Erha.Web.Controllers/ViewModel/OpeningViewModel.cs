using System;
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
    public class OpeningViewModel
    {
        public static OpeningViewModel CreateOpeningViewModel(ITRecPeriodRepository tRecPeriodRepository, ITJournalRepository tJournalRepository)
        {
            OpeningViewModel viewModel = new OpeningViewModel();

            IList<TRecPeriod> listRecPeriod = tRecPeriodRepository.GetAll();
            viewModel.RecPeriodList = new SelectList(listRecPeriod, "Id", "PeriodDesc");

            return viewModel;
        }

        public SelectList RecPeriodList { get; internal set; }
        public string RecPeriodId { get; set; }
    }
}
