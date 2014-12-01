using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AvDB_lab4.Entities;

namespace AvDB_lab4.DataAccess.Framework
{
    public interface IRepository<T> where T : BaseDbEntity
    {
        T GetById(Guid id);

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params string[] includeProperties);

        void InsertOrUpdate(T entity);

        void Delete(T entity);

        void Delete(Guid id);
    }
}