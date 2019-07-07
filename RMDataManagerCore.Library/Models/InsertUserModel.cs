using System;
using System.Collections.Generic;
using System.Text;

namespace RMDataManagerCore.Library.Models
{
    public class InsertUserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }

        public string Role { get; set; }

        public int? ShopId { get; set; }
    }
}
