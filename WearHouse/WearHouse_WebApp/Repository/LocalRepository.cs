using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace WearHouse_WebApp.Repository
{
    public class LocalRepository : IImageRepository
    {
        private List<string> _urlList;
        private string _wwwRootPath;
        public LocalRepository(string wwwRootPath)
        {
            _wwwRootPath = wwwRootPath;
            _urlList = new();
        }

        public async Task<List<string>> SaveImages(string UserName, string itemTitle, IFormFile[] images)
        {
            if (images != null)
            {
                int i = 0;
                foreach (var image in images)
                {
                    string imageName = "Img" + i++ + Path.GetExtension(image.FileName);
                    string subPath = Path.Combine(_wwwRootPath + "/Image", UserName, itemTitle);

                    //Check if dir exists
                    if (!Directory.Exists(subPath))
                        System.IO.Directory.CreateDirectory(subPath);

                    string imagePath = Path.Combine(subPath, imageName);
                    _urlList.Add(imagePath);

                    await using var fileStream = new FileStream(imagePath, FileMode.Create);
                    //OBS A try catch would be an excellent idea in this case
                    await image.CopyToAsync(fileStream);
                }
            }

            return _urlList;
        }

        public async Task<bool> DeleteImages(string UserName, string itemTitle, IFormFile[] images)
        {
            throw new NotImplementedException();
        }
    }
}
