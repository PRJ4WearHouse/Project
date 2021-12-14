using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Persistence.Interfaces
{
    /// <summary>
    /// Interface for comments management
    /// </summary>
    public interface IComment : IRepository<dbComments>
    {
        /// <summary>
        /// Gets the comments on a wearable in its database form
        /// </summary>
        /// <param name="wearableId"></param>
        /// <returns></returns>
        public Task<List<dbComments>> GetdbCommentsOnWearable(int wearableId);
    }
}
