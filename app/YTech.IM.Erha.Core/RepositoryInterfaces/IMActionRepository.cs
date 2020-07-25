using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
    public interface IMActionRepository : INHibernateRepositoryWithTypedId<MAction, string>
    {
        IEnumerable<MAction> GetPagedActionList(string orderCol, string orderBy, int pageIndex, int maxRows, ref int totalRows);

        IEnumerable<MAction> GetPagedActionListByStatus(string orderCol, string orderBy, int pageIndex, int maxRows, ref int totalRows, string actionStatus);
    }
}
