using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.Accounting;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
    public interface ITRecAccountRepository : INHibernateRepositoryWithTypedId<TRecAccount, string>
    {
        void RunClosing (TRecPeriod recPeriod);
        IList<TRecAccount> GetByAccountType(string accountCatType, MCostCenter costCenter, TRecPeriod recPeriod);

        void RunOpening(TRecPeriod recPeriod);
    }
}
