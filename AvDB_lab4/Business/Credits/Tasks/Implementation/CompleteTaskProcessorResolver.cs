using System;
using System.Collections.Generic;
using AvDB_lab4.Business.Credits.Tasks.Implementation.CompleteTaskProcessors;
using AvDB_lab4.Business.Credits.Tasks.Interfaces;
using AvDB_lab4.DataAccess.Framework;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;

namespace AvDB_lab4.Business.Credits.Tasks.Implementation
{
    public class CompleteTaskProcessorResolver: ICompleteTaskProcessorResolver
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IDictionary<ApprovalType, BaseCompleteTaskProcessor> processors;

        public CompleteTaskProcessorResolver(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            var startApprovalTaskProcessor = new StartApprovalTaskProcessor(unitOfWork);
            var finalApprovalTaskProcessor = new FinalApprovalTaskProcessor(unitOfWork);
            processors = new Dictionary
                <ApprovalType, BaseCompleteTaskProcessor>
            {
                {ApprovalType.RiskManagment, startApprovalTaskProcessor},
                {ApprovalType.CreditCommittee, startApprovalTaskProcessor},
                {ApprovalType.SecurityService, startApprovalTaskProcessor},
                {ApprovalType.Fianl, finalApprovalTaskProcessor}
            };
        }

        public BaseCompleteTaskProcessor Find(ApprovalType type)
        {
            return processors[type];
        }
    }
}