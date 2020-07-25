using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
    public interface IMActionCommRepository : INHibernateRepositoryWithTypedId<MActionComm, string>
    {
        IEnumerable<MActionComm> GetPagedActionCommList(string orderCol, string orderBy, int pageIndex, int maxRows, ref int totalRows, string employeeId);

        IList<MActionComm> GetByEmployeeId(string employeeId);

        MActionComm GetByEmployeeAndAction(MEmployee emp, MAction action);
    }
}
