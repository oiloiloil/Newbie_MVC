using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newbie_MVC.Models;

namespace Newbie_MVC.Controllers
{
    public class AdminAccountController : Controller
    {
        //
        // GET: /AdminAccount/
        [Authorize]
        public ActionResult Index()
        {
            @ViewBag.Message = "帳號管理";
            if (Session["UserName"] == null)
                RedirectToAction("Index", "Home");

            UserAccountModel model = getUserInfo();
            return View(model);
        }

        public UserAccountModel getUserInfo()
        {
            string user = Session["UserName"].ToString();
            UserAccountDb userAccountDb = new UserAccountDb();
            List<UserAccountModel> userList = userAccountDb.SelectWithUser(user);
            return userList[0];
        }

        public ActionResult createDropDownList()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            List<RoleModel> roleList = RoleDb.selectRole();
            foreach (RoleModel model in roleList)
            {
                dict.Add(model.role_id, model.role);
            }
            
            return null;
        }

    }
}
