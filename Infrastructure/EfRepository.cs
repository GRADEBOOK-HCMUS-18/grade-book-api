using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApplicationCore.Entity;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure
{
    public class EfRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _dbSet;

        public EfRepository(AppDbContext dbSet)
        {
            _dbSet = dbSet;
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Set<TEntity>().FirstOrDefault(obj => obj.Id == id);
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            if (include is null) return _dbSet.Set<TEntity>().FirstOrDefault(predicate);

            var queryable = include(_dbSet.Set<TEntity>());

            return queryable.FirstOrDefault(predicate);
        }

        public void Delete(TEntity entity)
        {
            var found = GetById(entity.Id);

            var result = _dbSet.Remove(found);

            _dbSet.SaveChanges();
        }

        public TEntity Insert(TEntity entity)
        {
            _ = _dbSet.Add(entity);
            _dbSet.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            _dbSet.Set<TEntity>().Attach(entity);
            _dbSet.Entry(entity).State = EntityState.Modified;

            _dbSet.SaveChanges();

            return entity;
        }

        public IEnumerable<TEntity> ListAll()
        {
            return _dbSet.Set<TEntity>();
        }

        public IEnumerable<TEntity> List(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")

        {
            IQueryable<TEntity> query = _dbSet.Set<TEntity>();
            if (filter is not null) query = query.Where(filter);

            var includes = includeProperties.Split(',', '|');
            if (!string.IsNullOrEmpty(includeProperties))
                query = includes
                    .Aggregate(query, (current, property) => current.Include(property));

            if (orderBy is not null) query = orderBy(query);

            return query.ToList();
        }

        public IEnumerable<TEntity> List(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _dbSet.Set<TEntity>();
            if (filter is not null) query = query.Where(filter);

            if (include is not null) query = include(query);
            if (orderBy is not null) query = orderBy(query);

            return query.ToList();
        }
    }
}