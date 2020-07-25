using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;
using System;
using SharpArch.Core;

namespace YTech.IM.GSP.Core.Master
{
    public class MAction : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature]
        public virtual string ActionName { get; set; }
        public virtual MActionCat ActionCatId{ get; set; }
        public virtual decimal? ActionPrice { get; set; }
        public virtual decimal? ActionComponentTool { get; set; }
        public virtual decimal? ActionComponentMedician { get; set; }
        public virtual decimal? ActionComponentDoctor { get; set; }
        public virtual decimal? ActionComponentTherapist { get; set; }
        public virtual string ActionStatus { get; set; }
        public virtual string ActionDesc { get; set; }
        public virtual string ActionComponentMedicianType { get; set; }
        public virtual string ActionComponentDoctorType { get; set; }
        public virtual string ActionComponentTherapistType { get; set; }

        public virtual string DataStatus { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual byte[] RowVersion { get; set; }

        public virtual IList<MActionItem> ActionItems { get; protected set; }

        #region Implementation of IHasAssignedId<string>

        public virtual void SetAssignedIdTo(string assignedId)
        {
            Check.Require(!string.IsNullOrEmpty(assignedId), "Assigned Id may not be null or empty");
            Id = assignedId.Trim();
        }

        #endregion
    }
}
