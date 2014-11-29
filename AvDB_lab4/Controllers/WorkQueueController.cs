using System.Web.Mvc;

namespace AvDB_lab4.Controllers
{
    public class WorkQueueController : Controller
    {

        public WorkQueueController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}