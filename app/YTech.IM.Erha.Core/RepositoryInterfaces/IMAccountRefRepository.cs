using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
    public interface IMAccountRefRepository : INHibernateRepositoryWithTypedId<MAccountRef, string>
    {

        MAccountRef GetByRefTableId(Enums.EnumReferenceTable enumReferenceTable, string warehouseId);
    }
}
