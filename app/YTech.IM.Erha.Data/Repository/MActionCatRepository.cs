using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.RepositoryInterfaces;

namespace YTech.IM.GSP.Data.Repository
{
    public class MActionCatRepository : NHibernateRepositoryWithTypedId<MActionCat, string>, IMActionCatRepository
    {
        public IEnumerable<MActionCat> GetPagedActionCatList(string orderCol, string orderBy, int pageIndex, int maxRows, ref int totalRows)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(MActionCat));

            //calculate total rows
            totalRows = Session.CreateCriteria(typeof(MActionCat))
                .SetProjection(Projections.RowCount())
                .FutureValue<int>().Value;

            //get list results
            criteria.SetMaxResults(maxRows)
              .SetFirstResult((pageIndex - 1) * maxRows)
              .AddOrder(new Order(orderCol, orderBy.Equals("asc") ? true : false))
              ;

            IEnumerable<MActionCat> list = criteria.List<MActionCat>();
            return list;
        }
    }
}
