using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IBM.Data.DB2.iSeries;
using Launchpad.Models.AccountViewModels;
using TOLC.ERP.Application;

namespace Launchpad.Controllers
{
    public class AccountController : Controller
    {
        public object Formathentication { get; private set; }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {

            LoginViewModel model = new LoginViewModel();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                ReturnValue rv = new ReturnValue();
                Session session = null;
                rv = new Security().Logon(model.Username, model.Password, ref session);
                if (session.securityIdentifier == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "" });
                }
                else
                {
                    HttpCookie userCookie = new HttpCookie("SecToken");
                    userCookie["FullName"] = session.FullName;
                    userCookie["Email"] = session.EmailAddress;
                    userCookie["Username"] = session.Username;
                    userCookie["SecurityKey"] = session.securityIdentifier;
                    userCookie.Expires.AddHours(1);
                    Response.SetCookie(userCookie);
                    FormsAuthentication.SetAuthCookie(userCookie["SecurityKey"], true);
                    return RedirectToAction("Launchpad", "Home"); ;
                }
            }
            catch (InvalidCastException e)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }
        }
    }
}