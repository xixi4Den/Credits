using System;
using AvDB_lab4.Business.Credits.Interfaces;
using AvDB_lab4.Business.Credits.Tasks.Interfaces;
using AvDB_lab4.DataAccess.Framework;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Models;

namespace AvDB_lab4.Business.Credits.Implementation
{
    public class CreditApplicationManager : ICreditApplicationManager
    {
        private readonly ICreditCategoryManager creditCategoryManager;
        private readonly ITaskManager taskManager;
        private readonly IUnitOfWork unitOfWork;

        public CreditApplicationManager(ICreditCategoryManager creditCategoryManager,
            ITaskManager taskManager,
            IUnitOfWork unitOfWork)
        {
            this.creditCategoryManager = creditCategoryManager;
            this.taskManager = taskManager;
            this.unitOfWork = unitOfWork;
        }

        public CreditApplicationViewModel GetNewCreditApplicationViewModel(ClientGroup clientGroup)
        {
            var result = new CreditApplicationViewModel();
            result.ClientGroupViewModel = new ClientGroupViewModel(clientGroup);
            result.CreditCategoryViewModel = new CreditCategoryViewModel();
            result.CreditCategoryViewModel.SelectedCreditCategoryId = Guid.Empty;
            switch (clientGroup)
            {
                case ClientGroup.PrivatePerson:
                    result.CreditCategoryViewModel =
                        creditCategoryManager.GetCreditCategoryDropDownListViewModel(ClientGroup.PrivatePerson);
                    break;
                case ClientGroup.SmallBusiness:
                    result.CreditCategoryViewModel =
                        creditCategoryManager.GetCreditCategoryDropDownListViewModel(ClientGroup.SmallBusiness);
                    break;
                case ClientGroup.CorporateClient:
                    result.CreditCategoryViewModel =
                        creditCategoryManager.GetCreditCategoryDropDownListViewModel(ClientGroup.CorporateClient);
                    break;
                default:
                    throw new ArgumentException();
            }
            return result;
        }

        public bool SaveNewCreditApplication(CreditApplicationViewModel viewModel)
        {
            Contract.NotNull(viewModel, "CreditApplicationViewModel cannot be null");
            Contract.NotNull(viewModel.ClientGroupViewModel, "ClientGroupViewModel cannot be null");
            Contract.NotNull(viewModel.CreditCategoryViewModel, "CreditCategoryViewModel  cannot be null");
            Contract.NotEmptyGuid(viewModel.CreditCategoryViewModel.SelectedCreditCategoryId, "SelectedCreditCategoryId cannot be empty");
            Contract.NotEmptyGuid(viewModel.ClientId, "ClientId cannot be empty");

            if (IsCreditApplicationAlreadyExistsForClient(viewModel.ClientId))
            {
                return false;
            }

            FillCreditApplicationViewModel(viewModel);

            var entity = AutoMapper.Mapper.Map<CreditApplication>(viewModel);
            unitOfWork.GetRepository<CreditApplication>().InsertOrUpdate(entity);
            unitOfWork.Commit();

            taskManager.CreateTasksForNewCreditApplication(entity);

            return true;
        }

        private bool IsCreditApplicationAlreadyExistsForClient(Guid clientId)
        {
            if (unitOfWork.GetRepository<CreditApplication>().Count(x => x.ClientId == clientId) > 0)
            {
                return true;
            }
            return false;
        }

        private static void FillCreditApplicationViewModel(CreditApplicationViewModel viewModel)
        {
            viewModel.RegisterDate = DateTime.Now;
            viewModel.IsCompleted = false;
            viewModel.CompleteDate = null;
            viewModel.Outcome = null;
            viewModel.RejectionReason = null;
        }

        public ApplicationDetailsViewModel GetApplicationDetailsViewModel(Guid id)
        {
            var creditApplication = unitOfWork.GetRepository<CreditApplication>().GetById(id);
            ApplicationDetailsViewModel applicationDetailsModel = new ApplicationDetailsViewModel();
            if (creditApplication.Client.ClientGroup == ClientGroup.PrivatePerson)
            {
                LegalPerson client = unitOfWork.GetRepository<LegalPerson>().GetById(creditApplication.ClientId);
                applicationDetailsModel.ClientName = client.FirstName + " " + client.LastName;
            }
            else
            {
                applicationDetailsModel.ClientName = unitOfWork.GetRepository<JuridicalPerson>().GetById(creditApplication.ClientId).Name;
            }
                
            AutoMapper.Mapper.Map(creditApplication, applicationDetailsModel);
            return applicationDetailsModel;
        }
    }
}