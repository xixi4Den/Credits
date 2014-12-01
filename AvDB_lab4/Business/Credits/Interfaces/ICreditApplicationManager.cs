using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Models;

namespace AvDB_lab4.Business.Credits.Interfaces
{
    public interface ICreditApplicationManager
    {
        CreditApplicationViewModel GetNewCreditApplicationViewModel(ClientGroup clientGroup);
        void SaveNewCreditApplication(CreditApplicationViewModel viewModel);
    }
}