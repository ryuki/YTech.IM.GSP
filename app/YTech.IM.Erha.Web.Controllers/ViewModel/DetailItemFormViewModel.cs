using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using SharpArch.Core;
using SharpArch.Core.PersistenceSupport;
using SharpArch.Web.NHibernate;
using YTech.IM.GSP.Core.Master;
using YTech.IM.GSP.Core.RepositoryInterfaces;
using YTech.IM.GSP.Core.Transaction;
using YTech.IM.GSP.Core.Transaction.Inventory;
using YTech.IM.GSP.Enums;

namespace YTech.IM.GSP.Web.Controllers.ViewModel
{
   public class DetailItemFormViewModel
    {
       public static DetailItemFormViewModel Create(string packetId, IMPacketItemCatRepository mPacketItemCatRepository)
        {
            DetailItemFormViewModel viewModel = new DetailItemFormViewModel();
           viewModel.PacketItemCatList = mPacketItemCatRepository.GetByPacketId(packetId);
            return viewModel;
        }
        public IList<MPacketItemCat> PacketItemCatList { get; internal set; }
    }
}
