using System;
using System.IO;
using System.Linq;
using System.Web;
using AvDB_lab4.Business.Credits.Interfaces;
using AvDB_lab4.Business.Credits.Tasks.Interfaces;
using AvDB_lab4.Business.Exceptions;
using AvDB_lab4.DataAccess.Framework;
using AvDB_lab4.Entities;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Models;
using System.Collections.Generic;
using Newtonsoft.Json;

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

        public void SaveNewCreditApplication(CreditApplicationViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            Contract.NotNull(viewModel, "CreditApplicationViewModel cannot be null");
            Contract.NotNull(viewModel.ClientGroupViewModel, "ClientGroupViewModel cannot be null");
            Contract.NotNull(viewModel.CreditCategoryViewModel, "CreditCategoryViewModel  cannot be null");
            Contract.NotEmptyGuid(viewModel.CreditCategoryViewModel.SelectedCreditCategoryId, "SelectedCreditCategoryId cannot be empty");
            Contract.NotEmptyGuid(viewModel.ClientId, "ClientId cannot be empty");

            if (IsCreditApplicationAlreadyExistsForClient(viewModel.ClientId))
            {
                throw new BusinessException("Application for this client already exists");
            }

            FillCreditApplicationViewModel(viewModel);

            var entity = AutoMapper.Mapper.Map<CreditApplication>(viewModel);
            unitOfWork.GetRepository<CreditApplication>().InsertOrUpdate(entity);
            unitOfWork.Commit();

            AddAttachments(entity.Id, attachments);

            taskManager.CreateTasksForNewCreditApplication(entity);
        }

        private void AddAttachments(Guid creditApplicationId, IEnumerable<HttpPostedFileBase> attachments)
        {
            foreach (var attachment in attachments)
            {
                if ((attachment != null) && (attachment.ContentLength > 0))
                {
                    var fileName = Path.GetFileName(attachment.FileName);
                    var path = Path.Combine(HttpContext.Current.Server.MapPath("~/Files"), Guid.NewGuid() + fileName);
                    attachment.SaveAs(path);
                    var attachmentEntity = new Attachment
                    {
                        ApplicationId = creditApplicationId,
                        IsContract = false,
                        Source = fileName,
                    };
                    unitOfWork.GetRepository<Attachment>().InsertOrUpdate(attachmentEntity);
                }
            }
            unitOfWork.Commit();
        }

        private bool IsCreditApplicationAlreadyExistsForClient(Guid clientId)
        {
            if (unitOfWork.GetRepository<CreditApplication>().Count(x => x.ClientId == clientId && !x.IsCompleted) > 0)
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
            var creditApplication = unitOfWork.GetRepository<CreditApplication>().Get(x => x.Id == id).SingleOrDefault();
            return GetApplicationDetails(creditApplication);
        }

        public List<ApplicationDetailsViewModel> GetApplicationListViewModel()
        {
            List<ApplicationDetailsViewModel> resultList = new List<ApplicationDetailsViewModel>();
            var creditApplicationList = unitOfWork.GetRepository<CreditApplication>().Get(x => true);
            foreach (var application in creditApplicationList)
            {
                resultList.Add(GetApplicationDetails(application));
            }
            return resultList;
        }

        public RepaymentChartViewModel GetRepaymentChartViewModel(Guid id)
        {
            RepaymentChartViewModel repaymentChartViewModel = new RepaymentChartViewModel();
            CreditApplication creditApplication = unitOfWork.GetRepository<CreditApplication>().GetById(id);
            if (creditApplication.Client.ClientGroup == ClientGroup.PrivatePerson)
            {
                LegalPerson client = unitOfWork.GetRepository<LegalPerson>().GetById(creditApplication.ClientId);
                repaymentChartViewModel.ClientName = client.FirstName + " " + client.LastName;
            }
            else
            {
                repaymentChartViewModel.ClientName = unitOfWork.GetRepository<JuridicalPerson>().GetById(creditApplication.ClientId).Name;
            }

            CreditCategory creditCategory = creditApplication.CreditCategory;
            List<string> keys = new List<string>();
            List<double> values = new List<double>();
            double percent = creditCategory.Rate;
            double money = Convert.ToDouble(creditCategory.MaxAmount);
            int months = creditCategory.Span;

            if (creditCategory.RepaymentScheme == RepaymentScheme.Annuity)
            {
                double monthlyPercent = percent / 100 / 12;
                double monthlyPayment = money * monthlyPercent / (1 - Math.Pow(1 + monthlyPercent, (-1) * months));
                for (int i = 0; i < months; i++)
                    values.Add(monthlyPayment);                
            }
            else
            {
                DateTime date = creditApplication.CompleteDate.Value;
                double fixedPayment = money / months;
                double variablePayment = 0;
                for (int i = 0; i < months; i++)
                {
                    variablePayment = money * percent / 100 * 31 / 365;
                    money -= fixedPayment;
                    values.Add(fixedPayment + variablePayment);
                }
            }
            keys = GetKeys(creditApplication.CompleteDate.Value, months);
            repaymentChartViewModel.Keys = JsonConvert.SerializeObject(keys.ToArray());
            repaymentChartViewModel.Values = JsonConvert.SerializeObject(values.ToArray());
            return repaymentChartViewModel;
        }

        private ApplicationDetailsViewModel GetApplicationDetails(CreditApplication creditApplication)
        {
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
            var contract = applicationDetailsModel.Attachments.FirstOrDefault(x => x.IsContract);
            if (contract != null)
            {
                applicationDetailsModel.Attachments.Remove(contract);
                applicationDetailsModel.Contract = contract;
            }           
            return applicationDetailsModel;
        }

        private List<string> GetKeys(DateTime date, int months)
        {
            List<string> keys = new List<string>();
            for (int i = 0; i < months; i++)
            {
                keys.Add(date.ToLongDateString());
                 date = date.AddMonths(1);
            }
            return keys;
        }
    }
}