using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.RepositoryInterfaces;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.Inventory;

namespace YTech.IM.GSP.Data.Repository
{
    public class TStockRefRepository : NHibernateRepositoryWithTypedId<TStockRef,string >, ITStockRefRepository
    {
       
    }
}
