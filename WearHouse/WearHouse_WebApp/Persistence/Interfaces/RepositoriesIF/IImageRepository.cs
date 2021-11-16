using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WearHouse_WebApp.Core.Domain;

namespace WearHouse_WebApp.Persistence.Core.RepositoriesIF
{
    //Consider facotry pattern, and perhaps abstract class
    public interface IImageRepository
    {
        Task<List<string>> SaveImages(int itemId, IFormFile[] images);

        //Perhaps ItemId is enough. There should be a version that only takes Id.
        Task<bool> DeleteImages(int itemId);

        Task<string> SaveProfileImage(string userId, IFormFile image);

        Task<IFormFile[]> RetriveImagesByWearableId(int itemId)
    }
}
