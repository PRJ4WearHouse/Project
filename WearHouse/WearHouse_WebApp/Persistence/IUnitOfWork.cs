using System;
using System.Threading.Tasks;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence.Repositories;

namespace WearHouse_WebApp.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IWearableRepository Wearables { get; }
        AzureImageStorage ImageStorage { get; }
        Task<bool> SaveWearableWithImages(WearableModel wearableModel);
        Task<int> Complete();
    }
}
