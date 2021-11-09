using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WearHouse_WebApp.Core.Repositories
{
    //Consider facotry pattern, and perhaps abstract class
    public interface IImageStorage
    {
        Task<List<string>> SaveImages(int itemId, IFormFile[] images);
        //Perhaps ItemId is enough. There should be a version that only takes Id.
        Task<bool> DeleteImages(int itemId, IFormFile[] images);
    }
}