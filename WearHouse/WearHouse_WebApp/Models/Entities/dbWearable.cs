using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using WearHouse_WebApp.Models.ViewModels;

namespace WearHouse_WebApp.Models.dbModels
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

        public dbWearable()
        {
            
        }

        //Something feels fishy. Do we need to make a conversion pattern or something?
        public dbWearable(WearableViewModel vm)
        {
            WearableId = vm.WearableId;
            Title = vm.Title;
            Description = vm.Description;
            if(vm.ImageUrlsList != null)
                ImageUrls = string.Join("\n", vm.ImageUrlsList);
            UserId = vm.UserId;
            UserContactInfo = vm.UserContactInfo;
            //OBS Should always have a state! Just for testing
            State = vm.State.ToString();
        }
    }
}
