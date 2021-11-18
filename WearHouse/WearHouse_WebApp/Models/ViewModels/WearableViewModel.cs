using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Models.ViewModels
{
    //Would it make sense to just inherit from dbModel?
    public class WearableViewModel
    {
        public int WearableId { get; set; }
        [StringLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }
        //Might not need username after all!
        public string Username { get; set; }
        public string UserId { get; set; }
        public string UserContactInfo { get; set; }
        public WearableState State { get; set; }
        public IFormFile[] ImageFiles { get; set; }
        public List<string> ImageUrlsList { get; set; }


        public WearableViewModel() { }

        public WearableViewModel(string title, string description, string imageUrls, string username, string contactInfo, WearableState wearableState = WearableState.Inactive)
        {
            Title = title;
            Description = description;
            ImageUrlsList = imageUrls.Split("\n").ToList();
            Username = username;
            UserContactInfo = contactInfo;
            State = wearableState;
        }

        public WearableViewModel(dbWearable dbWearable)
        {
            Title = dbWearable.Title;
            Description = dbWearable.Description;
            ImageUrlsList = dbWearable.ImageUrls.Split("\n").ToList();
            UserId = dbWearable.UserId;
            UserContactInfo = dbWearable.UserContactInfo;
            State = Enum.Parse<WearableState>(dbWearable.State);
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
