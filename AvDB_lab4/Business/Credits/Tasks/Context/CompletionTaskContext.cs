using System.Collections.Generic;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;
using AvDB_lab4.Models;

namespace AvDB_lab4.Business.Credits.Tasks.Context
{
    public class CompletionTaskContext
    {
        public ApprovalTaskViewModel ViewModel { get; set; }
        public ApprovalTask TaskForComplete { get; set; }
        public IEnumerable<ApprovalTask> RelatedTasks { get; set; }
    }
}