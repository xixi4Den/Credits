﻿using System;
using System.Collections.Generic;
using AvDB_lab4.Business.Credits.Tasks.Context;
using AvDB_lab4.Business.Credits.Tasks.Interfaces;
using AvDB_lab4.DataAccess.Framework;
using AvDB_lab4.Entities.Credits.Tasks;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;

namespace AvDB_lab4.Business.Credits.Tasks.Implementation.CompleteTaskProcessors
{
    public class BaseCompleteTaskProcessor: ICompleteTaskProcessor
    {
        protected readonly IUnitOfWork unitOfWork;

        private readonly IDictionary<ApprovalType, string> ApprovalTypeRolesMapping = new Dictionary
            <ApprovalType, string>
        {
            {ApprovalType.CreditCommittee, "CreditCommitteeEmployee"},
            {ApprovalType.RiskManagment, "RiskManager"},
            {ApprovalType.SecurityService, "SecurityManager"},
            {ApprovalType.Fianl, "AuthorizedParty"},
        };

        public BaseCompleteTaskProcessor(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        protected virtual void BeforeComplete(CompletionTaskContext context)
        {
            CheckPermissions(context);
            CheckAssignment(context);
            CheckCompletion(context);
        }

        protected virtual void Complete(CompletionTaskContext context)
        {
            var task = context.TaskForComplete;
            task.Outcome = context.ViewModel.OutcomeViewModel.SelectedOutcome;
            task.RejectionReason = context.ViewModel.RejectionReasonViewModel.SelectedRejectionReason;
            task.Status = TaskStatus.Completed;
            task.CompleteDate = DateTime.Now;
            unitOfWork.GetRepository<ApprovalTask>().InsertOrUpdate(task);
            unitOfWork.Commit();
        }

        protected virtual void AfterComplete(CompletionTaskContext context)
        {
            
        }

        public void Run(CompletionTaskContext context)
        {
            BeforeComplete(context);
            Complete(context);
            AfterComplete(context);
        }

        private bool CheckPermissions(CompletionTaskContext context)
        {
            var expectedRole = ApprovalTypeRolesMapping[context.TaskForComplete.ApprovalType];
            if (context.ViewModel.UserRoles.Contains(expectedRole))
            {
                return true;
            }
            return false;
        }

        private bool CheckAssignment(CompletionTaskContext context)
        {
            if (context.TaskForComplete.UserId == context.ViewModel.UserId)
            {
                return true;
            }
            return false;
        }

        private bool CheckCompletion(CompletionTaskContext context)
        {
            if (context.TaskForComplete.Status != TaskStatus.Completed)
            {
                return true;
            }
            return false;
        }
    }
}