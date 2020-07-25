using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.RepositoryInterfaces;

namespace YTech.IM.GSP.Data.Repository
{
    public class MActionCommRepository : NHibernateRepositoryWithTypedId<MActionComm, string>, IMActionCommRepository
    {
        #region IMActionRepository Members

        public IEnumerable<MActionComm> GetPagedActionCommList(string orderCol, string orderBy, int pageIndex, int maxRows, ref int totalRows, string employeeId)
        {
            //get employee entitiy
            //MEmployee emp = Session.

            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"  from MActionComm as p ");
            if (!string.IsNullOrEmpty(employeeId))
            {
                sql.AppendLine(@" where p.EmployeeId.Id = :employeeId");
            }

            string queryCount = string.Format(" select count(p.Id) {0}", sql);
            IQuery q = Session.CreateQuery(queryCount);
            if (!string.IsNullOrEmpty(employeeId))
            {
                q.SetString("employeeId", employeeId);
            }

            totalRows = Convert.ToInt32(q.UniqueResult());
            //totalRows = (int)q.UniqueResult();// q.FutureValue<int>().Value;


            sql.AppendFormat(@" order by  p.{0} {1}", orderCol, orderBy);
            string query = string.Format(" select p {0}", sql);
            q = Session.CreateQuery(query);
            if (!string.IsNullOrEmpty(employeeId))
            {
                q.SetString("employeeId", employeeId);
            }
            q.SetMaxResults(maxRows);
            q.SetFirstResult((pageIndex - 1) * maxRows);
            IEnumerable<MActionComm> list = q.List<MActionComm>();
            return list;


            //ICriteria criteria = Session.CreateCriteria(typeof(MActionComm));

            ////calculate total rows
            //totalRows = Session.CreateCriteria(typeof(MActionComm))
            //    .SetProjection(Projections.RowCount())
            //    .FutureValue<int>().Value;

            ////get list results
            //criteria.SetMaxResults(maxRows)
            //  .SetFirstResult((pageIndex - 1) * maxRows)
            //  .AddOrder(new Order(orderCol, orderBy.Equals("asc") ? true : false))
            //  ;

            //IEnumerable<MActionComm> list = criteria.List<MActionComm>();
            //return list;
        }

        public IList<MActionComm> GetByEmployeeId(string employeeId)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"   select p
                                from MActionComm as p");
            if (!string.IsNullOrEmpty(employeeId))
            {
                sql.AppendLine(@" where p.EmployeeId.Id = :employeeId");
            }
            IQuery q = Session.CreateQuery(sql.ToString());
            if (!string.IsNullOrEmpty(employeeId))
            {
                q.SetString("employeeId", employeeId);
            }

            return q.List<MActionComm>();
        }

        public MActionComm GetByEmployeeAndAction(MEmployee emp, MAction action)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine(@"   select p
                                from MActionComm as p");
            sql.AppendLine(@" where p.EmployeeId = :emp");
            sql.AppendLine(@"   and p.ActionId = :action");

            IQuery q = Session.CreateQuery(sql.ToString());
            q.SetEntity("emp", emp);
            q.SetEntity("action", action);

            return q.UniqueResult<MActionComm>();
        }

        #endregion
    }
}
