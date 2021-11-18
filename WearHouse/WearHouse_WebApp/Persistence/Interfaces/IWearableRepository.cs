using System.Collections.Generic;
using System.Threading.Tasks;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Persistence.Interfaces
{
    public interface IWearableRepository : IRepository<dbWearable>
    {
        Task<List<dbWearable>> GetWearablesByUserId(string userId);
        Task<dbWearable> GetSingleWearableWithUser(int id);
        Task<List<dbWearable>> GetAllWearablesWithUsers();
    }
}
