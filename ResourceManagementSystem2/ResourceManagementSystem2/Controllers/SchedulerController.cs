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
    //[Authorize]
    public sealed class SchedulerController : Controller
    {
        private readonly ProgrammerService _programmerService = new ProgrammerService();
        private readonly ProjectService _projectsService = new ProjectService();

        public SchedulerController()
        {

        }
        public JsonResult CreateProject([DataSourceRequest] DataSourceRequest request, ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                _projectsService.Insert(project, ModelState);
            }

            return Json(new[] { project }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult DestroyProject([DataSourceRequest] DataSourceRequest request, ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                _projectsService.Delete(project, ModelState);
            }

            return Json(new[] { project }.ToDataSourceResult(request, ModelState));
        }


        public ActionResult Index()
        {
            var programmers = _programmerService.GetProgrammers();
            foreach (var p in programmers)
            {
                p.Projects.ForEach(x => x.Programmers = null);
                p.Specializations.ForEach(x => x.Programmers = null);
            }
            return View(programmers);
        }

        public JsonResult ReadProjects([DataSourceRequest] DataSourceRequest request)
        {
            var a = _projectsService.GetAll();
            //return Json(_projectsService.GetAll().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
            return Json(_projectsService.GetAll().ToDataSourceResult(request));
        }

        public JsonResult UpdateProject([DataSourceRequest] DataSourceRequest request, ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                _projectsService.Update(project, ModelState);
            }

            return Json(new[] { project }.ToDataSourceResult(request, ModelState));
        }
    }
}