using System.Collections.Generic;
using WearHouse_WebApp.Core.Domain;
using WearHouse_WebApp.Models.ViewModels;

namespace WearHouse_WebApp.Persistence.Core.RepositoriesIF
{
    public interface IWearableRepository : IRepository<WearableViewModel>
    {
        IEnumerable<WearableModel> GetWearablesByUsername(string username);
    }
}
