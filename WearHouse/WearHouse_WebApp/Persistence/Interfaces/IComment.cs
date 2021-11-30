using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Persistence.Interfaces
{
    public interface IComment : IRepository<Models.Entities.dbComments>
    {
        public Task<List<dbComments>> GetdbCommentsOnWearable(int wearableId);
    }
}
