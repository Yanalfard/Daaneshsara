using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public class MainRepo<TEntity> : IMainRepo<TEntity> where TEntity : class
    {
        private AminWebEntities _db;
        private DbSet<TEntity> _dbSet;

        public MainRepo(AminWebEntities db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual bool Delete(TEntity entity)
        {
            try
            {
                if (_db.Entry(entity).State == EntityState.Detached)
                    _dbSet.Attach(entity);
                _dbSet.Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool DeleteById(object id)
        {
            try
            {
                TEntity entity = _dbSet.Find(id);
                if (entity != null)
                    return Delete(entity);
                return false;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Update(TEntity entity)
        {
            try
            {
                _dbSet.Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includes = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (where != null)
                query = query.Where(where);

            if (orderBy != null)
                query = orderBy(query);

            if (includes != null)
                foreach (string i in includes.Split(','))
                    query = query.Include(i);

            return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Save()
        {
            _db.SaveChanges();
        }
    }
}
