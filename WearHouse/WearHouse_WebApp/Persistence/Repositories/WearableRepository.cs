using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WearHouse_WebApp.Data;
using WearHouse_WebApp.Models.Domain;
using WearHouse_WebApp.Models.Entities;

namespace WearHouse_WebApp.Persistence.Repositories
{
    public class WearableRepository
    {
        private readonly DbContext Context;
        private readonly DbSet<dbWearable> _entities;

        public WearableRepository(DbContext context)
        {
            Context = context;
            _entities = context.Set<dbWearable>();
        }

        public ApplicationDbContext DbContext
        {
            get { return Context as ApplicationDbContext; }
        }

        public async Task Add(dbWearable entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<dbWearable> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public Task<List<WearableModel>> Find(Expression<Func<dbWearable, bool>> predicate)
        {
            return Context.Set<dbWearable>()
                .Where(predicate)
                .Select(item => item.ConvertToModel())
                .ToListAsync();
        }

        public WearableModel Get(int id)
        {
            return _entities.Find(id)?.ConvertToModel();
        }

        public Task<List<WearableModel>> GetAll()
        {
            return _entities
                .Select(item => item.ConvertToModel())
                .ToListAsync();
        }

        public void Remove(dbWearable entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(List<dbWearable> entities)
        {
            _entities.RemoveRange(entities);
        }

        public async Task<WearableModel> SingleOrDefault(Expression<Func<dbWearable, bool>> predicate)
        {
            var res = await _entities.SingleOrDefaultAsync(predicate);
            return res?.ConvertToModel();
        }

        public Task<List<WearableModel>> GetWearablesByUserId(string userId)
        {
            return DbContext.dbWearables
                .Where(w => w.UserId == userId)
                .Select(item => item.ConvertToModel())
                .ToListAsync();
        }
    }
}
