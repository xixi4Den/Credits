﻿using System;
using System.Web;
using System.Web.Mvc;
using AvDB_lab4.Business.Credits.Tasks.Interfaces;
using AvDB_lab4.Models;
using Microsoft.AspNet.Identity;
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
                var viewModel = taskManager.GetTaskViewModelsByRoles(roles);
                return View(viewModel);
            }
            throw new ApplicationException("You are not authenticated or have not ");
        }

        public ActionResult TaskDetails(Guid taskId)
        {
            var viewModel = taskManager.GetTaskViewModelByTaskId(taskId);
            return View(viewModel);
        }

        public ActionResult AssignToMe(Guid taskId)
        {
            var userId = User.Identity.GetUserId();
            taskManager.AssignTaskToUser(taskId, userId);
            return RedirectToAction("TaskDetails", new { taskId = taskId });
        }

        public ActionResult Work(Guid id)
        {
            //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);    HttpNotFound();
            var viewModel = taskManager.GetApprovalTaskViewModelByTaskId(id);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Work([Bind(Include = "Id, OutcomeViewModel, RejectionReasonViewModel")] ApprovalTaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                viewModel.UserRoles = userManager.GetRoles(User.Identity.GetUserId());
                viewModel.UserId = User.Identity.GetUserId();
                taskManager.CompleteApprovalTask(viewModel);
            }
            return RedirectToAction("Index");
        }
    }
}