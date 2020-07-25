using System;
using System.Collections.Generic;
using System.Text;
using NHibernate;
using NHibernate.Criterion;
using SharpArch.Data.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.RepositoryInterfaces;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.Inventory;
using YTech.IM.GSP.Enums;

namespace YTech.IM.GSP.Data.Repository
{
  public  class TCommissionRepository : NHibernateRepositoryWithTypedId<TCommission, string>, ITCommissionRepository
    {
      public TCommission GetByTransDetAndCommissionType(TTransDet det, EnumCommissionPeople enumCommissionPeople)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine(@"   select com
                                from TCommission as com
                                        where com.TransDetId = :det
                                    and com.CommissionType = :commissionPeople ");
          IQuery q = Session.CreateQuery(sql.ToString());
          q.SetString("commissionPeople", enumCommissionPeople.ToString());
          q.SetEntity("det", det);
          return q.UniqueResult<TCommission>();
      }

      public IList<TCommission> GetByTransDate(DateTime dateFrom, DateTime dateTo)
      {
          StringBuilder sql = new StringBuilder();
          sql.AppendLine(@"   select com
                                from TCommission as com
                                    left outer join com.TransDetId as det
                                        where det.TransId.TransDate between :dateFrom and :dateTo ");
          IQuery q = Session.CreateQuery(sql.ToString());
          q.SetDateTime("dateFrom", dateFrom);
          q.SetDateTime("dateTo", dateTo);
          return q.List<TCommission>();
      }
    }
}
