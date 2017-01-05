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
            List<LogOnModel> list = db.SelectWithUserAndPass(model.UserName, model.Password);
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

        public void setAuth(string user)
        {
            string userData = "ApplicationSpecific data for this user";
            string userName = user;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, 
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                false,
                userData,
                FormsAuthentication.FormsCookiePath);
            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);
            // Create the cookie.
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

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
