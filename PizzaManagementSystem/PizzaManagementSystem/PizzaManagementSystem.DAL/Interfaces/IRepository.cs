using System;
using System.Linq;
using System.Linq.Expressions;

namespace PizzaManagementSystem.DAL.Interfaces
{
    public interface IRepository<TEntity>
    {
        void Save(TEntity entity);
        TEntity Get(int ID, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetBy(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>>[] includes = null);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, object>>[] includes = null);
        void Delete(TEntity entity);
    }
}
