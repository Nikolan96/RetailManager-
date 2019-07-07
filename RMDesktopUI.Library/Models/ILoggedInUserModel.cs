using System;

namespace RMDesktopUI.Library.Models
{
    public interface ILoggedInUserModel
    {

         string ID { get; set; }

         string FirstName { get; set; }

         string LastName { get; set; }

         string EmailAddress { get; set; }

         string Role { get; set; }

         int ShopId { get; set; }

         DateTime CreatedDate { get; set; }

        void PopulateLoggedInUser(LoggedInUserModel user);
    }
}