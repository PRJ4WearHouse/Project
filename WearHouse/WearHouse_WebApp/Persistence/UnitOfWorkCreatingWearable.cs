using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence.Interfaces;
using WearHouse_WebApp.Persistence.Repositories;

namespace WearHouse_WebApp.Persistence
{
    public class UnitOfWorkCreatingWearable : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWorkCreatingWearable(ApplicationDbContext context, string azureConnString)
        {
            _context = context;
            Wearables = new WearableRepository(_context);
            ImageStorage = new AzureImageStorage(azureConnString);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public IWearableRepository Wearables { get; }
        public AzureImageStorage ImageStorage { get; }

        public async Task<bool> SaveWearableWithImages(WearableModel wearable)
        {
            var dbEntity = wearable.ConvertToDbWearable();
            //Add model to db
            await Wearables.Add(dbEntity);

            //Save changes to generate unchangeable wearableID. 
            //OBS Should be a transaction instead!
            await Complete();

            //Get wearableID
            int itemId = dbEntity.WearableId;

            //Save images at 
            if (wearable.ImageFiles != null)
            {
                dbEntity.ImageUrls = ImageStorage.SaveImagesToWearable(wearable.ImageFiles, itemId).Result;

                //Save changes
                await Complete();
            }

            return true;
        }

        public Task<int> Complete()
        {
            return _context.SaveChangesAsync();
        }
    }
}
