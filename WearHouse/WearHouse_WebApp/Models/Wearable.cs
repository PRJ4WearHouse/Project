using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace WearHouse_WebApp.Models
{
    public class Wearable
    {
        [Key]
        public int WearableId { get; set; }
        [StringLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrls { get; set; }
        public List<string> ImageUrlsList { get; set; }
        public string Username { get; set; }
        public string UserContactInfo { get; set; }
        public State WearableState { get; set; }
        [NotMapped] public IFormFile[] ImageFiles { get; set; }



        public Wearable() { }

        public Wearable(string title, string description, string imageUrls, string username, string contactInfo, State state = State.Inactive)
        {
            Title = title;
            Description = description;
            ImageUrlsList = imageUrls.Split("\n").ToList();
            Username = username;
            UserContactInfo = contactInfo;
            WearableState = state;
        }
    }

    public enum State
    {
        Selling,
        Renting,
        Borrowing,
        Giving,
        Inactive
    }
}
