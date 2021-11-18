using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WearHouse_WebApp.Persistence.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);

        Task AddRange(List<TEntity> entities);

        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(int id);

        Task<List<TEntity>> GetAll();

        void Remove(TEntity entity);

        void RemoveRange(List<TEntity> entities);

        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
}