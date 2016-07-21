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
        private readonly ProjectService _projectService = new ProjectService();

        public ActionResult Index()
        {
            return View(_projectService.GetAll().ToArray());
        }
    }
}