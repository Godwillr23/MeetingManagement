using ChilliSoft_Assignment.Helpers;
using ChilliSoft_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ChilliSoft_Assignment.Controllers
{
    public class MeetingController : Controller
    {
        private readonly DataContext _context = new DataContext();
        Helper helper = new Helper();

        // Get Meeting
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
                Value = c.Firstname.Substring(0, 1).ToUpper() + c.Lastname.Substring(0, 1).ToUpper(),
                Text = c.Firstname + "," + c.Lastname

            }).ToList();

            ViewBag.Staff = staff;
        }

        //Create New Meeting
        public ActionResult Create()
        {
            Session.Remove("MeetingID");
            GetMeetingType();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Meeting model)
        {
            //GetMeetingType();

            if (ModelState.IsValid)
            {
                try
                {
                    string code = helper.firstLetterWord(model.MeetingCode);
                    int lastId = helper.getLastAddedMeetingID(code);

                    //calculation to get the previous saved MeetingCode
                    // int prevCodeId = lastId - 1;
                    int previousId = helper.getPreviousMeetingId(code);

                    model.MeetingCode = code + lastId;
                    model.DateCreated = DateTime.Now.Date;

                    _context.Meetings.Add(model);
                    _context.SaveChanges();

                    Session["MeetingID"] = model.MeetingId;

                    //Open the meeting Item screen with previous meeting Items.
                    if (helper.checkMeetingCode(code) > 0)
                    {
                        return RedirectToAction("PreviousMeeting", "Meeting", new { id = previousId });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Meeting");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            else {
                GetMeetingType();
            }

            return View(model);
        }

        //display previous meeting to be carried forward
        public ActionResult PreviousMeeting(int? id)
        {
            //Session["MeetingID"] = id;
            var meeting_items = _context.MeetingItems.Where(x => x.MeetingId == id).ToList();
            return View(meeting_items);
        }

        // Create meeting using carried forward Meeting Items

        public JsonResult AddMeetingItems(List<MeetingItem> model)
        {
            int id = Convert.ToInt32(Session["MeetingID"].ToString());

            MeetingItem entities = new MeetingItem();
            
                //Check for NULL.
                if (model == null)
                {
                    model = new List<MeetingItem>();
                }

                //Loop and insert records.
                foreach (var item in model)
                {

                    entities.MeetingId = id;
                    entities.MeetingItemName = Regex.Replace(item.MeetingItemName, @"\n", "").Trim();
                    entities.ItemDescription = Regex.Replace(item.ItemDescription, @"\n", "").Trim();
                    entities.ActionBy = Regex.Replace(item.ActionBy, @"\n", "").Trim();
                    entities.ItemStatus = Regex.Replace(item.ItemStatus, @"\n", "").Trim();
                    entities.DueDate = Regex.Replace(item.DueDate, @"\n", "").Trim();
                    entities.isSelected = false;
                    entities.DateCreated = DateTime.Now.Date;                   

                    _context.MeetingItems.Add(entities);
                    _context.SaveChanges();
                    SaveItemStatus(entities.ItemId, entities.ItemStatus);
                }

            return Json(entities);

        }

        public ActionResult CarriedForwardItems(int? id)
        {
            var meeting_items = _context.MeetingItems.Where(x => x.MeetingId == id).ToList();
            return View(meeting_items);
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
                try
                {
                    _context.Entry(model).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
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
            try
            {
                var meeting = _context.Meetings.SingleOrDefault(x => x.MeetingId == id);
                _context.Meetings.Remove(meeting);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            return RedirectToAction("Index");
        }

        //End Meeting

            // Start Meeting Items
        public ActionResult MeetingItems(int? id)
        {
            Session["MeetingID"] = id;
            var meeting_items = _context.MeetingItems.Where(x => x.MeetingId == id).ToList();
            return View(meeting_items);
        }

        //Save Status for each Item on Create and Update
        public void SaveItemStatus(int itemId, string status)
        {
            try
            {

                MeetingItemStatus itemstatus = new MeetingItemStatus();
                itemstatus.ItemId = itemId;
                itemstatus.Status = status;

                _context.MeetingItemStatuses.Add(itemstatus);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException);
            }
        }

        
        public ActionResult CreateMeetingItem()
        {
            GetStatus();
            GetStaff();
            GetMeeting();
            return View();
        }

        //Add new Meeting Item
        [HttpPost]
        public ActionResult CreateMeetingItem(MeetingItem model)
        {
            int meetingid = Convert.ToInt32(Session["MeetingID"].ToString());

            if (ModelState.IsValid)
            {
                try
                {

                    model.MeetingId = meetingid;

                    if (ModelState.IsValid)
                    {
                        model.DateCreated = DateTime.Now.Date;

                        _context.MeetingItems.Add(model);
                        _context.SaveChanges();

                        SaveItemStatus(model.ItemId, model.ItemStatus);
                        return RedirectToAction("MeetingItems", new { id = meetingid });
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException);
                }
            }
            else {
                GetStatus();
                GetStaff();
                GetMeeting();
            }

            return View(model);
        }

        public ActionResult AddNewStatus(int? id)
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

        //Add new Status for the Item
        [HttpPost]
        public ActionResult AddNewStatus(MeetingItem model)
        {
            int meetingid = Convert.ToInt32(Session["MeetingID"].ToString());

            if (ModelState.IsValid)
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();

                SaveItemStatus(model.ItemId, model.ItemStatus);
                return RedirectToAction("MeetingItems", new { id = meetingid });
            }

            return View(model);
        }

        public ActionResult EditMeetingItem(int? id)
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

        //Update Meeting Item
        [HttpPost]
        public ActionResult EditMeetingItem(MeetingItem model)
        {
            int meetingid = Convert.ToInt32(Session["MeetingID"].ToString());

            if (ModelState.IsValid)
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();

                return RedirectToAction("MeetingItems", new { id = meetingid });
            }

            return View(model);
        }
        public ActionResult MeetingItemHistory(int? id)
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
        public ActionResult DeleteMeetingItem(int? id)
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

        //Delete Meeting Item
        [HttpPost]
        public ActionResult DeleteMeetingItem(int id)
        {
            int meetingid = Convert.ToInt32(Session["MeetingID"].ToString());

            var status = _context.Statuses.SingleOrDefault(x => x.StatusItemId == id);
            _context.Statuses.Remove(status);
            _context.SaveChanges();

            return RedirectToAction("MeetingItems", new { id = meetingid });
        }
    }
}