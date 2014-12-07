using System;
using System.Linq;
using System.Web.UI.WebControls;
using AvDB_lab4.Business.Credits.Tasks.Context;
using AvDB_lab4.Business.Exceptions;
using AvDB_lab4.DataAccess.Framework;
using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Entities.Credits.Tasks;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;

namespace AvDB_lab4.Business.Credits.Tasks.Implementation.CompleteTaskProcessors
{
    public class StartApprovalTaskProcessor: BaseCompleteTaskProcessor
    {
        public StartApprovalTaskProcessor(IUnitOfWork unitOfWork): base(unitOfWork)
        {   }

        protected override void BeforeComplete(CompletionTaskContext context)
        {
            IsApplicationRejected(context);
            base.BeforeComplete(context);
        }

        protected override void AfterComplete(CompletionTaskContext context)
        {
            if (context.ViewModel.OutcomeViewModel.SelectedOutcome == Outcome.Accept)
            {
                if (IsThisTaskLast(context))
                {
                    CreateFinalApprovalTask(context);
                }
            }
            else
            {
                RejectUncompletedRelatedTasks(context);
                RejectApplication(context);
            }
        }

        private void RejectApplication(CompletionTaskContext context)
        {
            var application = context.TaskForComplete.CreditApplication;
            application.Outcome = Outcome.Reject;
            application.RejectionReason = context.ViewModel.RejectionReasonViewModel.SelectedRejectionReason;
            application.CompleteDate = DateTime.Now;
            application.IsCompleted = true;

            unitOfWork.GetRepository<CreditApplication>().InsertOrUpdate(application);
            unitOfWork.Commit();
        }

        private void RejectUncompletedRelatedTasks(CompletionTaskContext context)
        {
            var applicableTasks = context.RelatedTasks.Where(x => x.Status != TaskStatus.Completed);
            foreach (var task in applicableTasks)
            {
                task.CompleteDate = DateTime.Now;
                task.Outcome = Outcome.Reject;
                task.RejectionReason = RejectionReason.RejectedOtherApproval;
                task.Status = TaskStatus.Completed;

                unitOfWork.GetRepository<ApprovalTask>().InsertOrUpdate(task);
            }
            unitOfWork.Commit();
        }

        private void CreateFinalApprovalTask(CompletionTaskContext context)
        {
            var finalTask = new ApprovalTask
            {
                CreditApplicationId = context.TaskForComplete.CreditApplicationId,
                ApprovalType = ApprovalType.Fianl,
                DispalyText = TaskNames.FinalApproval,
                CreateDate = DateTime.Now,
                Status = TaskStatus.New,
                CompleteDate = null,
                Outcome = null,
                RejectionReason = null,
                UserId = null,
            };
            unitOfWork.GetRepository<ApprovalTask>().InsertOrUpdate(finalTask);
            unitOfWork.Commit();
        }

        private bool IsThisTaskLast(CompletionTaskContext context)
        {
            if (context.RelatedTasks.All(x => x.Status == TaskStatus.Completed))
            {
                return true;
            }
            return false;
        }

        private void IsApplicationRejected(CompletionTaskContext context)
        {
            if (context.TaskForComplete.CreditApplication.IsCompleted)
            {
                throw new BusinessException("This credit application has been completed");
            }
        }
    }
}