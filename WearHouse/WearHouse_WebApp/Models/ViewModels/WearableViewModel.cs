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
        public Domain.WearableModel Wearable{get;set;}
        public string CommentToAdd { get; set; }

    }
}
