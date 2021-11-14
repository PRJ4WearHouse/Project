using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence.Interfaces;

namespace WearHouse_WebApp.Persistence.Repositories
{
    public class WearableRepository : RepositoryEfCore<dbWearable>, IWearableRepository
    {
        private readonly DbSet<dbWearable> _entities;

        public WearableRepository(DbContext context) : base(context)
        {
            _entities = context.Set<dbWearable>();
        }

        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        
        public Task<List<dbWearable>> GetWearablesByUserId(string userId)
        {
            return DbContext.dbWearables.Where(w => w.UserId == userId).ToListAsync();
        }
    }
}
