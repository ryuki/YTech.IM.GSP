using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
   public interface IMItemUomRepository : INHibernateRepositoryWithTypedId<MItemUom, string>
   {
       MItemUom GetByItem(MItem item);
   }
}
