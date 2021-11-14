﻿using System.ComponentModel.DataAnnotations;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.ViewModels;

namespace WearHouse_WebApp.Models.Entities
{
    public class dbWearable
    {
        //Consider changing key to random generated string.
        [Key]
        public int WearableId { get; set; }
        [StringLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrls { get; set; }
        public string UserId { get; set; }
        public string UserContactInfo { get; set; }
        public string State { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public dbWearable() { }

        public WearableModel ConvertToModel()
        {
            return new WearableModel(this);
        }
    }
}
