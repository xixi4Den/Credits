using System.Web.Mvc;
using AvDB_lab4.Business.Clients.Interfaces;

namespace AvDB_lab4.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientManager clientManager;

        public ClientsController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
        }

        [HttpGet]
        public JsonResult LegalPersonAutocomplete(string query)
        {
            var result = clientManager.GetPersonNamesStartsWith(query);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult JuridicalPersonAutocomplete(string query)
        {
            var result = clientManager.GetCompanyNamesStartsWith(query);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}