using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using MyIntranet.ViewModels;
using MyIntranet.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyIntranet.Controllers
{
    public class UserHrController : Controller
    {
        //Create data context
        private MyIntranetEntities db = new MyIntranetEntities();
        
        [HttpGet]
        public ActionResult RequestLeave()
        {
            //Create drop down list for half days
            List<SelectListItem> uiHalfDay = new List<SelectListItem>();
            uiHalfDay.Add( new SelectListItem { Text = "All Day", Value = "" });
            uiHalfDay.Add( new SelectListItem { Text = "Morning", Value = "AM" });
            uiHalfDay.Add( new SelectListItem { Text = "Evening", Value = "PM" });

            ViewBag.StartHalfDay = ViewBag.EndHalfDay = uiHalfDay;
            ViewBag.UserHrId = Session["UserHrId"];


            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> RequestLeave([Bind(Include = "StartDate,EndDate,StartHalfDay,EndHalfDay,UserHrId")]LeaveRequest request)
        {
            bool conflict = false;
            bool tooSoon = false;
            bool error = false;
            List<string> errorMessage = new List<string> { };
            

            //Firstly, check the dates actually make sense and don't go over a financial year end
            if (!(request.StartDate.Month < 4 && request.EndDate.Month > 3))
            {

                request.DateRequested = DateTime.Now;

                //Count how many days the request takes up by sending to utility class
                request.Days = MyIntranet.Utilities.DateAndTime.CalculateWorkingDays(request.StartDate, request.EndDate, request.StartHalfDay, request.EndHalfDay);

                //Check for leave conflicts with dependents
                UserHr conflictcheck = (from c in db.UserHrs where c.Id == request.UserHrId select c).First();
                conflict = conflictcheck.LeaveConflict(db, request.StartDate, request.EndDate);

                //Check user has enough days remaining
                bool enoughDays = (conflictcheck.LeaveRemainingByDate(request.EndDate) > 0);

                //Check for overlapping requests for this user
                bool selfConflict = (from l in conflictcheck.LeaveRequests where l.StartDate <= request.EndDate && l.EndDate >= request.StartDate select l.Id).Any();

                //Check holiday has been booked at least six weeks in advance
                tooSoon = (request.StartDate > DateTime.Today.AddDays(-42));

                if (enoughDays || selfConflict || request.Days == 0)
                {
                    error = true;
                    if (enoughDays) errorMessage.Add("You don't have enough days left for this leave request.");
                    if (selfConflict) errorMessage.Add("Your leave request overlaps with another request you have made.");
                    if (request.Days == 0) errorMessage.Add("Your leave request doesn't contain any working days");
                }

                //If we've got no issues, create the request
                if (!error)
                {
                    //Add the missing fields, such as leave approved by HR and pending by supervisor
                    request.HrApproved = true;

                    if (ModelState.IsValid)
                    {
                        db.LeaveRequests.Add(request);
                        await db.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                error = true;
                errorMessage.Add("Leave request overlaps financial year or start date is after end date");
            }



            ViewBag.Errors = errorMessage;

            //Create drop down list for half days
            List<SelectListItem> uiHalfDay = new List<SelectListItem>();
            uiHalfDay.Add(new SelectListItem { Text = "All Day", Value = "" });
            uiHalfDay.Add(new SelectListItem { Text = "Morning", Value = "AM" });
            uiHalfDay.Add(new SelectListItem { Text = "Evening", Value = "PM" });

            ViewBag.StartHalfDay = ViewBag.EndHalfDay = uiHalfDay;

            return View();
        }
    }

}