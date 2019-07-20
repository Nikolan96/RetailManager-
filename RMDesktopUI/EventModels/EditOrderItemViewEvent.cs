using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.EventModels
{
    public class EditOrderItemViewEvent : INavigationEvent
    {
        public int ID { get; set; }

        public EditOrderItemViewEvent(int ID)
        {
            this.ID = ID;
        }
    }
}
