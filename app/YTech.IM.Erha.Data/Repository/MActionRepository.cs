using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.RepositoryInterfaces;

namespace YTech.IM.GSP.Data.Repository
{
    public class MActionRepository : NHibernateRepositoryWithTypedId<MAction, string>, IMActionRepository
    {
        public IEnumerable<MAction> GetPagedActionList(string orderCol, string orderBy, int pageIndex, int maxRows, ref int totalRows)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(MAction));

            //calculate total rows
            totalRows = Session.CreateCriteria(typeof(MAction))
                .SetProjection(Projections.RowCount())
                .FutureValue<int>().Value;

            //get list results
            criteria.SetMaxResults(maxRows)
              .SetFirstResult((pageIndex - 1) * maxRows)
              .AddOrder(new Order(orderCol, orderBy.Equals("asc") ? true : false))
              ;

            IEnumerable<MAction> list = criteria.List<MAction>();
            return list;
        }

        public IEnumerable<MAction> GetPagedActionListByStatus(string orderCol, string orderBy, int pageIndex, int maxRows, ref int totalRows, string actionStatus)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(MAction));

            //calculate total rows
            totalRows = Session.CreateCriteria(typeof(MAction))
                .Add(Expression.Eq("ActionStatus",actionStatus))
                .SetProjection(Projections.RowCount())
                .FutureValue<int>().Value;

            //get list results
            criteria.SetMaxResults(maxRows)
                .Add(Expression.Eq("ActionStatus", actionStatus))
              .SetFirstResult((pageIndex - 1) * maxRows)
              .AddOrder(new Order(orderCol, orderBy.Equals("asc") ? true : false))
              ;

            IEnumerable<MAction> list = criteria.List<MAction>();
            return list;
        }
    }
}
