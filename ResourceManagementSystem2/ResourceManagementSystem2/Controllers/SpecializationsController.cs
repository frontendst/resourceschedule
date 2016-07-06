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
    public class SpecializationsController : Controller
    {
        private readonly SpecializationService _specializationService = new SpecializationService();

        public ActionResult Index()
        {
            return View(_specializationService.GetAll().ToArray());
        }

        public JsonResult Create([DataSourceRequest] DataSourceRequest request, SpecializationViewModel specialization)
        {
            if (ModelState.IsValid)
            {
                _specializationService.Insert(specialization);
            }

            return Json(new[] { specialization }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Destroy([DataSourceRequest] DataSourceRequest request, SpecializationViewModel specialization)
        {
            bool success = false;
            if (ModelState.IsValid)
            {
                 success = _specializationService.Delete(specialization);
            }
            var debug = new[] { specialization }.ToDataSourceResult(request, ModelState);
            debug.Errors = !success;
            return Json(debug);
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            //var a = _specializationService.GetAll();//дебаг
            return Json(_specializationService.GetAll().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadForDropdown()
        {
           // var a = _specializationService.GetAll();//дебаг
            return Json(_specializationService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update([DataSourceRequest] DataSourceRequest request, SpecializationViewModel specialization)
        {
            if (ModelState.IsValid)
            {
                _specializationService.Update(specialization);
            }
            return Json(new[] { specialization }.ToDataSourceResult(request, ModelState));
        }
    }
}