using ResourceManagementSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ResourceManagementSystem2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var context = new DbContext();
            ////var proj = new Project { Name = "PriceTagGod", Color = "green", StartTime = DateTime.Now, EndTime = DateTime.Now };
            //var spec = new Specialization { Name = "Front-end"};
            //var proger = new Programmer { Name = "Artem", Surname = "Moroz" };
            //context.Specializations.Add(spec);
            ////context.Projects.Add(proj);
            ////context.Specializations.Add(spec);

            ////proger.Specializations.Add(spec);
            ////proger.Projects.Add(proj);

            //context.Programmers.Add(proger);

            ////context.Programmers.Remove(proger);
            //context.SaveChanges();
            //var crt = context.Programmers.Count();
            return View();
        }
    }
}