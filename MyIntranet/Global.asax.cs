using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace MyIntranet
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        [System.Web.Mvc.AuthorizeAttribute(Roles = "Users")]
        void Session_Start(object sender, EventArgs e)
        {
            using (var context = new MyIntranet.Models.MyIntranetEntities())
            {
                bool contactExists = context.Users.Any(u => u.UserName.Equals(User.Identity.Name));

                if (contactExists)
                {
                    //Grab details then push to session data
                    var details = context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();

                    //Use reflection to put values in appropriately named session value names
                    foreach (System.Reflection.PropertyInfo info in details.GetType().GetProperties())
                    {
                       if (info.CanRead)
                       {
                           HttpContext.Current.Session[info.Name] = info.GetValue(details, null);
                       }
                    }
                    
                }
                else
                {
                }
            }
        }
    }
}
