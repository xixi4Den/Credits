using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AvDB_lab4.Entities;

namespace AvDB_lab4.DataAccess.Framework
{
    public class CommonRepository<T> : IRepository<T> where T : BaseDbEntity
    {
        private readonly ApplicationDbContext context;

        public CommonRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public T GetById(Guid id)
        {
            return context.Set<T>().Find(id);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            params string[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var property in includeProperties)
            {
                query = query.Include(property);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return query.ToList();
        }

        public void InsertOrUpdate(T entity)
        {
            var updatedEntity = context.Entry(entity);
            if (updatedEntity == null)
            {
                context.Set<T>().Add(entity);
            }
            else
            {
                context.Set<T>().Attach(entity);
            }
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public void Delete(Guid id)
        {
            T entity = GetById(id);
            Delete(entity);
        }
    }
}