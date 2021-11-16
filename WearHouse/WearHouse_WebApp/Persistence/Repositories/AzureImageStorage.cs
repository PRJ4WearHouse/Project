using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            // Creating Client who controls container aka the wearable
            // eg. Blob Containers/itemid41 (server side)
            BlobContainerClient containerClient = new BlobContainerClient(_connectionString, "itemid" + itemId.ToString());

            // Creates container if not already created on the storage server
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

            // Run foreach cycling through the FormFIles given
            List<string> blobUrls = new List<string>();

            int i = 0;
            foreach (var image in imagesFiles)
            {
                // Create name of blob eg. Img12.jpg
                string imageName = "Img" + i++ + Path.GetExtension(image.FileName);
                // Upload the blob to image server
                await containerClient.UploadBlobAsync(imageName, image.OpenReadStream());
                blobUrls.Add("https://wearhouseimages.blob.core.windows.net/"+ containerClient.Uri + "/"+ imageName + "");
            }
            // Return amount of images cycled through

            return String.Join("\n", blobUrls);
        }
        
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
