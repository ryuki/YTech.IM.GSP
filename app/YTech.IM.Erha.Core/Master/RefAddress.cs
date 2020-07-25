using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;
using System;
using SharpArch.Core;

namespace YTech.IM.GSP.Core.Master
{
    public class RefAddress : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        [NotNull, NotEmpty]
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string AddressLine3 { get; set; }
        public virtual string AddressPhone { get; set; }
        public virtual string AddressFax { get; set; }
        public virtual string AddressCity { get; set; }
        public virtual string AddressContact { get; set; }
        public virtual string AddressContactMobile { get; set; }
        public virtual string AddressEmail { get; set; }
        public virtual string AddressRt { get; set; }
        public virtual string AddressRw { get; set; }
        public virtual string AddressPostCode { get; set; }

        public virtual string AddressIdCardLine1 { get; set; }
        public virtual string AddressIdCardLine2 { get; set; }
        public virtual string AddressIdCardLine3 { get; set; }
        public virtual string AddressIdCardPhone { get; set; }
        public virtual string AddressIdCardCity { get; set; }
        public virtual string AddressIdCardRt { get; set; }
        public virtual string AddressIdCardRw { get; set; }
        public virtual string AddressIdCardPostCode { get; set; }

        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }

        #region Implementation of IHasAssignedId<string>

        public virtual void SetAssignedIdTo(string assignedId)
        {
            Check.Require(!string.IsNullOrEmpty(assignedId), "Assigned Id may not be null or empty");
            Id = assignedId.Trim();
        }

        #endregion
    }
}
