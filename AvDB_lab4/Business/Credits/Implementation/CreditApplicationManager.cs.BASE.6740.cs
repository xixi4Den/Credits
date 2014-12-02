using System;
using AvDB_lab4.Business.Credits.Interfaces;
using AvDB_lab4.DataAccess.Framework;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Models;

namespace AvDB_lab4.Business.Credits.Implementation
{
    public class CreditApplicationManager : ICreditApplicationManager
    {
        private readonly ICreditCategoryManager creditCategoryManager;
        private readonly IUnitOfWork unitOfWork;

        public CreditApplicationManager(ICreditCategoryManager creditCategoryManager,
            IUnitOfWork unitOfWork)
        {
            this.creditCategoryManager = creditCategoryManager;
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

        public void SaveNewCreditApplication(CreditApplicationViewModel viewModel)
        {
            Contract.NotNull(viewModel, "CreditApplicationViewModel cannot be null");
            Contract.NotNull(viewModel.ClientGroupViewModel, "ClientGroupViewModel cannot be null");
            Contract.NotNull(viewModel.CreditCategoryViewModel, "CreditCategoryViewModel  cannot be null");
            Contract.NotEmptyGuid(viewModel.CreditCategoryViewModel.SelectedCreditCategoryId, "SelectedCreditCategoryId cannot be empty");
            Contract.NotEmptyGuid(viewModel.ClientId, "ClientId cannot be empty");

            viewModel.RegisterDate = DateTime.Now;
            viewModel.IsCompleted = false;
            viewModel.CompleteDate = null;
            viewModel.Outcome = null;
            viewModel.RejectionReason = null;

            var entity = AutoMapper.Mapper.Map<CreditApplication>(viewModel);
            unitOfWork.GetRepository<CreditApplication>().InsertOrUpdate(entity);
            unitOfWork.Commit();
        }
    }
}