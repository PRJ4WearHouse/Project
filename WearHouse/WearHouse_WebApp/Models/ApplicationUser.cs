using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearHouse_WebApp.Models.dbModels;

namespace WearHouse_WebApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Location { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public List<dbWearable> Wearables { get; set; }
    }
}
