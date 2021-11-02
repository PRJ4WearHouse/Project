using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace WearHouse_WebApp.Models.dbModels
{
    public class dbWearable
    {
        [Key]
        public int WearableId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrls { get; set; }
        public string UserId { get; set; }
        public string UserContactInfo { get; set; }
        public string State { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public string ApplicationUserId { get; set; }
    }
}
