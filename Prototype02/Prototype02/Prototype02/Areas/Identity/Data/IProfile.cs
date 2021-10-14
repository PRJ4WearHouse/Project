using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Prototype02.Models;

namespace Prototype02.Areas.Identity.Data
{
    public abstract class IProfile : IdentityUser
    {
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public List<WearablePost> posts = new List<WearablePost>();
    }
}
