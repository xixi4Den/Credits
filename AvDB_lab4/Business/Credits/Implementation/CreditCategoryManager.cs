using System.Collections.Generic;
using System.Web.Mvc;
using AvDB_lab4.Business.Credits.Interfaces;
using AvDB_lab4.DataAccess.Framework;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Entities.Credits;
using AvDB_lab4.Models;
using WebGrease.Css.Extensions;

namespace AvDB_lab4.Business.Credits.Implementation
{
    public class CreditCategoryManager: ICreditCategoryManager
    {
        private IUnitOfWork unitOfWork;

        public CreditCategoryManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public CreditCategoryViewModel GetCreditCategoryDropDownListViewModel(ClientGroup clientGroup)
        {
            var creditCategories =
                unitOfWork.GetRepository<CreditCategory>().Get(x => x.ClientGroup == clientGroup);

            var availableCreditCategoryList = new List<object>();
            creditCategories.ForEach(category => availableCreditCategoryList.Add(
                new {Value = category.Id, Text = category.DisplayText})
                );
            
            var viewModel = new CreditCategoryViewModel();
            viewModel.AvailableCreditCategoryList = new SelectList(
                availableCreditCategoryList,
                "Value",
                "Text");
            return viewModel;
        }
    }
}