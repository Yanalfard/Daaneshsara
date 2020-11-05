using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IMainRepo<TEntity> where TEntity : class
    {
        TEntity Add(TEntity entity);

         bool Delete(TEntity entity);

         bool DeleteById(object id);

         bool Update(TEntity entity);

         IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where = null,
             Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includes = null);

         TEntity GetById(object id);

         void Save();
    }
}