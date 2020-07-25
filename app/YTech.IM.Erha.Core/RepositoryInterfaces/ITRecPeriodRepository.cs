using System;
using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.Accounting;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
    public interface ITRecPeriodRepository : INHibernateRepositoryWithTypedId<TRecPeriod, string>
    {
        DateTime? GetLastDateClosing();
    }
}
