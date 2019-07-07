using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.EventModels
{
    public class EditShopViewEvent
    {
        public ShopModel SelectedShop { get; set; }

        public EditShopViewEvent(ShopModel shopModel)
        {
            SelectedShop = shopModel;
        }
    }
}
