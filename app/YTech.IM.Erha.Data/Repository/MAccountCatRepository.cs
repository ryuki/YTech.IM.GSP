using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.RepositoryInterfaces;

namespace YTech.IM.GSP.Data.Repository
{
    public class MAccountCatRepository : NHibernateRepositoryWithTypedId<MAccountCat, string>, IMAccountCatRepository
    {
    }
}
