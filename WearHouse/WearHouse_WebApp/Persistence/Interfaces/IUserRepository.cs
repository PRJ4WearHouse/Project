using System.Threading.Tasks;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Persistence.Interfaces
{
    /// <summary>
    /// Interface getting users from database
    /// </summary>
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        /// <summary>
        /// Gets a specific user and all related wearables
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApplicationUser> GetUserWithWearables(string id);
    }
}
