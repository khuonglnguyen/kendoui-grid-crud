using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelerikMvcAppDemo.Models;
using TelerikMvcAppDemo.Models.SubModels;

namespace TelerikMvcAppDemo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View("GetData");
        }

        public static EmployeeEntities db = new EmployeeEntities();
        public JsonResult GetData([DataSourceRequest] DataSourceRequest request)
        {
            _Employee employee = new _Employee();
            var e = db.Employees.ToList();
            List<_Employee> _employees = new List<_Employee>();
            _employees.Clear();

            e.ForEach(i =>
            {
                employee = new _Employee();
                employee.EmployeeID = i.EmployeeID;
                employee.FirstName = i.FirstName;
                employee.LastName = i.LastName;
                _employees.Add(employee);
            });

            return Json(_employees.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Create_Employee([DataSourceRequest] DataSourceRequest request, Employee employee)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                if (employee != null && ModelState.IsValid)
                {
                    Employee emp = new Employee();
                    emp.EmployeeID = employee.EmployeeID;
                    emp.FirstName = employee.FirstName;
                    emp.LastName = employee.LastName;

                    db.Employees.Add(emp);
                    db.SaveChanges();
                    employee.EmployeeID = emp.EmployeeID;
                }
                return Json(new[] { employee }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Update_Employee([DataSourceRequest] DataSourceRequest request, Employee employee)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                if (employee != null && ModelState.IsValid)
                {
                    Employee emp = db.Employees.Single(x => x.EmployeeID == employee.EmployeeID);
                    emp.EmployeeID = employee.EmployeeID;
                    emp.FirstName = employee.FirstName;
                    emp.LastName = employee.LastName;
                    db.SaveChanges();
                }
                return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Delete_Employee([DataSourceRequest] DataSourceRequest request, Employee employee)
        {
            using (EmployeeEntities db = new EmployeeEntities())
            {
                if (employee != null && ModelState.IsValid)
                {
                    Employee emp = db.Employees.Single(x => x.EmployeeID == employee.EmployeeID);
                    db.Employees.Remove(emp);
                    db.SaveChanges();
                }
                return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
            }
        }
    }
}