using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AvDB_lab4.Business.Clients.Interfaces;
using AvDB_lab4.DataAccess.Framework;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Models;

namespace AvDB_lab4.Business.Clients.Implementation
{
    public class ClientManager: IClientManager
    {
        private readonly IUnitOfWork unitOfWork;

        public ClientManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IList<PersonAutocompleteModel> GetPersonNamesStartsWith(string startsWith)
        {
            var result = unitOfWork.GetRepository<LegalPerson>()
                .Get(x => x.FirstName.StartsWith(startsWith) || x.LastName.StartsWith(startsWith))
                .Select(x => new PersonAutocompleteModel{ FullName = string.Format("{0}, {1}", x.FirstName, x.LastName), Id = x.Id})
                .ToList();

            return result;
        }

        public IList<CompanyAutocompleteModel> GetCompanyNamesStartsWith(string startsWith)
        {
            var result = unitOfWork.GetRepository<JuridicalPerson>()
                .Get(x => x.Name.StartsWith(startsWith))
                .Select(x => new CompanyAutocompleteModel{ Name = x.Name, Id = x.Id})
                .ToList();

            return result;
        }
    }
}