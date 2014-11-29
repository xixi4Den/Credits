using System;
using AvDB_lab4.Entities;

namespace AvDB_lab4.DataAccess.Framework
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();

        IRepository<T> GetRepository<T>() where T : BaseDbEntity;
    }
}