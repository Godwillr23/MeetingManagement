using ChilliSoft_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ChilliSoft_Assignment.Controllers
{
    public class MeetingItemController : Controller
    {
        private readonly DataContext _context = new DataContext();

        // GET: MeetingItem
        public ActionResult Index()
        {
            var meeting_items = _context.MeetingItems.ToList();
            return View(meeting_items);
        }

        //Method to get meeting Type
        public void GetMeeting()
        {
            // Create db context object here
            //Get the value from database and then set it to ViewBag to pass it View
            List<SelectListItem> meeting_type = _context.Meetings.OrderBy(x => x.MeetingCode).Select(c => new SelectListItem
            {
                Value = c.MeetingId.ToString(),
                Text = c.MeetingCode

            }).ToList();

            ViewBag.MeetingCode = meeting_type;
        }

        //Method to get Statuses List
        public void GetStatus()
        {
            // Create db context object here
            //Get the value from database and then set it to ViewBag to pass it View
            List<SelectListItem> status = _context.Statuses.OrderBy(x => x.StatusItem).Select(c => new SelectListItem
            {
                Value = c.StatusItem,
                Text = c.StatusItem

            }).ToList();

            ViewBag.Status = status;
        }

        //Method to get Staff Member
        public void GetStaff()
        {
            // Create db context object here
            //Get the value from database and then set it to ViewBag to pass it View
            List<SelectListItem> staff = _context.Staffs.OrderBy(x => x.Firstname).Select(c => new SelectListItem
            {
                Value = c.Firstname.Substring(0,1).ToUpper()+ c.Lastname.Substring(0, 1).ToUpper(),
                Text = c.Firstname+","+c.Lastname

            }).ToList();

            ViewBag.Staff = staff;
        }

        //Save Status for each Item on Create and Update
        public void SaveItemStatus(int itemId,string status) {

            MeetingItemStatus itemstatus = new MeetingItemStatus();
            itemstatus.ItemId = itemId;
            itemstatus.Status = status;

            _context.MeetingItemStatuses.Add(itemstatus);
            _context.SaveChanges();
        }

        //Add new Meeting Item
        public ActionResult Create()
        {
            GetStatus();
            GetStaff();
            GetMeeting();
            return View();
        }
        [HttpPost]
        public ActionResult Create(MeetingItem model)
        {
            if (ModelState.IsValid)
            {
                model.DateCreated = DateTime.Now.Date;

                _context.MeetingItems.Add(model);
                _context.SaveChanges();

                SaveItemStatus(model.ItemId, model.ItemStatus);
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            GetStatus();
            GetStaff();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meeting_items = _context.MeetingItems.SingleOrDefault(e => e.ItemId == id);
            if (meeting_items == null)
            {
                return HttpNotFound();
            }
            return View(meeting_items);
        }

        [HttpPost]
        public ActionResult Edit(MeetingItem model)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meeting_items = _context.MeetingItems.SingleOrDefault(e => e.ItemId == id);
            if (meeting_items == null)
            {
                return HttpNotFound();
            }
            return View(meeting_items);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meeting_items = _context.MeetingItems.SingleOrDefault(e => e.ItemId == id);
            if (meeting_items == null)
            {
                return HttpNotFound();
            }
            return View(meeting_items);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var status = _context.Statuses.SingleOrDefault(x => x.StatusItemId == id);
            _context.Statuses.Remove(status);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}