using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
    public interface IMCustomerRepository : INHibernateRepositoryWithTypedId<MCustomer, string>
    {
        IEnumerable<MCustomer> GetPagedCustomerList(string orderCol, string orderBy, int pageIndex, int maxRows, ref int totalRows);
        IEnumerable<MCustomer> GetPagedCustomerList(string orderCol, string orderBy, int pageIndex, int maxRows, ref int totalRows, string searchBy, string searchText);

    }
}
