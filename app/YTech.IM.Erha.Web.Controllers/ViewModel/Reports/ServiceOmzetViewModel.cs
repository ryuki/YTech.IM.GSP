using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.Accounting;

namespace YTech.IM.GSP.Web.Controllers.ViewModel.Reports
{
    public class ServiceOmzetViewModel : TTransRoom
    {
        public decimal? TransactionSubtotal { get; set; }
        public decimal? TransactionDiscount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string TransactionFactur { get; set; }
    }
}
