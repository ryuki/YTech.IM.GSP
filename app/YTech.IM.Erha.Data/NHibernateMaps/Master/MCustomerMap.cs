using FluentNHibernate.Automapping;
using YTech.IM.GSP.Core.Master;
using FluentNHibernate.Automapping.Alterations;

namespace YTech.IM.GSP.Data.NHibernateMaps.Master
{
   public class MCustomerMapp : IAutoMappingOverride<MCustomer>
    {
        #region Implementation of IAutoMappingOverride<MCustomer>

        public void Override(AutoMapping<MCustomer> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("dbo.M_CUSTOMER");
            mapping.Id(x => x.Id, "CUSTOMER_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.CustomerStatus, "CUSTOMER_STATUS");
            mapping.Map(x => x.CustomerDesc, "CUSTOMER_DESC");
            
            // mapping.HasOne(x => x.AddressId).EntityName(typeof(RefAddress).Name);//.ForeignKey("ADDRESS_ID");
            mapping.References(x => x.AddressId, "ADDRESS_ID").Fetch.Join(); 
            mapping.References(x => x.PersonId, "PERSON_ID").Fetch.Join(); 
            //mapping.HasOne(x => x.AddressId).ForeignKey("ADDRESS_ID");

            mapping.Map(x => x.CustomerLetter, "CUSTOMER_LETTER");
            mapping.Map(x => x.CustomerPhoneJakarta, "CUSTOMER_PHONE_JAKARTA");
            mapping.Map(x => x.CustomerFaceTreatment, "CUSTOMER_FACE_TREATMENT");
            mapping.Map(x => x.CustomerAllergy, "CUSTOMER_ALLERGY");
            mapping.Map(x => x.CustomerSkinProblem, "CUSTOMER_SKIN_PROBLEM");
            mapping.Map(x => x.CustomerPlanTreatment, "CUSTOMER_PLAN_TREATMENT");
            
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
