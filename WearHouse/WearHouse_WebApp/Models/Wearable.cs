using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WearHouse_WebApp.Models
{
    public class Wearable
    {
        [Key]
        public int WearableId { get; set; }
        [StringLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> ImageUrls { get; set; }
        public string Username { get; set; }

        public Wearable() { }

        public Wearable(string title, string description, string imageUrls, string username)
        {
            Title = title;
            Description = description;
            ImageUrls = imageUrls.Split("\n").ToList();
            Username = username;
        }
    }

    public enum Gender
    {
        Mens,
        Womens,
        Unisex
    }
}
