using System;
using System.Collections.Generic;
using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Models;

namespace AvDB_lab4.Business.Credits.Tasks.Interfaces
{
    public interface ITaskManager
    {
        void CreateTasksForNewCreditApplication(CreditApplication creditApplication);
        IEnumerable<TaskViewModel> GetTaskViewModelsByRoles(IList<string> roles);
        TaskViewModel GetTaskViewModelByTaskId(Guid id);
        ApprovalTaskViewModel GetApprovalTaskViewModelByTaskId(Guid id);
        void AssignTaskToUser(Guid taskId, string userId);
        void CompleteApprovalTask(ApprovalTaskViewModel viewModel);
    }
}