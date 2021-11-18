using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence.Interfaces;

namespace WearHouse_WebApp.Persistence.Repositories
{
    //Generic data access
    public class RepositoryEfCore<TEntity> : IRepository<TEntity> where TEntity : class
    {
        //Skal bruges af specifikke rep klasse.
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _entities;

        public RepositoryEfCore(DbContext context)
        {
            Context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public async Task AddRange(List<TEntity> entities)
        {
            await _entities.AddRangeAsync(entities);
        }

        public Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>()
                .Where(predicate)
                .ToListAsync();
        }

        public TEntity Get(int id)
        {
            return _entities.Find(id);
        }

        public Task<List<TEntity>> GetAll()
        {
            return _entities.ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(List<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }
    }
}
