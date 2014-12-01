using System.Web.Mvc;
using AvDB_lab4.Business.Credits.Interfaces;
using AvDB_lab4.Entities.Clients;
using AvDB_lab4.Models;
using WebGrease;

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
        public ActionResult Create([Bind(Include = "ClientGroupViewModel,CreditCategoryViewModel,ClientId")] CreditApplicationViewModel viewModel)
        {
            try
            {
                creditApplicationManager.SaveNewCreditApplication(viewModel);
                return RedirectToAction("Index", "WorkQueue");
            }
            catch
            {
                return View();
            }
        }
    }
}
