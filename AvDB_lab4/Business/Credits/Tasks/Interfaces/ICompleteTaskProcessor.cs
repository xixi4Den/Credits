using AvDB_lab4.Business.Credits.Tasks.Context;
using AvDB_lab4.DataAccess.Framework;

namespace AvDB_lab4.Business.Credits.Tasks.Interfaces
{
    public interface ICompleteTaskProcessor
    {
        void Run(CompletionTaskContext context);
    }
}