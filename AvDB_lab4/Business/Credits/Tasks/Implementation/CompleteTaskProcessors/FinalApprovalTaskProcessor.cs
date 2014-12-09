using System;
using System.IO;
using System.Web;
using AvDB_lab4.Business.Credits.Tasks.Context;
using AvDB_lab4.DataAccess.Framework;
using AvDB_lab4.Entities;
using AvDB_lab4.Entities.Credits;

namespace AvDB_lab4.Business.Credits.Tasks.Implementation.CompleteTaskProcessors
{
    public class FinalApprovalTaskProcessor: BaseCompleteTaskProcessor
    {
        public FinalApprovalTaskProcessor(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        protected override void AfterComplete(CompletionTaskContext context)
        {
            if (context.ViewModel.OutcomeViewModel.SelectedOutcome == Outcome.Accept)
            {
                AcceptApplication(context);
                GenerateContract(context);
            }
            else
            {
                RejectApplication(context);
            }
        }

        private void GenerateContract(CompletionTaskContext context)
        {
            var folderPath = HttpContext.Current.Server.MapPath("~/Files");
            var prefix = Guid.NewGuid().ToString();
            var fileName = String.Join("_", prefix, "contract.pdf");
            File.Copy(Path.Combine(folderPath, "contract.pdf"), Path.Combine(folderPath, fileName));
            var attachment = new Attachment
            {
                ApplicationId = context.TaskForComplete.CreditApplicationId,
                IsContract = true,
                Source = fileName,
            };
            unitOfWork.GetRepository<Attachment>().InsertOrUpdate(attachment);
            unitOfWork.Commit();
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

        private void AcceptApplication(CompletionTaskContext context)
        {
            var application = context.TaskForComplete.CreditApplication;
            application.Outcome = Outcome.Accept;
            application.RejectionReason = context.ViewModel.RejectionReasonViewModel.SelectedRejectionReason;
            application.CompleteDate = DateTime.Now;
            application.IsCompleted = true;

            unitOfWork.GetRepository<CreditApplication>().InsertOrUpdate(application);
            unitOfWork.Commit();
        }
    }
}