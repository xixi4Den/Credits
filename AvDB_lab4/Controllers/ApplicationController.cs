using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AvDB_lab4.Business.Credits.Interfaces;
using AvDB_lab4.Business.Exceptions;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Models;
using AvDB_lab4.DataAccess.Framework;
using AvDB_lab4.Entities.Credits;

namespace AvDB_lab4.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly ICreditApplicationManager creditApplicationManager;

        public ApplicationController(ICreditApplicationManager creditApplicationManager)
        {
            this.creditApplicationManager = creditApplicationManager;
        }

        public ActionResult Details(System.Guid id)
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] != null ? TempData["ErrorMessage"].ToString() : null;
            var applicationDetailsViewModel = creditApplicationManager.GetApplicationDetailsViewModel(id);
            return View(applicationDetailsViewModel);
        }

        public ActionResult Create(int clientGroupId)
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] != null ? TempData["ErrorMessage"].ToString() : null;
            var viewModel = creditApplicationManager.GetNewCreditApplicationViewModel((ClientGroup)clientGroupId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientGroupViewModel,CreditCategoryViewModel,ClientId")] CreditApplicationViewModel viewModel, IEnumerable<HttpPostedFileBase> attachments)
        {
            try
            {
                if (!Request.IsAuthenticated || !User.IsInRole("Operator"))
                {
                    throw new BusinessException("You don't have permissions for this operation");
                }
                creditApplicationManager.SaveNewCreditApplication(viewModel, attachments);
                return RedirectToAction("List", "Application");
            }
            catch(BusinessException e)
            {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToAction("Create", "Application",
                    new {clientGroupId = (int)viewModel.ClientGroupViewModel.SelectedClientGroup});
            }
        }

        public ActionResult List()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"] != null ? TempData["ErrorMessage"].ToString() : null;
            return View(creditApplicationManager.GetApplicationListViewModel());
        }
    }
}
