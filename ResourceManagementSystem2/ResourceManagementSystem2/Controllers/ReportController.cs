using ResourceManagementSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ResourceManagementSystem2.Controllers
{
    public class ReportController : Controller
    {
        readonly private ReportService _reportService = new ReportService();

        public FileResult Index(int year = 0)
        {
            if (year == 0)
                year = DateTime.Now.Year;
            return File(_reportService.GetReport(year), "application/vnd.ms-excel", "RMS Report " + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx");
        }
    }
}