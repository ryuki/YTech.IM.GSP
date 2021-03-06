﻿using System.Collections.Generic;
using NHibernate.Validator.Constraints;
using SharpArch.Core.DomainModel;
using System;
using SharpArch.Core;
using YTech.IM.GSP.Core.Master;

namespace YTech.IM.GSP.Core.Transaction.Inventory
{
    public class TCommission : EntityWithTypedId<string>, IHasAssignedId<string>
    {
        public TCommission() { }

        public TCommission(TTransDet transDet)
        {
            Check.Require(transDet != null, "transDet may not be null");

            TransDetId = transDet;
        }

        [DomainSignature]
        [NotNull, NotEmpty]
        public virtual TTransDet TransDetId { get; protected set; }
        public virtual MEmployee EmployeeId { get; set; }
        public virtual string CommissionType { get; set; }
        public virtual decimal ? CommissionValue { get; set; }
        public virtual string CommissionStatus { get; set; }
        public virtual string CommissionDesc { get; set; }

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
