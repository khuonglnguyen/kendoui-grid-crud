using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Web.Mvc;
using TelerikMvcAppDemo.App_Start;
using TelerikMvcAppDemo.Models;
using TelerikMvcAppDemo.Repositories;

namespace TelerikMvcAppDemo.Controllers
{
    [AuthorizationFilter]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View("GetData");
        }
        public JsonResult GetData([DataSourceRequest] DataSourceRequest request)
        {
            var emp = _unitOfWork.Employees.GetAll();
            return Json(emp.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Create([DataSourceRequest] DataSourceRequest request, Employee employee)
        {
            if (employee != null && ModelState.IsValid)
            {
                _unitOfWork.Employees.Add(employee);
                _unitOfWork.Complete();
            }
            return Json(new[] { employee }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Update([DataSourceRequest] DataSourceRequest request, Employee employee)
        {
            if (employee != null && ModelState.IsValid)
            {
                Employee emp = _unitOfWork.Employees.GetById(employee.EmployeeID);
                emp.Name = employee.Name;
                emp.Email = employee.Email;
                emp.Address = employee.Address;
                emp.Phone = employee.Phone;

                _unitOfWork.Complete();
            }
            return Json(ModelState.IsValid ? true : ModelState.ToDataSourceResult());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Delete([DataSourceRequest] DataSourceRequest request, Employee employee)
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