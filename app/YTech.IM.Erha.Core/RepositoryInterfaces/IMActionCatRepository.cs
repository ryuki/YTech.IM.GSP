using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
    public interface IMActionCatRepository : INHibernateRepositoryWithTypedId<MActionCat, string>
    {
        IEnumerable<MActionCat> GetPagedActionCatList(string orderCol, string orderBy, int pageIndex, int maxRows, ref int totalRows);
    }
}
