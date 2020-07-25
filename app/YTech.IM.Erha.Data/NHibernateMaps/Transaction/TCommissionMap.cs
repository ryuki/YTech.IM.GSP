using FluentNHibernate.Automapping;
using YTech.IM.GSP.Core.Master;
using FluentNHibernate.Automapping.Alterations;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.HR;
using YTech.IM.GSP.Core.Transaction.Inventory;

namespace YTech.IM.GSP.Data.NHibernateMaps.Transaction
{
    public class TCommissionMap : IAutoMappingOverride<TCommission>
    {
        #region IAutoMappingOverride<TCommission> Members

        public void Override(AutoMapping<TCommission> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.T_COMMISSION");
            mapping.Id(x => x.Id, "COMMISSION_ID")
                 .GeneratedBy.Assigned();

            mapping.References(x => x.TransDetId, "TRANS_DET_ID").Not.Nullable();
            mapping.References(x => x.EmployeeId, "EMPLOYEE_ID").Not.Nullable();
            mapping.Map(x => x.CommissionType, "COMMISSION_TYPE");
            mapping.Map(x => x.CommissionValue, "COMMISSION_VALUE"); 
            mapping.Map(x => x.CommissionStatus, "COMMISSION_STATUS");
            mapping.Map(x => x.CommissionDesc, "COMMISSION_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();
        }

        #endregion
    }
}
