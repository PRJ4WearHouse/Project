using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WearHouse_WebApp.Persistence.Interfaces
{
    public interface IAzureImageStorage
    {
        Task<string> SaveImagesToWearable(IFormFile[] imagesFiles, int itemId);
        Task<string> SaveProfileImageToUsername(string username, IFormFile image);
    }
}
