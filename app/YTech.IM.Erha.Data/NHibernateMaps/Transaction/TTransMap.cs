﻿using FluentNHibernate.Automapping;
using YTech.IM.GSP.Core.Master;
using FluentNHibernate.Automapping.Alterations;
using YTech.IM.GSP.Core.Transaction;

namespace YTech.IM.GSP.Data.NHibernateMaps.Transaction
{
    public class TTransMap : IAutoMappingOverride<TTrans>
    {
        #region Implementation of IAutoMappingOverride<TTrans>

        public void Override(AutoMapping<TTrans> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.T_TRANS");
            mapping.Id(x => x.Id, "TRANS_ID")
                 .GeneratedBy.Assigned();

            mapping.References(x => x.WarehouseId, "WAREHOUSE_ID").LazyLoad();
            mapping.References(x => x.WarehouseIdTo, "WAREHOUSE_ID_TO").LazyLoad();
            mapping.Map(x => x.TransDate, "TRANS_DATE");
            mapping.Map(x => x.TransBy, "TRANS_BY");
            mapping.References(x => x.TransRefId, "TRANS_REF_ID").LazyLoad();
            mapping.Map(x => x.TransFactur, "TRANS_FACTUR");
            mapping.References(x => x.EmployeeId, "EMPLOYEE_ID").LazyLoad();
            mapping.Map(x => x.TransDueDate, "TRANS_DUE_DATE");
            mapping.Map(x => x.TransPaymentMethod, "TRANS_PAYMENT_METHOD");
            mapping.Map(x => x.TransSubTotal, "TRANS_SUB_TOTAL");
            mapping.Map(x => x.TransDiscount, "TRANS_DISC");
            mapping.Map(x => x.TransTax, "TRANS_TAX");
            mapping.Map(x => x.TransStatus, "TRANS_STATUS");
            mapping.Map(x => x.TransDesc, "TRANS_DESC");

            mapping.Map(x => x.DataStatus, "DATA_STATUS");
            mapping.Map(x => x.CreatedBy, "CREATED_BY");
            mapping.Map(x => x.CreatedDate, "CREATED_DATE");
            mapping.Map(x => x.ModifiedBy, "MODIFIED_BY");
            mapping.Map(x => x.ModifiedDate, "MODIFIED_DATE");
            mapping.Map(x => x.RowVersion, "ROW_VERSION").ReadOnly();


            mapping.HasMany(x => x.TransDets)
                .AsBag()
                .Inverse()
                .KeyColumn("TRANS_ID");
                //.Not.LazyLoad();
        }

        #endregion
    }
}
