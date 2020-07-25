using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
    public interface IMAccountRepository : INHibernateRepositoryWithTypedId<MAccount, string>
    {
        IEnumerable<MAccount> GetPagedAccountList(string orderCol, string orderBy, int pageIndex, int maxRows, ref int totalRows,MAccountCat accountCat);
        IList<MAccount> GetByAccountCat(MAccountCat accountCat);
    }
}
