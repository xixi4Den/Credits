using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AvDB_lab4.Business.Credits.Tasks.Interfaces;
using AvDB_lab4.DataAccess;
using AvDB_lab4.DataAccess.Framework;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Entities.Credits.Tasks;
using AvDB_lab4.Entities.Credits.Tasks.Approvals;
using AvDB_lab4.Models;
using Microsoft.AspNet.Identity;

namespace AvDB_lab4.Business.Credits.Tasks.Implementation
{
    public class TaskManager: ITaskManager
    {
        private IUnitOfWork unitOfWork;

        public TaskManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void CreateTasksForNewCreditApplication(CreditApplication creditApplication)
        {
            ApprovalTask task = CreateApprovalTask(creditApplication, ApprovalType.CreditCommittee,
                TaskNames.ApprovalFromCreditCommittee);
            unitOfWork.GetRepository<ApprovalTask>().InsertOrUpdate(task);
            task = CreateApprovalTask(creditApplication, ApprovalType.RiskManagment, 
                TaskNames.ApprovalFromRiskManager);
            unitOfWork.GetRepository<ApprovalTask>().InsertOrUpdate(task);
            task = CreateApprovalTask(creditApplication, ApprovalType.SecurityService,
                TaskNames.ApprovalFromSecurityService);
            unitOfWork.GetRepository<ApprovalTask>().InsertOrUpdate(task);
            unitOfWork.Commit();
        }

        public IEnumerable<TaskViewModel> GetTaskViewModelsByRoles(IList<string> roles)
        {
            var result = new List<TaskViewModel>();
            foreach (var role in roles)
            {
                result.AddRange(GetTasksByRole(role));
            }
            FillTaskViewModels(result);
            return result;
        }

        public TaskViewModel GetTaskViewModelByTaskId(Guid id)
        {
            var task = unitOfWork.GetRepository<ApprovalTask>().GetById(id);
            var viewModel = Mapper.Map<TaskViewModel>(task);
            FillTaskViewModel(viewModel);
            return viewModel;
        }

        public void AssignTaskToUser(Guid taskId, string userId)
        {
            var task = unitOfWork.GetRepository<BaseTask>().GetById(taskId);
            if (task.UserId != null)
            {
                throw new InvalidOperationException();
            }
            task.UserId = userId;
            unitOfWork.GetRepository<BaseTask>().InsertOrUpdate(task);
            unitOfWork.Commit();
        }

        private void FillTaskViewModels(List<TaskViewModel> result)
        {
            foreach (var item in result)
            {
                FillTaskViewModel(item);
            }
        }

        private void FillTaskViewModel(TaskViewModel item)
        {
            var client = unitOfWork.GetRepository<BaseClient>().GetById(item.ClientId);
            if (item.UserId != null)
            {
                using (var context = new ApplicationDbContext())
                {
                    item.AssignedTo = context.Set<ApplicationUser>().Find(item.UserId).UserName;
                }
            }
            if (client.ClientGroup == ClientGroup.PrivatePerson)
            {
                var legalPerson = client as LegalPerson;
                item.ClientName = string.Format("{0} {1}", legalPerson.FirstName, legalPerson.LastName);
            }
            else
            {
                var juridicalPerson = client as JuridicalPerson;
                item.ClientName = juridicalPerson.Name;
            }
        }

        private ApprovalTask CreateApprovalTask(CreditApplication creditApplication, ApprovalType type, string name)
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

        private IEnumerable<TaskViewModel> GetTasksByRole(string role)
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