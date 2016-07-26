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
    public class ProgrammersController : Controller
    {

        private readonly ProgrammerService _programmerService = new ProgrammerService();

        public ActionResult Index()
        {
            return View(_programmerService.GetAll().ToArray());
        }

        public JsonResult Create([DataSourceRequest] DataSourceRequest request, ProgrammerViewModel programmer)
        {
            if (ModelState.IsValid)
            {
                _programmerService.Insert(programmer);
            }

            return Json(new[] { programmer }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Destroy([DataSourceRequest] DataSourceRequest request, ProgrammerViewModel programmer)
        {
            if (ModelState.IsValid)
            {
                _programmerService.Delete(programmer);
            }

            return Json(new[] { programmer }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var result = _programmerService.GetAll(request.Page, request.PageSize);
            request.Page = 0;
            var dataSourceResult = result.ToDataSourceResult(request);
            dataSourceResult.Total = _programmerService.Count();
            return Json(dataSourceResult);
        }

        public JsonResult ReadForScheduler(int? year, int? month)
        {
            if(year == null || month == null)
            {
                year = DateTime.Now.Year;
                month = DateTime.Now.Month;
            }
            var d = _programmerService.ExcludeDeleted(_programmerService.GetAll(), new DateTime(year.Value, month.Value, 1));
            return Json(_programmerService.ExcludeDeleted(_programmerService.GetAll(), new DateTime(year.Value, month.Value, 1)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(ProgrammerViewModel programmer)
        { 
            if (ModelState.IsValid)
            {
                _programmerService.Update(programmer);
            }
            return Json( programmer, JsonRequestBehavior.AllowGet);
        }


    }
}

