using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System.IO;


namespace ResourceManagementSystem2.Models
{
    public class ReportService
    {
        private readonly ProgrammerService _programmerService = new ProgrammerService();
        private readonly ProjectService _projectService = new ProjectService();


        private ExcelPackage pck;

        public ReportService()
        {
            pck = new ExcelPackage();
        }

        public byte[] GetReport(int year)
        {

            SetFormat(year);
            WriteProgrammers(year);
            WriteProjects(year);
            //WriteProjectsAmount(year);
            MemoryStream mem = new MemoryStream();
            pck.SaveAs(mem);
            return mem.ToArray();
        }

        private void SetFormat(int year = 0, int startMonth = 1, int endMonth = 12)
        {
            if (year == 0)
                year = DateTime.Now.Year;

            pck.Workbook.Worksheets.Add("Сводная");
            DateTime date = new DateTime(year, 1, 1);
            for (int i = 1; i <= 12; i++)
            {
                pck.Workbook.Worksheets.Add(new DateTime(year, i, 1).ToString("MMMM"));
            }

            for (int i = startMonth + 1; i <= endMonth + 1; i++)
            {
                pck.Workbook.Worksheets[i].Cells["A1"].Value = "Отдел";
                pck.Workbook.Worksheets[i].Cells["B1"].Value = "ФИО";
                pck.Workbook.Worksheets[i].Cells["C1"].Value = "Кол-во проектов";
                pck.Workbook.Worksheets[i].Cells["D1"].Value = "Загрузка";
                pck.Workbook.Worksheets[i].Cells["E1"].Value = "PM";
                pck.Workbook.Worksheets[i].Cells["F1"].Value = "Отпуск";
                pck.Workbook.Worksheets[i].Cells["A1:F1"].Style.Font.Bold = true;
                pck.Workbook.Worksheets[i].Cells["A1:F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                pck.Workbook.Worksheets[i].Cells["A1:F1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                pck.Workbook.Worksheets[i].Row(1).Height = 25;
                pck.Workbook.Worksheets[i].Cells.AutoFitColumns();
            }
        }

        private void WriteProgrammers(int year = 0, int startMonth = 1, int endMonth = 12)
        {
            if (year == 0)
                year = DateTime.Now.Year;

            var programmers = _programmerService.GetAll();
            for (int i = startMonth + 1; i <= endMonth + 1; i++)
            {
                var iter = 2;
                foreach (var p in programmers)
                {
                    pck.Workbook.Worksheets[i].Cells["A" + iter].Value = p.Department.Name;
                    pck.Workbook.Worksheets[i].Cells["B" + iter].Value = p.Name;
                    iter++;
                }
            }
        }

        private void WriteProjects(int year = 0, int startMonth = 1, int endMonth = 12)
        {
            if (year == 0)
                year = DateTime.Now.Year;

            var projects = _projectService.GetAll();
            for (int i = startMonth + 1; i <= endMonth + 1; i++)
            {
                var iter = 1;
                foreach (var p in projects)
                {
                    pck.Workbook.Worksheets[i].Cells[((char)(Convert.ToInt16('F') + iter)).ToString() + "1"].Value = p.Title;
                    iter++;
                }
                pck.Workbook.Worksheets[i].Row(1).Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                pck.Workbook.Worksheets[i].Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

            }
        }

        private void WriteProjectsAmount(int year = 0, int startMonth = 1, int endMonth = 12)
        {
            if (year == 0)
                year = DateTime.Now.Year;

            var programmers = _programmerService.GetAll();


            for (int i = startMonth + 1; i <= endMonth + 1; i++)
            {
                var iter = 1;
                foreach (var p in programmers)
                {
                    var uniqueProjectsAmount = p.ToEntity();
                    pck.Workbook.Worksheets[i].Cells["C" + iter].Value = uniqueProjectsAmount;
                    iter++;
                }
            }
        }

    }
}