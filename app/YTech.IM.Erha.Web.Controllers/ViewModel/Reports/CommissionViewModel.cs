using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.Accounting;
using YTech.IM.GSP.Core.Transaction.Inventory;

namespace YTech.IM.GSP.Web.Controllers.ViewModel.Reports
{
    public class CommissionViewModel : TCommission
    {
        public string ActionName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? TransDate { get; set; }
        public string TransFactur { get; set; }
        public string TransDetQty { get; set; }
        public string TransDetTotal { get; set; }
    }
}
