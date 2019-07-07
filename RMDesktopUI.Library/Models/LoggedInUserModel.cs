using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.Library.Models
{
    public class LoggedInUserModel : ILoggedInUserModel
    {

        public string ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Role { get; set; }

        public int ShopId { get; set; }

        public DateTime CreatedDate { get; set; }

        public void PopulateLoggedInUser(LoggedInUserModel user)
        {
            this.ID = user.ID;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.EmailAddress = user.EmailAddress;
            this.Role = user.Role;
            this.ShopId = user.ShopId;
            this.CreatedDate = user.CreatedDate;
        }
    }
}
