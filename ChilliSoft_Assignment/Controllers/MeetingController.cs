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
    public class MeetingController : Controller
    {
        private readonly DataContext _context = new DataContext();

        // GET: Meeting
        public ActionResult Index()
        {
            var meetings = _context.Meetings.ToList();
            return View(meetings);
        }

        //Method to get meeting Type
        public void GetMeetingType()
        {
            // Create db context object here
            //Get the value from database and then set it to ViewBag to pass it View
            List<SelectListItem> meeting_type = _context.MeetingTypes.OrderBy(x => x.MeetingName).Select(c => new SelectListItem
            {
                Value = c.MeetingName,
                Text = c.MeetingName

            }).ToList();

            ViewBag.MeetingCode = meeting_type;
        }

        //Get last added ID
        public int getLastAddedMeetingID(string meeting) {

            int id = _context.Meetings.
                Where(r => r.MeetingCode.StartsWith(meeting)).
                Select(c => c.MeetingId).Count();

            return id + 1;
        }

        //Create New Meeting
        public ActionResult Create()
        {
            GetMeetingType();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Meeting model)
        {
            //GetMeetingType();
            if (ModelState.IsValid)
            {
                string code = model.MeetingCode.Substring(0,1);
                int lastId = getLastAddedMeetingID(code);
                
                model.MeetingCode = code + lastId;
                model.DateCreated = DateTime.Now.Date;

                _context.Meetings.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //Update meeting details
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meeting = _context.Meetings.SingleOrDefault(e => e.MeetingId == id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        [HttpPost]
        public ActionResult Edit(Meeting model)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //Display meeting information
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meeting = _context.Meetings.SingleOrDefault(e => e.MeetingId == id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        //Remove meeting added
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meeting = _context.Meetings.SingleOrDefault(e => e.MeetingId == id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var meeting = _context.Meetings.SingleOrDefault(x => x.MeetingId == id);
            _context.Meetings.Remove(meeting);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}