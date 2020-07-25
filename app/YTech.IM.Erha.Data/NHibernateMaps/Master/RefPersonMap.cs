using FluentNHibernate.Automapping;
using YTech.IM.GSP.Core.Master;
using FluentNHibernate.Automapping.Alterations;

namespace YTech.IM.GSP.Data.NHibernateMaps.Master
{
    public class RefPersonMap : IAutoMappingOverride<RefPerson>
    {
        #region Implementation of IAutoMappingOverride<RefPerson>

        public void Override(AutoMapping<RefPerson> mapping)
        {
            mapping.DynamicUpdate();
            mapping.DynamicInsert();
            mapping.SelectBeforeUpdate();

            mapping.Table("REF_PERSON");
            mapping.Id(x => x.Id, "PERSON_ID")
                 .GeneratedBy.Assigned();

            mapping.Map(x => x.PersonFirstName, "PERSON_FIRST_NAME");
            mapping.Map(x => x.PersonLastName, "PERSON_LAST_NAME");
            mapping.Map(x => x.PersonDob, "PERSON_DOB");
            mapping.Map(x => x.PersonPob, "PERSON_POB");
            mapping.Map(x => x.PersonGender, "PERSON_GENDER");
            mapping.Map(x => x.PersonPhone, "PERSON_PHONE");
            mapping.Map(x => x.PersonMobile, "PERSON_MOBILE");
            mapping.Map(x => x.PersonEmail, "PERSON_EMAIL");
            mapping.Map(x => x.PersonReligion, "PERSON_RELIGION");
            mapping.Map(x => x.PersonRace, "PERSON_RACE");
            mapping.Map(x => x.PersonIdCardType, "PERSON_ID_CARD_TYPE");
            mapping.Map(x => x.PersonIdCardNo, "PERSON_ID_CARD_NO");
            mapping.Map(x => x.PersonDesc, "PERSON_DESC");
            mapping.Map(x => x.PersonAnotherName, "PERSON_ANOTHER_NAME");
            mapping.Map(x => x.PersonOccupation, "PERSON_OCCUPATION");
            mapping.Map(x => x.PersonOfficceName, "PERSON_OFFICCE_NAME");
            mapping.Map(x => x.PersonOfficceAddress, "PERSON_OFFICCE_ADDRESS");
            mapping.Map(x => x.PersonOfficceCity, "PERSON_OFFICCE_CITY");
            mapping.Map(x => x.PersonOfficcePhone, "PERSON_OFFICCE_PHONE");
            mapping.Map(x => x.PersonOfficcePostCode, "PERSON_OFFICCE_POST_CODE");
            mapping.Map(x => x.PersonOfficceFax, "PERSON_OFFICCE_FAX");
            mapping.Map(x => x.PersonLastEdu, "PERSON_LAST_EDU");
            mapping.Map(x => x.PersonMarriedStatus, "PERSON_MARRIED_STATUS");
            mapping.Map(x => x.PersonHobby, "PERSON_HOBBY");
            mapping.Map(x => x.PersonNationality, "PERSON_NATIONALITY");
            mapping.Map(x => x.PersonBloodType, "PERSON_BLOOD_TYPE");
            
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
