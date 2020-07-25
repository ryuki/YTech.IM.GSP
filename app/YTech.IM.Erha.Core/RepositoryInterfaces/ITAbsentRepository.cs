using System;
using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.HR;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
    public interface ITAbsentRepository : INHibernateRepositoryWithTypedId<TAbsent, string>
    {
        IList<TAbsent> GetAbsent(DateTime? dayWork);
        IList<TAbsent> GetAbsentByEmployeeId(MEmployee employeeId);
        IList<TAbsent> GetAbsentByEmployeeId(MEmployee employeeId, DateTime? workDay);
        IList<TAbsent> GetAbsentByEmployeeId(MEmployee employeeId, DateTime startPeriod, DateTime endPeriod);
    }
}
