using System.Web.Mvc;

namespace DevOpportunity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string searchString, int? page)
        {
            return RedirectToAction("Index", "Customers", new { searchString, page });
        }
        public ActionResult About()
        {
            ViewBag.Message = "Instructions:";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}