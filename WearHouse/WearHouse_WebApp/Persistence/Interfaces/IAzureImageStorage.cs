using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WearHouse_WebApp.Persistence.Interfaces
{
    /// <summary>
    /// Interface for image storage
    /// </summary>
    public interface IAzureImageStorage
    {
        /// <summary>
        /// Saves a an image in the image database with an item id as reference
        /// </summary>
        /// <param name="imagesFiles"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<string> SaveImagesToWearable(IFormFile[] imagesFiles, int itemId);

        /// <summary>
        /// Saves an image to the image database with a username as reference
        /// </summary>
        /// <param name="username"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        Task<string> SaveProfileImageToUsername(string username, IFormFile image);
    }
}
