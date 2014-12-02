using System;
using System.Collections.Generic;
using AvDB_lab4.Entities;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;

namespace AvDB_lab4.DataAccess.Framework
{
    public class CommonUnitOfWork: IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        private IDictionary<Type, object> repositories;
        private bool isDisposed;


        public CommonUnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            repositories = new Dictionary<Type, object>();
            isDisposed = false;
            RegisterRepository(new CommonRepository<JuridicalPerson>(context));
            RegisterRepository(new CommonRepository<LegalPerson>(context));
            RegisterRepository(new CommonRepository<CreditCategory>(context));
            RegisterRepository(new CommonRepository<CreditApplication>(context));
            RegisterRepository(new CommonRepository<ApprovalTask>(context));
        }

        protected ApplicationDbContext Context
        {
            get
            {
                return context;
            }
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public virtual IRepository<T> GetRepository<T>() where T : BaseDbEntity
        {
            return (IRepository<T>)repositories[typeof(T)];
        }

        protected void RegisterRepository<T>(IRepository<T> repository) where T : BaseDbEntity
        {
            repositories[typeof(T)] = repository;
        }

        public virtual void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            DisposeContext();
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        private void DisposeContext()
        {
            if (context == null)
            {
                return;
            }

            context.Dispose();
        }
    }
}