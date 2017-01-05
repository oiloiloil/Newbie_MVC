using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newbie_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        [Authorize]
        public ActionResult Index()
        {
            @ViewBag.Message = "員工資料維護";
            return View();
        }

    }
}
