using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DevOpportunity.Models;

namespace DevOpportunity.Controllers
{
    public class CustomersController : Controller
    {
        private mytmpbaseEntities db = new mytmpbaseEntities();

        // GET: Customers
        public ActionResult Index(string searchString, int page = 1)
        {
            int customersCount = 0;
            if (!String.IsNullOrEmpty(searchString))
            {
                customersCount = db.Customers.Where(a => a.CompanyName.Contains(searchString)
                                                  || a.ContactName.Contains(searchString)
                                                  || a.ContactTitle.Contains(searchString)
                                                  || a.Address.Contains(searchString)
                                                  || a.City.Contains(searchString)
                                                  || a.Region.Contains(searchString)
                                                  || a.PostalCode.Contains(searchString)
                                                  || a.Country.Contains(searchString)
                                                  || a.Phone.Contains(searchString)
                                                  || a.Fax.Contains(searchString)).Count();
            }
            else
            {
                customersCount = db.Customers.Count();
            }
            Pager pager = new Pager(customersCount, page);

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
                                                  || a.Fax.Contains(searchString)).OrderBy(a => a.CustomerID).Skip((pager.CurrentPage - 1)*pager.PageSize).Take(pager.PageSize);
            }
            else
            {
                customers = db.Customers.OrderBy(a => a.CustomerID).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);
            }            

            Pagination<Customers> pagination = new Pagination<Customers>()
            {
                Items = customers,
                Pager = pager
            };

            return View(pagination);
        }

        // GET: Customers/Details/5
        public ActionResult Details(string id, int page = 1)
        {
            Pager pager = new Pager(db.Orders.Where(a => a.CustomerID == id).Count(), page);

            ViewBag.CustomerID = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IEnumerable<Orders> orders = db.Orders.Where(a => a.CustomerID == id).OrderBy(a => a.CustomerID).Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize);
            if (orders == null)
            {
                return HttpNotFound();
            }

            Pagination<Orders> pagination = new Pagination<Orders>()
            {
                Items = orders,
                Pager = pager
            };

            return View(pagination);
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
