using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.Accounting;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
    public interface ITJournalRefRepository : INHibernateRepositoryWithTypedId<TJournalRef, string>
    {
        TJournalRef GetByReference(Enums.EnumReferenceTable enumReferenceTable, string transStatus, string transId);
    }
}
