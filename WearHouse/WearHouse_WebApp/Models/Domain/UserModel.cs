using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Models.Domain
{
    public class UserModel
    {
        public UserModel()
        { }
        public UserModel(ApplicationUser applicationUser, bool WithWearables)
        {
            ProfileImageUrl = applicationUser.ProfileImageUrl;
            Username = applicationUser.UserName;
            UserId = applicationUser.Id;
            Address = null;
            FirstName = applicationUser.FirstName;
            LastName = applicationUser.LastName;
            ContactInfo = applicationUser.Email;
            Wearables = (WithWearables && applicationUser.Wearables != null)
                ? applicationUser.Wearables.Select(item => item.ConvertToWearableModel()).ToList()
                : null;

        }



        public string Username { get; set; }
        public string UserId { get; set; }
        public string Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IFormFile ProfileImage { get; set; }
        public string ProfileImageUrl { get; set; }
        public List<WearableModel> Wearables { get; set; }
        public string ContactInfo { get; set; }
    }
}
