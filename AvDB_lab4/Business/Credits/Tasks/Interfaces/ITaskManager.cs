using System.Collections.Generic;
using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Models;

namespace AvDB_lab4.Business.Credits.Tasks.Interfaces
{
    public interface ITaskManager
    {
        void CreateTaskForNewCreditApplication(CreditApplication creditApplication);
        IEnumerable<TaskViewModel> GetTasksByRoles(IList<string> roles);
    }
}