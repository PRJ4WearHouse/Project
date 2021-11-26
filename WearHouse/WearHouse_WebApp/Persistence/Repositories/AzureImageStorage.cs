using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using WearHouse_WebApp.Persistence.Interfaces;

namespace WearHouse_WebApp.Persistence.Repositories
{
    public class AzureImageStorage : IAzureImageStorage
    {
        //To save files on public server
        private string _connectionString;
        public AzureImageStorage(string connectionString) { _connectionString = connectionString; }

        public async Task<string> SaveImagesToWearable(IFormFile[] imagesFiles, int itemId)
        {
            BlobContainerClient containerClient = new BlobContainerClient(_connectionString, "itemid" + itemId.ToString());

            //OBS Consider overwriting images, if already exists
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

            List<string> blobUrls = new List<string>();
            int i = 0;
            foreach (var image in imagesFiles)
            {
                string imageName = "Img" + i++ + Path.GetExtension(image.FileName);
                await containerClient.UploadBlobAsync(imageName, image.OpenReadStream());
                blobUrls.Add(containerClient.Uri + "/" + imageName);
            }
            return String.Join("\n", blobUrls);
        }

        public async Task<string> SaveProfileImageToUsername(string username, IFormFile image)
        {
            BlobContainerClient containerClient = new BlobContainerClient(_connectionString, username.ToLower());

            await containerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
            var imageName = username + Path.GetExtension(image.FileName);

            await containerClient.UploadBlobAsync(imageName, image.OpenReadStream());

            return containerClient.Uri + "/" + imageName;
        }

    }
}
