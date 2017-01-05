using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newbie_MVC.Models;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace Newbie_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeDb employeeDb = new EmployeeDb();
        //
        // GET: /Employee/
        [Authorize]
        public ActionResult Index()
        {
            @ViewBag.Message = "員工資料維護";
            List<EmployeeModel> employeeList = new List<EmployeeModel>();
            employeeList = employeeDb.select();
            ViewBag.employeeList = employeeList;
            return View();
        }

        [HttpPost]
        public ActionResult Create(EmployeeModel m)
        {
            if(m.name != null)
                employeeDb.insert(m);

            return View();
        }

        [HttpPost]
        public string Find(int id)
        {
            EmployeeModel model = employeeDb.find(id);
            var json = new JavaScriptSerializer().Serialize(model);
            return json;
        }

        [HttpPost]
        public String Edit(int id, EmployeeModel m)
        {
            employeeDb.edit(id, m);
            return null;
        }

        [HttpPost]
        public String Delete(int id)
        {
            employeeDb.delete(id);
            return null;
        }

    }
}
