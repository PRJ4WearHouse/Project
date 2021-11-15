using System.Threading.Tasks;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Persistence.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetUserWithWearables(string id);
    }
}
