using System.Web.Mvc;

namespace TaxiparkWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CarIndex()
        {
            return View();
        }
    }
}