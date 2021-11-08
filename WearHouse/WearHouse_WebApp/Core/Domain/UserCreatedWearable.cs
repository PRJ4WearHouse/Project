using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WearHouse_WebApp.Models.dbModels;

namespace WearHouse_WebApp.Core.Domain
{
    public class UserCreatedWearable
    {
        public int WearableId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public WearableState State { get; set; }
        public IFormFile[] ImageFiles { get; set; }

        public UserCreatedWearable() { }

        public UserCreatedWearable(string title, string description, WearableState wearableState = WearableState.Inactive)
        {
            Title = title;
            Description = description;
            State = wearableState;
        }

        public UserCreatedWearable(UserCreatedWearable wearable)
        {
            Title = wearable.Title;
            Description = wearable.Description;
            State = wearable.State;
            ImageFiles = wearable.ImageFiles;
        }
    }


    public enum WearableState
    {
        Selling,
        Renting,
        Borrowing,
        Giving,
        Inactive
    }
}
