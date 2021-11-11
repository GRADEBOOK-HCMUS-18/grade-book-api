using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ApplicationCore.Entity;

namespace ApplicationCore.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity GetById(int id);

        TEntity GetFirst(Expression<Func<TEntity, bool>> predicate);

        void Delete(TEntity entity);

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        IEnumerable<TEntity> ListAll();

        IEnumerable<TEntity> List(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
        );
    }
}