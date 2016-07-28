using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using ResourceManagementSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResourceManagementSystem2.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly DepartmentService _departmentService = new DepartmentService();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Create([DataSourceRequest] DataSourceRequest request, DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Insert(department);
            }

            return Json(new[] { department }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Destroy([DataSourceRequest] DataSourceRequest request, DepartmentViewModel department)
        {
            bool success = false;
            if (ModelState.IsValid)
            {
                success = _departmentService.Delete(department);
            }
            var result = new[] { department }.ToDataSourceResult(request, ModelState);
            result.Errors = !success;
            return Json(result);
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_departmentService.GetAll().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadForDropdown()
        {
            return Json(_departmentService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update([DataSourceRequest] DataSourceRequest request, DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                _departmentService.Update(department);
            }
            return Json(new[] { department }.ToDataSourceResult(request, ModelState));
        }
    }
}