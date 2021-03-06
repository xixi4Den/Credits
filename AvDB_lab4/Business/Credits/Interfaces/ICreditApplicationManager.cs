﻿using System.Web;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Models;
using System.Collections.Generic;

namespace AvDB_lab4.Business.Credits.Interfaces
{
    public interface ICreditApplicationManager
    {
        CreditApplicationViewModel GetNewCreditApplicationViewModel(ClientGroup clientGroup);
        void SaveNewCreditApplication(CreditApplicationViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments);
        ApplicationDetailsViewModel GetApplicationDetailsViewModel(System.Guid id);
        List<ApplicationDetailsViewModel> GetApplicationListViewModel();
        RepaymentChartViewModel GetRepaymentChartViewModel(System.Guid id);
    }
}