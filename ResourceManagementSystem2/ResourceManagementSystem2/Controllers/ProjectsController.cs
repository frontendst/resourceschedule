using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResourceManagementSystem2.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Web.Routing;

namespace ResourceManagementSystem2.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ProgrammerService _programmerService = new ProgrammerService();
        private readonly ISchedulerEventService<ProjectViewModel> _projectService = new ProjectService();

        public ActionResult Index()//говно метод
        {
            var array = _programmerService.GetProjects();
            var projectViewModels = new List<ProjectViewModel>();

            foreach(var project in array)
            {
                projectViewModels.Add(new ProjectViewModel(project));
            }
            return View(projectViewModels.ToArray());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateProject([DataSourceRequest] DataSourceRequest request, ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                _projectService.Insert(project, ModelState);
            }
            return View("Index", _projectService.GetAll());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UpdateProject([DataSourceRequest] DataSourceRequest request, ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                _projectService.Update(project, ModelState);
            }
            RouteValueDictionary routeValues = this.GridRouteValues();
            return View("Index", _projectService.GetAll());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DestroyProject([DataSourceRequest] DataSourceRequest request, ProjectViewModel project)
        {
            if (ModelState.IsValid)
            {
                _projectService.Delete(project, ModelState);
            }
            RouteValueDictionary routeValues = this.GridRouteValues();
            return RedirectToAction("Index", routeValues);
        }
    }
}
