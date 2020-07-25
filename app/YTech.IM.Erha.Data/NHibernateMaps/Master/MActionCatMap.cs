using FluentNHibernate.Automapping;
using YTech.IM.GSP.Core.Master;
using FluentNHibernate.Automapping.Alterations;

namespace YTech.IM.GSP.Data.NHibernateMaps.Master
{
    public class MActionCatMap : IAutoMappingOverride<MActionCat>
    {
        #region Implementation of IAutoMappingOverride<MActionCat>

        public void Override(AutoMapping<MActionCat> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.M_ACTION_CAT");
            mapping.Id(x => x.Id, "ACTION_CAT_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.ActionCatName, "ACTION_CAT_NAME");  
            mapping.Map(x => x.ActionCatStatus, "ACTION_CAT_STATUS");
            mapping.Map(x => x.ActionCatDesc, "ACTION_CAT_DESC");  

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
