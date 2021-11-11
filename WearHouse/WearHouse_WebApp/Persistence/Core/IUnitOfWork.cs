using System;
using System.Threading.Tasks;
using WearHouse_WebApp.Persistence.Repositories;

namespace WearHouse_WebApp.Persistence.Core
{
    public interface IUnitOfWork : IDisposable
    {
        WearableRepository Wearables { get; }
        AzureImageStorage ImageStorage { get; }
        Task<int> Complete();
    }
}
