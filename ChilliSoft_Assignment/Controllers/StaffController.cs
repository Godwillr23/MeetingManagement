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
    public class StaffController : Controller
    {
        private readonly DataContext _context = new DataContext();

        // GET: Staff
        public ActionResult Index()
        {
            var staff = _context.Staffs.ToList();
            return View(staff);
        }

        // Add Staff Member  
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Staff model)
        {
            if (ModelState.IsValid)
            {
                _context.Staffs.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //Update Staff Details
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var staff = _context.Staffs.SingleOrDefault(e => e.StaffId == id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        [HttpPost]
        public ActionResult Edit(Staff model)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //Delete Staff Member
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var staff = _context.Staffs.SingleOrDefault(e => e.StaffId == id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var staff = _context.Staffs.SingleOrDefault(x => x.StaffId == id);
            _context.Staffs.Remove(staff);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}