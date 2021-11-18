﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Models.Domain
{
    public class WearableModel
    {
        public WearableState State { get; set; }
        [Required]
        public int ID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public UserModel Owner { get; set; }
        public IFormFile[] ImageFiles { get; set; }
        public List<string> ImageUrls { get; set; }

        //OBS Slet mig!
        public dbWearable dbModel { get; set; }
        
        public WearableModel(dbWearable dbWearable, bool withOwner)
        {
            Title = dbWearable.Title;
            Description = dbWearable.Description;
            ID = dbWearable.WearableId;
            State = (WearableState)Enum.Parse(typeof(WearableState), dbWearable.State);
            if(withOwner)
                Owner = (dbWearable.ApplicationUser != null)
                    ? dbWearable.ApplicationUser.ConvertToUserModelWithoutWearables()
                    : null ;
            if (dbWearable.ImageUrls != null)
                ImageUrls = dbWearable.ImageUrls.Split("\n").ToList();

            //OBS Slet også mig!
            dbModel = dbWearable;
        }

        //Can only convert, if user is set. (User is Primary key)
        //OBS This check might need to be reserved in Repo class
        public dbWearable ConvertToDbWearable()
        {
            if(Owner != null)
                return new dbWearable()
                {
                    Description = this.Description,
                    Title = this.Title,
                    State = Enum.GetName(typeof(WearableState), this.State),
                    UserContactInfo = this.Owner.ContactInfo,
                    UserId = Owner.UserId,
                };
            else
            {
                throw new Exception("No owner defined");
            }
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
