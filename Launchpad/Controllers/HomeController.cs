using IBM.Data.DB2.iSeries;
using Launchpad.Models;
using Launchpad.Models.HomeViewModels;
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
            LaunchpadViewModel vm = new LaunchpadViewModel
            {
                Brands = GetBrands()
            };
            ViewBag.name = Request.Cookies["SecToken"]["FullName"];
            return View(vm);
        }

        public ActionResult LaunchBDM(string a)
        {
            BDMViewModel vm = new BDMViewModel
            {
                Brands = GetBrands()
            };
            ViewBag.a = a.ToString();
            return View(vm);
        }
        //Search Brand Items
        [HttpPost]
        public ActionResult BrandItems(BDMViewModel vm)
        {
            var brandstring = vm.Searchbrand;
            List<string> keywords = new List<string>(brandstring.Split(' '));
            List<string> descKeywords = new List<string>();
            foreach (string str in keywords)
            {
                if (str != "")
                {
                    descKeywords.Add(str);
                }
            }
            ItemSearch searchquery = new ItemSearch
            {
                UPC = "",
                Description = descKeywords
                
            };
            BDMViewModel newvm = new BDMViewModel
            {
                Items = GetItems(searchquery)
            };
            return PartialView("_LaunchBDMStep1Table", newvm);
        }
        private IEnumerable<BrandItem> GetItems(ItemSearch newsearch)
        {
            iDB2DataReader readerITM = null;
            Item item = new Item();
            List<BrandItem> itemlist = new List<BrandItem>();

            item.List(Request.Cookies["SecToken"]["SecurityKey"], newsearch, ref readerITM);
            if (readerITM != null)
            {
                while (readerITM.Read())
                {
                    var newitem = new BrandItem
                    {
                        ItemID = readerITM["ITMITM"].ToString(),
                        Size = readerITM["ITMDSS"].ToString(),
                        ItemDescEng = readerITM["ITMEED"].ToString()
                    };
                    itemlist.Add(newitem);
                }

            }

            return itemlist;
        }
        public ActionResult LaunchCDM(string a)
        {
            CDMViewModel vm = new CDMViewModel
            {
                Brands = GetBrands()
            };
            ViewBag.a = a.ToString();
            return View(vm);
        }
        [HttpGet]
        public ActionResult ItemRecord()
        {
            LaunchpadViewModel vm = new LaunchpadViewModel();
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