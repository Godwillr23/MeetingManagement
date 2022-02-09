using ChilliSoft_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;

namespace ChilliSoft_Assignment.Controllers
{
    public class ReportController : Controller
    {
        private readonly DataContext _context = new DataContext();
        // GET: Report
        public ActionResult Index()
        {
            var meeting_items = _context.MeetingItems.ToList();
            return View(meeting_items);
        }

        [HttpPost]
        public FileResult Export()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[5] { new DataColumn("MeetingItemName"),
                                            new DataColumn("ItemDescription"),
                                            new DataColumn("ItemStatus"),
                                            new DataColumn("ActionBy"),
                                            new DataColumn("DueDate") });

            var meetingitems = from m in _context.MeetingItems
                            select m;

            foreach (var mi in meetingitems)
            {
                dt.Rows.Add(mi.MeetingItemName, mi.ItemDescription, mi.ItemStatus, mi.ActionBy, mi.DueDate);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "MeetingItems.xlsx");
                }
            }
        }
    }
}