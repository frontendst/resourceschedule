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
    public class WeekendsController : Controller
    {
        private readonly WeekendService _weekendService = new WeekendService();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Create([DataSourceRequest] DataSourceRequest request, WeekendViewModel weekend)
        {
            bool success = false;

            if (ModelState.IsValid)
            {
                success = _weekendService.Insert(weekend);
            }
            var result = new[] { weekend }.ToDataSourceResult(request, ModelState);
            result.Errors = !success;
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Destroy([DataSourceRequest] DataSourceRequest request, WeekendViewModel weekend)
        {
            if (ModelState.IsValid)
            {
                _weekendService.Delete(weekend);
            }
            return Json(new[] { weekend }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_weekendService.GetAll().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReadForDrawing()
        {
            return Json(_weekendService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update([DataSourceRequest] DataSourceRequest request, WeekendViewModel weekend)
        {
            if (ModelState.IsValid)
            {
                _weekendService.Update(weekend);
            }
            return Json(new[] { weekend }.ToDataSourceResult(request, ModelState));
        }

    }
}
