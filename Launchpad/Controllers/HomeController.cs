using IBM.Data.DB2.iSeries;
using Launchpad.Models.ItemViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOLC.ERP.Application;

namespace Launchpad.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Launchpad(string view)
        {
            if (view == "" || view== null)
            {
                ViewBag.role = "admin";
            }
            else {
                ViewBag.role = view;
            }
            SearchViewModel vm = new SearchViewModel();
            vm.Brands = GetBrands();
            ViewBag.name = Request.Cookies["SecToken"]["FullName"];
            return View(vm);
        }

        public ActionResult LaunchBDM(string a)
        {
            SearchViewModel vm = new SearchViewModel();
            vm.Brands = GetBrands();
            ViewBag.a = a.ToString();
            return View(vm);
        }

        public ActionResult LaunchCDM(string a)
        {
            SearchViewModel vm = new SearchViewModel();
            ViewBag.Brands = GetBrands();
            ViewBag.a = a.ToString();
            return View(vm);
        }
        [HttpGet]
        public ActionResult ItemRecord()
        {
            SearchViewModel vm = new SearchViewModel();
            ViewBag.test = "Get Successful";
            return PartialView("_ItemRecord",vm);
        }
        [HttpGet]
        public ActionResult ItemReport(string id)
        {
            ViewBag.currentdate = DateTime.Now.ToShortDateString();
            return View();
        }
        [HttpGet]
        public ActionResult ItemReportPartial(string id)
        {
            return View();
        }
        private IEnumerable<SelectListItem> GetBrands()
        {
            List<SelectListItem> brandlist = new List<SelectListItem>();
            Brand Brands = new Brand();
            iDB2DataReader readerPRN = null;
            //Brands.List(HttpContext.Session["SecurityKey"].ToString(), ref readerPRN);
            Brands.List(Request.Cookies["SecToken"]["SecurityKey"], ref readerPRN);

            if (readerPRN != null)
            {
                while (readerPRN.Read())
                {
                    var brand = new SelectListItem
                    {
                        Value = readerPRN["PRNNAM"].ToString(),
                        Text = readerPRN["PRNNAM"].ToString()
                    };
                    brandlist.Add(brand);
                }

            }
            return brandlist;
        }
       
    }
}