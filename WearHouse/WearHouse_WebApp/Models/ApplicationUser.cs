using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WearHouse_WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Location { get; set; }
    }
}
