using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
    public interface IMActionItemRepository : INHibernateRepositoryWithTypedId<MActionItem, string>
    {
        //IEnumerable<MActionItem> GetPagedActionList(string orderCol, string orderBy, int pageIndex, int maxRows, ref int totalRows);
        IList<MActionItem> GetByActionId(string actionId);
    }
}
