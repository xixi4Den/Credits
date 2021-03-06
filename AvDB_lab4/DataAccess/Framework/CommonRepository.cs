﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AvDB_lab4.Entities;
using AvDB_lab4.Entities.Credits;

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
            
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
                context.Set<T>().Add(entity);
            }
            else
            {
                context.Set<T>().Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
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

        public int Count(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.Count();
        }
    }
}