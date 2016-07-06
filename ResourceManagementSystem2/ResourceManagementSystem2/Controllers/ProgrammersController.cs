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
           // var debug = _programmerService.GetAll().ToArray();
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
            //var debug = _programmerService.GetAll(request.Page, request.PageSize);
           
            var result = _programmerService.GetAll(request.Page, request.PageSize);

            request.Page = 0;
            var dataSourceResult = result.ToDataSourceResult(request);
            dataSourceResult.Total = _programmerService.Count();
            return Json(dataSourceResult);
        }

        public JsonResult ReadForScheduler()
        {
            //var debug = _programmerService.GetAll();
            return Json(_programmerService.GetAll(), JsonRequestBehavior.AllowGet);
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

