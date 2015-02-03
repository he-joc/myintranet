using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyIntranet.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Users")]
        public ActionResult Index(int uid = 1)
        {
            string iReturn;
            
            using (var context = new MyIntranet.Models.MyIntranetEntities())
            {
                // Query for the Blog named ADO.NET Blog 
                var details = context.Users.Where(u => u.UserName == "HARDING\\joc").FirstOrDefault(); 

                iReturn = details.LastName + ", " + details.FirstName;
            }

            ViewBag.UserName = Session["FirstName"] + " " + Session["LastName"];
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}