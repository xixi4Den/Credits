using System;
using System.Web.Mvc;
using AvDB_lab4.Business.Credits.Interfaces;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Models;

namespace AvDB_lab4.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly ICreditApplicationManager creditApplicationManager;

        public ApplicationController(ICreditApplicationManager creditApplicationManager)
        {
            this.creditApplicationManager = creditApplicationManager;
        }

        public ActionResult Details(int id)
        {

            return View();
        }

        public ActionResult Create(int clientGroupId)
        {
            var viewModel = creditApplicationManager.GetNewCreditApplicationViewModel((ClientGroup)clientGroupId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult Create([Bind(Include = "ClientGroupViewModel,CreditCategoryViewModel,ClientId")] CreditApplicationViewModel viewModel)
        {
            try
            {
                if (creditApplicationManager.SaveNewCreditApplication(viewModel))
                {
                    return RedirectToAction("Index", "WorkQueue");
                }
                throw new Exception();
            }
            catch
            {
                throw new ApplicationException("Application cannot be created. Please contact your system administrator.");
            }
        }
    }
}
