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
        Task<List<string>> SaveImages(int itemId, IFormFile[] images);
        //Perhaps ItemId is enough. There should be a version that only takes Id.
        Task<bool> DeleteImages(int itemId, IFormFile[] images);
    }
}