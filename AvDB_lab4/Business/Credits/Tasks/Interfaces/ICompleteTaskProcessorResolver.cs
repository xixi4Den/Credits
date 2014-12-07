using AvDB_lab4.Business.Credits.Tasks.Implementation.CompleteTaskProcessors;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;

namespace AvDB_lab4.Business.Credits.Tasks.Interfaces
{
    public interface ICompleteTaskProcessorResolver
    {
        BaseCompleteTaskProcessor Find(ApprovalType type);
    }
}