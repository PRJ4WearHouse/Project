using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WearHouse_WebApp.Core.Domain;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models.dbModels;
using WearHouse_WebApp.Models.ViewModels;

namespace WearHouse_WebApp.Persistence.Core.RepositoriesIF
{
    public interface IWearableRepository
    {
        List<WearableModel> GetWearablesById(string username);

    }
}
