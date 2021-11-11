using System;
using Microsoft.AspNetCore.Http;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Models.Domain
{
    public class WearableModel
    {
        private WearableState _state;
        public WearableState State
        {
            get => _state;
            set
            {
                _state = value;
                if (dbModel == null)
                    dbModel = new dbWearable(); 
                dbModel.State = Enum.GetName(typeof(WearableState), value);
            }
        }

        public IFormFile[] ImageFiles { get; set; }
        public string Username { get; set; }
        public dbWearable dbModel { get; set; }
        
        //OBS Never really used
        public WearableModel(string title, string description, WearableState wearableState = WearableState.Inactive)
        {
            dbModel = new dbWearable
            {
                Title = title,
                Description = description
            };
            State = wearableState;
        }

        //Copy constructor
        public WearableModel(WearableModel wearableModel)
        {
            dbModel = new dbWearable
            {
                Title = wearableModel.dbModel.Title,
                Description = wearableModel.dbModel.Description
            };
            State = wearableModel.State;
            ImageFiles = wearableModel.ImageFiles;
        }

        //OBS conversion. Take care of image URL's
        public WearableModel(dbWearable dbWearable)
        {
            dbModel = dbWearable;
            State = (WearableState)Enum.Parse(typeof(WearableState), dbModel.State);
        }

        //OBS So far only used in test
        public WearableModel()
        {
            State = WearableState.Inactive;
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
