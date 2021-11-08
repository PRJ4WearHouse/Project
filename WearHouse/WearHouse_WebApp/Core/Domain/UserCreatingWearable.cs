using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WearHouse_WebApp.Core.Domain
{
    public class UserCreatingWearable
    {
        public string Username { get; set; }
        public List<UserCreatedWearable> Wearables { get; set; }
    }
}
