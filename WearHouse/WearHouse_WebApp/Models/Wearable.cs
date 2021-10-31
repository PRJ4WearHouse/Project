using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Username { get; set; }
        [NotMapped]
        public IFormFile[] ImageFile { get; set; }
        /*
        public uint size { get; private set; }
        public string wearableImagePath { get; private set; }
        public string brand { get; private set; }
        public string description { get; private set; }
        public string type { get; set; } = default;
        public Gender gender { get; private set; }

        public Wearable(uint size, string wearableImagePath, string brand, string description, Gender gender)
        {
            this.size = size;
            this.wearableImagePath = wearableImagePath;
            this.brand = brand;
            this.description = description;
            this.gender = gender;
        }*/
    }

    public enum Gender
    {
        Mens,
        Womens,
        Unisex
    }
}
