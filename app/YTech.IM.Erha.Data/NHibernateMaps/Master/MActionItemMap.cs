using FluentNHibernate.Automapping;
using YTech.IM.GSP.Core.Master;
using FluentNHibernate.Automapping.Alterations;

namespace YTech.IM.GSP.Data.NHibernateMaps.Master
{
    public class MActionItemMap : IAutoMappingOverride<MActionItem>
    {
        #region Implementation of IAutoMappingOverride<MActionItem>

        public void Override(AutoMapping<MActionItem> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.M_ACTION_ITEM");
            mapping.Id(x => x.Id, "ACTION_ITEM_ID")
                 .GeneratedBy.Assigned();

            mapping.References(x => x.ActionId, "ACTION_ID");
            mapping.References(x => x.ItemId, "ITEM_ID");
            mapping.Map(x => x.ActionItemQty, "ACTION_ITEM_QTY");
            mapping.References(x => x.ItemUomId, "ITEM_UOM_ID");

            mapping.Map(x => x.ActionItemStatus, "ACTION_ITEM_STATUS");
            mapping.Map(x => x.ActionItemDesc, "ACTION_ITEM_DESC");

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
