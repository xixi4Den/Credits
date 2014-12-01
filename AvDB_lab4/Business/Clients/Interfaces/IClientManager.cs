using System.Collections;
using System.Collections.Generic;
using AvDB_lab4.Models;

namespace AvDB_lab4.Business.Clients.Interfaces
{
    public interface IClientManager
    {
        IList<PersonAutocompleteModel> GetPersonNamesStartsWith(string startsWith);
        IList<CompanyAutocompleteModel> GetCompanyNamesStartsWith(string startsWith);
    }
}