using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManagerCore.Library.Models
{
    public class UserModel
    {
        public string ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public int ShopId { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
