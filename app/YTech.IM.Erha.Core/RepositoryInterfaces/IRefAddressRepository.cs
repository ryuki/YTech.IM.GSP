using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;


namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
   public interface IRefAddressRepository : INHibernateRepositoryWithTypedId<RefAddress, string>
    {
    }
}
