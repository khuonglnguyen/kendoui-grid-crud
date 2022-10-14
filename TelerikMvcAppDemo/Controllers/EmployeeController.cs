using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelerikMvcAppDemo.Models;
using TelerikMvcAppDemo.Models.SubModels;
using TelerikMvcAppDemo.Repositories;

namespace TelerikMvcAppDemo.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Employee
        public ActionResult Index()
        {
            return View("GetData");
        }

        public static EmployeeEntities db = new EmployeeEntities();

        public JsonResult GetData([DataSourceRequest] DataSourceRequest request)
        {
            var emp = _unitOfWork.Employees.GetAll();
            return Json(emp.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Create_Employee([DataSourceRequest] DataSourceRequest request, Employee employee)
        {
            if (employee != null && ModelState.IsValid)
            {
                Employee emp = new Employee();
                emp.EmployeeID = employee.EmployeeID;
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;

                _unitOfWork.Employees.Add(emp);
                _unitOfWork.Complete();
            }
            return Json(new[] { employee }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Update_Employee([DataSourceRequest] DataSourceRequest request, Employee employee)
        {
            if (employee != null && ModelState.IsValid)
            {
                Employee emp = _unitOfWork.Employees.GetById(employee.EmployeeID);
                emp.EmployeeID = employee.EmployeeID;
                emp.FirstName = employee.FirstName;
                emp.LastName = employee.LastName;

                _unitOfWork.Complete();
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Delete_Employee([DataSourceRequest] DataSourceRequest request, Employee employee)
        {
            if (employee != null && ModelState.IsValid)
            {
                Employee emp = _unitOfWork.Employees.GetById(employee.EmployeeID);

                _unitOfWork.Employees.Remove(emp);
                _unitOfWork.Complete();
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
    }
}