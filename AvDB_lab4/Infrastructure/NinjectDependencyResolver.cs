using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AvDB_lab4.Business.Clients.Implementation;
using AvDB_lab4.Business.Clients.Interfaces;
using AvDB_lab4.Business.Credits.Implementation;
using AvDB_lab4.Business.Credits.Interfaces;
using AvDB_lab4.DataAccess.Framework;
using Ninject;

namespace AvDB_lab4.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IUnitOfWork>().To<CommonUnitOfWork>();
            kernel.Bind<ICreditCategoryManager>().To<CreditCategoryManager>();
            kernel.Bind<ICreditApplicationManager>().To<CreditApplicationManager>();
            kernel.Bind<IClientManager>().To<ClientManager>();
        }
    }

}