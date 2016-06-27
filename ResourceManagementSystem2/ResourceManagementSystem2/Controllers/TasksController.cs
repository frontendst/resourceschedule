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
    public class TasksController : Controller
    {
        private readonly ProgrammerService _programmerService = new ProgrammerService();
        private readonly TaskService _taskService = new TaskService();

        public JsonResult Create([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                _taskService.Insert(task, ModelState);
            }
            
            return Json(new[] { task }.ToDataSourceResult(request, ModelState), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Destroy([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                _taskService.Delete(task, ModelState);
            }

            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult Index()
        {
            var programmers = _programmerService.GetProgramerEntities();
            return View(programmers);
        }

        public JsonResult Read([DataSourceRequest] DataSourceRequest request)
        {
            var deb = _taskService.GetAll();
            return Json(_taskService.GetAll().ToDataSourceResult(request));
        }

        public JsonResult GetTaskColors()
        {
            var deb = _taskService.GetAll();
            return Json(_taskService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update([DataSourceRequest] DataSourceRequest request, TaskViewModel task)
        {
            if (ModelState.IsValid)
            {
                _taskService.Update(task, ModelState);
            }
            return Json(new[] { task }.ToDataSourceResult(request, ModelState));
        }
    }
}