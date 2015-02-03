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
    public class UsersController : Controller
    {
        private MyIntranetEntities db = new MyIntranetEntities();

        // GET: Users
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var results = new List<Models.User> { };

            string department;

            //We need all departments, and will subquery
            var allDepartmentData = (from d in db.Departments select d).ToList();


            //If no department is set, run a query to get all staff
            if (id > 0)
            {
                // 
                var usersData = (from u in db.Users where u.DepartmentId == id select u);
                results = usersData.ToList();

                // Grab department name from db using the department dataset
                var departmentData = (from a in allDepartmentData where a.Id == id select a).First();
                department = departmentData.Name;

            } else { 
                var usersData = (from u in db.Users select u);
                results = usersData.ToList();

                department = "All";
            }

            //Fill the viewmodel to be passed to the view
            UserViewModel ReturnModel = new UserViewModel {Department = allDepartmentData, User = results};


            ViewBag.department = department;

            return View(ReturnModel);
        }
        public ActionResult View(int? id)
        {

            var dataContext = new MyIntranet.Models.MyIntranetEntities();
            var results = new List<Models.User> { };

            var usersData = (from u in dataContext.Users where u.Id == id select u).FirstOrDefault();

            return View(usersData);
        }
        [HttpGet]    
        public ActionResult Edit(int? id)
        {
            var userData = (from u in db.Users where u.Id == id select u).FirstOrDefault();

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", userData.Department);
            ViewBag.LeaveSupervisor = new SelectList(db.Users, "Id", "FullName", userData.UserHr.LeaveSupervisor);

            return View(userData);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Edit(User user)
        {
              
            if(ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;

                //If updated data for child class exists, then mark those classes to be updated
                if (user.GetType().GetProperty("UserSocial") != null) db.Entry(user.UserSocial).State = EntityState.Modified;
                if (user.GetType().GetProperty("UserHr") != null) db.Entry(user.UserHr).State = EntityState.Modified;
                if (user.GetType().GetProperty("UserComms") != null) db.Entry(user.UserComm).State = EntityState.Modified;

                await db.SaveChangesAsync();
                return RedirectToAction("Edit", user.Id);
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", user.Department);
            ViewBag.LeaveSupervisor = new SelectList(db.Users, "Id", "FullName", user.UserHr.LeaveSupervisor);
            return View(user);
        }
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name");
            ViewBag.LeaveSupervisor = new SelectList(db.Users, "Id", "FullName");
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Name", user.DepartmentId);
            return View(user);
        }
           
    }
}
