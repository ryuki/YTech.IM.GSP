using FluentNHibernate.Automapping;
using YTech.IM.GSP.Core.Master;
using FluentNHibernate.Automapping.Alterations;

namespace YTech.IM.GSP.Data.NHibernateMaps.Master
{
    public class MActionCommMap : IAutoMappingOverride<MActionComm>
    {
        #region Implementation of IAutoMappingOverride<MActionComm>

        public void Override(AutoMapping<MActionComm> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();
            //mapping.Cache.ReadOnly();

            mapping.Table("dbo.M_ACTION_COMM");
            mapping.Id(x => x.Id, "ACTION_COMM_ID")
                 .GeneratedBy.Assigned();

            mapping.References(x => x.ActionId, "ACTION_ID");
            mapping.References(x => x.EmployeeId, "EMPLOYEE_ID");
            mapping.Map(x => x.ActionCommType, "ACTION_COMM_TYPE");
            mapping.Map(x => x.ActionCommVal, "ACTION_COMM_VAL");
            mapping.Map(x => x.ActionCommStatus, "ACTION_COMM_STATUS");
            mapping.Map(x => x.ActionCommDesc, "ACTION_COMM_DESC");

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
