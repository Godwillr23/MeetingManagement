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
    public class ItemStatusController : Controller
    {
        private readonly DataContext _context = new DataContext();

        // GET: ItemStatus
        public ActionResult Index()
        {
            var meeting_status = _context.Statuses.ToList();
            return View(meeting_status);
        }
        // Add Status   
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Status model)
        {
            if (ModelState.IsValid)
            {
                _context.Statuses.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //Update Status
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meeting_status = _context.Statuses.SingleOrDefault(e => e.StatusItemId == id);
            if (meeting_status == null)
            {
                return HttpNotFound();
            }
            return View(meeting_status);
        }

        [HttpPost]
        public ActionResult Edit(Status model)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //Delete Status
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var meeting_status = _context.Statuses.SingleOrDefault(e => e.StatusItemId == id);
            if (meeting_status == null)
            {
                return HttpNotFound();
            }
            return View(meeting_status);
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