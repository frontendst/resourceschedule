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
    [Authorize]
    public sealed class SchedulerController : Controller
    {
        private readonly ProgrammerService _programmerService = new ProgrammerService();
        private readonly ProjectService _projectsService = new ProjectService();

        public JsonResult CreateProject([DataSourceRequest] DataSourceRequest request, ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                _projectsService.Insert(project);
            }

            return Json(new[] { project }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult DestroyProject([DataSourceRequest] DataSourceRequest request, ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                _projectsService.Delete(project);
            }

            return Json(new[] { project }.ToDataSourceResult(request));
        }

        public ActionResult Index()
        {

            var programmers = _programmerService.GetProgramerEntities();
            foreach (var p in programmers)
            {
                p.Specialization = null;

                p.Tasks = null;

            }
            //var debug = programmers.ToArray();
            return View(programmers.ToArray());
        }

        public JsonResult ReadProjects([DataSourceRequest] DataSourceRequest request)
        {
            return Json(_projectsService.GetAll().ToDataSourceResult(request));
        }

        public JsonResult GetProjectColors()
        {
            //var deb = _projectsService.GetAll();
            return Json(_projectsService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateProject([DataSourceRequest] DataSourceRequest request, ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                _projectsService.Update(project);
            }

            return Json(new[] { project }.ToDataSourceResult(request, ModelState));
        }
    }
}