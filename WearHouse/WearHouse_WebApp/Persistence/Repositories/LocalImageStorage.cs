using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WearHouse_WebApp.Core.Repositories;

namespace WearHouse_WebApp.Persistence.Repositories
{
    public class LocalImageStorage : IImageStorage
    {
        private List<string> _urlList;
        private string _wwwRootPath;
        public LocalImageStorage(string wwwRootPath)
        {
            _wwwRootPath = wwwRootPath;
            _urlList = new();
        }

        public async Task<List<string>> SaveImages(int itemId, IFormFile[] images)
        {
            if (images != null)
            {
                int i = 0;
                foreach (var image in images)
                {
                    string imageName = "Img" + i++ + Path.GetExtension(image.FileName);
                    string subPath = Path.Combine(_wwwRootPath + "/Image", itemId.ToString());

                    //Check if dir exists
                    if (!Directory.Exists(subPath))
                        System.IO.Directory.CreateDirectory(subPath);

                    string imagePath = Path.Combine(subPath, imageName);
                    _urlList.Add(imagePath);

                    await using var fileStream = new FileStream(imagePath, FileMode.Create);
                    //OBS A try catch would be an excellent idea in this case run out of space etc.
                    await image.CopyToAsync(fileStream);
                }
            }

            return _urlList;
        }

        public async Task<bool> DeleteImages(int itemId, IFormFile[] images)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveProfileImage(string userId, IFormFile image)
        {
            string imagePath = null;
            if (image != null)
            {
                string imageName = "PrImg" + Path.GetExtension(image.FileName);
                string subPath = Path.Combine(_wwwRootPath + "/Image", userId);

                if (!Directory.Exists(subPath))
                    System.IO.Directory.CreateDirectory(subPath);

                imagePath = Path.Combine(subPath, imageName);

                await using var fileStream = new FileStream(imagePath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }
            return imagePath;
        }
    }
}
