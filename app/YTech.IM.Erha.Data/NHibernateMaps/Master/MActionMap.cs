using FluentNHibernate.Automapping;
using YTech.IM.GSP.Core.Master;
using FluentNHibernate.Automapping.Alterations;

namespace YTech.IM.GSP.Data.NHibernateMaps.Master
{
    public class MActionMap : IAutoMappingOverride<MAction>
    {
        #region Implementation of IAutoMappingOverride<MAction>

        public void Override(AutoMapping<MAction> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.M_ACTION");
            mapping.Id(x => x.Id, "ACTION_ID")
                 .GeneratedBy.Assigned();

            mapping.References(x => x.ActionCatId, "ACTION_CAT_ID");
            mapping.Map(x => x.ActionName, "ACTION_NAME");

            mapping.Map(x => x.ActionPrice, "ACTION_PRICE");
            mapping.Map(x => x.ActionComponentTool, "ACTION_COMPONENT_TOOL");
            mapping.Map(x => x.ActionComponentMedician, "ACTION_COMPONENT_MEDICIAN");
            mapping.Map(x => x.ActionComponentDoctor, "ACTION_COMPONENT_DOCTOR");
            mapping.Map(x => x.ActionComponentTherapist, "ACTION_COMPONENT_THERAPIST");
            mapping.Map(x => x.ActionStatus, "ACTION_STATUS");
            mapping.Map(x => x.ActionDesc, "ACTION_DESC");
            mapping.Map(x => x.ActionComponentMedicianType, "ACTION_COMPONENT_MEDICIAN_TYPE");
            mapping.Map(x => x.ActionComponentDoctorType, "ACTION_COMPONENT_DOCTOR_TYPE");
            mapping.Map(x => x.ActionComponentTherapistType, "ACTION_COMPONENT_THERAPIST_TYPE");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();

            mapping.HasMany(x => x.ActionItems)
                //.Access.Property()
                .AsBag()
                .Inverse()
                .KeyColumn("ACTION_ID");
        }

        #endregion
    }
}
