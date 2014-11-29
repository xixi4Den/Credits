using System.Web.Mvc;

namespace AvDB_lab4.Controllers
{
    public class ApplicationController : Controller
    {
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index", "WorkQueue");
            }
            catch
            {
                return View();
            }
        }
    }
}
