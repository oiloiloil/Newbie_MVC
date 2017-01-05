using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Newbie_MVC.Models;
using System.Web.Security;

namespace Newbie_MVC.Controllers
{
    public class AccountController : Controller
    {

        // **************************************
        // URL: /Account/LogOn
        // **************************************

        public ActionResult LogOn() {
            resetViewMessage();
            return View();
        }


        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff() {
            ClearSession();
            FormsAuthentication.SignOut();
            return RedirectToAction( "Index", "Home" );
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
            resetViewMessage();
            AccountDb db = new AccountDb();
            UserAccountDb userAccountDb = new UserAccountDb();
            List<LogOnModel> list = db.DbConnection(model.UserName, model.Password);
            if (list.Count > 0)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, true);
                userAccountDb.ChangeLatestLoginTime(model.UserName, DateTime.Now);
                Session["UserName"] = list[0].UserName;
                Session["Role"] = list[0].Role;
                return RedirectToAction("Index", "Home");
            }
            else
                ViewBag.Message = "無此使用者";
            return View(model);
        }

        public void ClearSession()
        {
            Session["UserName"] = null;
        }

        public void resetViewMessage()
        {
            ViewBag.message = "";
        }
    }
}
