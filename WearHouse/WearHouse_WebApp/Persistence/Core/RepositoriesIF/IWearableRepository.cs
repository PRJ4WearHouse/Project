using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearHouse_WebApp.Core.Domain;
using WearHouse_WebApp.Models.ViewModels;

namespace WearHouse_WebApp.Core.Repositories
{
    public interface IWearableRepository : IRepository<WearableViewModel>
    {
        IEnumerable<WearableModel> GetWearablesByUsername(string username);
    }
}
