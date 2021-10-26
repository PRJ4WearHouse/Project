using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prototype02.Areas.Identity.Data;

namespace Prototype02.Models
{
    public class Profile : IProfile
    {
        public Profile()
        {
            this.Name = "";
            this.ProfileImage = "";
            this.UserName = "";
        }

        public Profile(string name, string profileImage, string userName) {
            Name = name;
            ProfileImage = profileImage;
            UserName = userName;
        }
    }
}
