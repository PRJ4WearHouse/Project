using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WearHouse_WebApp.Repository
{
    //Consider facotry pattern, and perhaps abstract class
    public interface IImageRepository
    {
        Task<List<string>> SaveImages(string username, string itemTitle, IFormFile[] images);
        Task<bool> DeleteImages(string username, string itemTitle, IFormFile[] images);
    }
}