using RMDesktopUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.EventModels
{
    public class EditUserViewEvent
    {
        public UserModel SelectedUser { get; set; }

        public EditUserViewEvent(UserModel user)
        {
            SelectedUser = user;
        }
    }
}
