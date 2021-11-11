using System;
using System.Collections.Generic;
using System.Linq.Expressions;

//Taget direkte fra Mosh Hamadani
namespace WearHouse_WebApp.Persistence.Core.RepositoriesIF
{
    public interface IRepository<TEntity, TModel> where TEntity : class
    {
        TModel Get(int id);
        IEnumerable<TModel> GetAll();
        IEnumerable<TModel> Find(Expression<Func<TEntity, bool>> predicate);
        
        TModel SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}