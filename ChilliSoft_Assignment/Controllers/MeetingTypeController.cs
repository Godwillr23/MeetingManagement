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
    public class MeetingTypeController : Controller
    {        private readonly DataContext _context = new DataContext();

        // GET: MeetingType
        public ActionResult Index()
        {
            var meeting_type = _context.MeetingTypes.ToList();
            return View(meeting_type);
        }
        // Get  
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(MeetingType model)
        {
            if (ModelState.IsValid)
            {
                _context.MeetingTypes.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meeting_type = _context.MeetingTypes.SingleOrDefault(e => e.MeetingTypeId == id);
            if (meeting_type == null)
            {
                return HttpNotFound();
            }
            return View(meeting_type);
        }

        [HttpPost]
        public ActionResult Edit(MeetingType model)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }  
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meeting_type = _context.MeetingTypes.SingleOrDefault(e => e.MeetingTypeId == id);
            if (meeting_type == null)
            {
                return HttpNotFound();
            }

            return View(meeting_type);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var meetingtype = _context.MeetingTypes.SingleOrDefault(x => x.MeetingTypeId == id);
            _context.MeetingTypes.Remove(meetingtype);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}