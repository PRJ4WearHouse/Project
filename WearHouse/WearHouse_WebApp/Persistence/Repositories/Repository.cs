using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WearHouse_WebApp.Models.Entities;
using WearHouse_WebApp.Persistence.Core.RepositoriesIF;

namespace WearHouse_WebApp.Persistence.Repositories
{
    //Generic data access
    public class Repository<TEntity, TModel> : IRepository<TEntity, TModel> where TEntity : IMap
    {
        //Skal bruges af specifikke rep klasse.
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _entities;

        public Repository(DbContext context)
        {
            Context = context;
            _entities = context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            foreach (var map in _entities)
            {
                Console.WriteLine("Jeg vil gerne have noget mere.");
            }
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public IEnumerable<TModel> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public TModel Get(int id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<TModel> GetAll()
        {
            return _entities.ToList();
        }

        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public TModel SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }
    }
}
