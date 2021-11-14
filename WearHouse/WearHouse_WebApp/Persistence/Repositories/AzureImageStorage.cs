using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;

namespace WearHouse_WebApp.Persistence.Repositories
{
    public class AzureImageStorage
    {
        //To save files on public server
        private string _connectionString;
        public AzureImageStorage(string connectionString) { _connectionString = connectionString; }

        public async Task<string> SaveImagesToWearable(IFormFile[] imagesFiles, int itemId)
        {
            BlobContainerClient containerClient = new BlobContainerClient(_connectionString, "itemid" + itemId.ToString());

            //OBS Consider overwriting images, if already exists
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

            int i = 0;
            foreach (var image in imagesFiles)
            {
                string imageName = "Img" + i++ + Path.GetExtension(image.FileName);
                await containerClient.UploadBlobAsync(imageName, image.OpenReadStream());
            }
            return i.ToString();
        }

        //OBS! Download image files doesn't work yet!
        
        public async Task<IFormFile[]> RetriveImagesByWearableId(int id)
        {
            BlobContainerClient containerClient = new BlobContainerClient(_connectionString, "itemid" + id.ToString());
            if (containerClient.Exists())
            {
                List<IFormFile> files = new List<IFormFile>();
                // List all blobs in the container
                await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
                {
                    var blobClient = containerClient.GetBlobClient(blobItem.Name);
                    var blobDownStream = await blobClient.DownloadStreamingAsync(); 
                    var blobResponse = blobDownStream.GetRawResponse();
                    var blobStream = blobResponse?.ContentStream;
                    if (blobStream != null)
                    {
                        files.Add(new FormFile(
                            blobStream,
                            0,
                            blobStream.Length,
                            "Name",
                            blobItem.Name)
                        );
                    }
                }
            }

            return null;
        }
        
    }
}
