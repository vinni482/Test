using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PagedList;

namespace DevOpportunity.Controllers
{
    public class CustomersController : Controller
    {
        private mytmpbaseEntities db = new mytmpbaseEntities();

        // GET: Customers
        public ActionResult Index(string searchString, int page = 1)
        {
            ViewBag.SearchString = searchString;
            IEnumerable<Customers> customers;
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = db.Customers.Where(a => a.CompanyName.Contains(searchString)
                                                  || a.ContactName.Contains(searchString)
                                                  || a.ContactTitle.Contains(searchString)
                                                  || a.Address.Contains(searchString)
                                                  || a.City.Contains(searchString)
                                                  || a.Region.Contains(searchString)
                                                  || a.PostalCode.Contains(searchString)
                                                  || a.Country.Contains(searchString)
                                                  || a.Phone.Contains(searchString)
                                                  || a.Fax.Contains(searchString));
            }
            else
            {
                customers = db.Customers;
            }

            return View(customers.OrderBy(a => a.CustomerID).ToPagedList(page, 10));
        }

        // GET: Customers/Details/5
        public ActionResult Details(string id, int page = 1)
        {
            ViewBag.CustomerID = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<Orders> orders = db.Orders.Where(a => a.CustomerID == id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders.OrderBy(a => a.CustomerID).ToPagedList(page, 10));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
