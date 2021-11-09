using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WearHouse_WebApp.Models.dbModels;

namespace WearHouse_WebApp.Core.Domain
{
    public class WearableModel
    {
        public WearableState State
        {
            get
            {
                return State;
            }
            set
            {
                State = value;
                dbmodel.State = Enum.GetName(typeof(WearableState), State);
            }
        }

        public IFormFile[] ImageFiles { get; set; }

        public string Username { get; set; }

        public WearableModel() { }


        public dbWearable dbmodel { get; set; }

        public WearableModel(string title, string description, WearableState wearableState = WearableState.Inactive)
        {
            dbmodel = new dbWearable();
            dbmodel.Title = title;
            dbmodel.Description = description;
            State = wearableState;
            
        }

        public WearableModel(WearableModel wearableModel)
        {
            dbmodel = new dbWearable();
            dbmodel.Title = wearableModel.dbmodel.Title;
            dbmodel.Description = wearableModel.dbmodel.Description;
            State = wearableModel.State;
            ImageFiles = wearableModel.ImageFiles;
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
