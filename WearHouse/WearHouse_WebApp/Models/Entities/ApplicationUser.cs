using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WearHouse_WebApp.Models.Domain;

namespace WearHouse_WebApp.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Location { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImageUrl { get; set; }

        public List<dbWearable> Wearables { get; set; }

        public List<dbComments> Comments { get; set; }

        public UserModel ConvertToUserModel()
        {
            return new UserModel(this, true);
        }

        public UserModel ConvertToUserModelWithoutWearables()
        {
            return new UserModel(this, false);
        }
    }
}
