using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;
using System;
using SharpArch.Core;

namespace YTech.IM.GSP.Core.Master
{
    public class MActionItem : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual MAction ActionId { get; set; }
        public virtual MItem ItemId { get; set; }
        public virtual decimal? ActionItemQty { get; set; }
        public virtual MItemUom ItemUomId { get; set; }
        public virtual string ActionItemStatus { get; set; }
        public virtual string ActionItemDesc { get; set; }  

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
