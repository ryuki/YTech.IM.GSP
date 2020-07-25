using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;
using System;
using SharpArch.Core;

namespace YTech.IM.GSP.Core.Master
{
    public class MActionComm : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        [DomainSignature] 
        public virtual MAction ActionId { get; set; }
        public virtual MEmployee EmployeeId { get; set; }
        public virtual string ActionCommType { get; set; }
        public virtual decimal? ActionCommVal { get; set; }
        public virtual string ActionCommStatus { get; set; }
        public virtual string ActionCommDesc { get; set; }

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
