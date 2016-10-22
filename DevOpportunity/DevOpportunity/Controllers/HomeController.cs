using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DevOpportunity.Controllers
{
    public class HomeController : Controller
    {
        mytmpbaseEntities context = new mytmpbaseEntities();

        public ActionResult Index()
        {
            IEnumerable<Customers> customers = context.Customers;
            return View(customers);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Orders> orders = context.Orders.Where(a=>a.CustomerID == id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }
    }
}