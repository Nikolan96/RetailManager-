using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.EventModels
{
    public class EditProductViewEvent
    {
        public ProductModel SelectedProduct { get; set; }

        public EditProductViewEvent(ProductModel productModel)
        {
            SelectedProduct = productModel;
        }
    }
}
