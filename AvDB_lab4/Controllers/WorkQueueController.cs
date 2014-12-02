using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AvDB_lab4.Business.Credits.Tasks.Interfaces;
using AvDB_lab4.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace AvDB_lab4.Controllers
{
    public class WorkQueueController : Controller
    {
        private readonly ITaskManager taskManager;

        public WorkQueueController(ITaskManager taskManager)
        {
            this.taskManager = taskManager;
        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var roles = userManager.GetRoles(User.Identity.GetUserId());
                var viewModel = taskManager.GetTasksByRoles(roles);
                return View(viewModel);
            }
            throw new ApplicationException("You are not authenticated or have not ");
        }
    }
}