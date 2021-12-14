using System.Collections.Generic;
using System.Threading.Tasks;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Persistence.Interfaces
{
    /// <summary>
    /// Interface for getting wearables
    /// </summary>
    public interface IWearableRepository : IRepository<dbWearable>
    {
        /// <summary>
        /// Gets all wearables related to a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<dbWearable>> GetWearablesByUserId(string userId);
        /// <summary>
        /// Gets a specific wearable and its user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<dbWearable> GetSingleWearableWithUser(int id);
        /// <summary>
        /// Gets a specific wearable and its comments
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<dbWearable> GetWearableWithComments(int id);
        /// <summary>
        /// Gets all wearables with their user
        /// </summary>
        /// <returns></returns>
        Task<List<dbWearable>> GetAllWearablesWithUsers();
    }
}
