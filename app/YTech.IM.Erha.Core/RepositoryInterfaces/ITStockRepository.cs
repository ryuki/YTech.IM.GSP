﻿using System;
using System.Collections;
using System.Collections.Generic;
using SharpArch.Core.PersistenceSupport.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.Inventory;

namespace YTech.IM.GSP.Core.RepositoryInterfaces
{
    public interface ITStockRepository : INHibernateRepositoryWithTypedId<TStock, string>
    {
        IList GetSisaStockList(MItem itemId, MWarehouse mWarehouse);
    }
}
