using System;
using System.Collections.Generic;
using AutoMapper;
using AvDB_lab4.Business.Credits.Tasks.Interfaces;
using AvDB_lab4.DataAccess.Framework;
using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Entities.Credits.Tasks;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;
using AvDB_lab4.Models;

namespace AvDB_lab4.Business.Credits.Tasks.Implementation
{
    public class TaskManager: ITaskManager
    {
        private IUnitOfWork unitOfWork;

        public TaskManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void CreateTaskForNewCreditApplication(CreditApplication creditApplication)
        {
            ApprovalTask task = createApprovalTask(creditApplication, ApprovalType.CreditCommittee,
                TaskNames.ApprovalFromCreditCommittee);
            unitOfWork.GetRepository<ApprovalTask>().InsertOrUpdate(task);
            task = createApprovalTask(creditApplication, ApprovalType.RiskManagment, 
                TaskNames.ApprovalFromRiskManager);
            unitOfWork.GetRepository<ApprovalTask>().InsertOrUpdate(task);
            task = createApprovalTask(creditApplication, ApprovalType.SecurityService,
                TaskNames.ApprovalFromSecurityService);
            unitOfWork.GetRepository<ApprovalTask>().InsertOrUpdate(task);
            unitOfWork.Commit();
        }

        public IEnumerable<TaskViewModel> GetTasksByRoles(IList<string> roles)
        {
            var result = new List<TaskViewModel>();
            foreach (var role in roles)
            {
                result.AddRange(getTasksByRole(role));
            }
            return result;
        }

        private ApprovalTask createApprovalTask(CreditApplication creditApplication, ApprovalType type, string name)
        {
            return new ApprovalTask
            {
                CreditApplicationId = creditApplication.Id,
                ApprovalType = type,
                DispalyText = name,
                CreateDate = DateTime.Now,
                Status = TaskStatus.New,
                CompleteDate = null,
                Outcome = null,
                RejectionReason = null,              
                UserId = null,
            };
        }

        private IEnumerable<TaskViewModel> getTasksByRole(string role)
        {
            IEnumerable<BaseTask> tasks;
            switch (role)
            {                
                case "CreditCommitteeEmployee":
                    tasks = unitOfWork.GetRepository<ApprovalTask>().Get(
                        x => x.ApprovalType == ApprovalType.CreditCommittee,
                        includeProperties: "CreditApplication");
                    return Mapper.Map<IEnumerable<TaskViewModel>>(tasks);
                case "RiskManager":
                    tasks = unitOfWork.GetRepository<ApprovalTask>().Get(
                        x => x.ApprovalType == ApprovalType.RiskManagment,
                        includeProperties: "CreditApplication");
                    return Mapper.Map<IEnumerable<TaskViewModel>>(tasks);
                case "SecurityManager":
                    tasks = unitOfWork.GetRepository<ApprovalTask>().Get(
                        x => x.ApprovalType == ApprovalType.SecurityService,
                        includeProperties: "CreditApplication");
                    return Mapper.Map<IEnumerable<TaskViewModel>>(tasks);
                case "AuthorizedParty":
                    tasks = unitOfWork.GetRepository<ApprovalTask>().Get(
                        x => x.ApprovalType == ApprovalType.Fianl,
                        includeProperties: "CreditApplication");
                    return Mapper.Map<IEnumerable<TaskViewModel>>(tasks);
                default:
                    return new List<TaskViewModel>();
            }
        } 
    }
}