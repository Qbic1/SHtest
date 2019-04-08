using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TestSolutionSmartHead.Models;
using TestSolutionSmartHead.Models.Enums;

namespace TestSolutionSmartHead
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AddAdmin();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddAdmin()
        {
            Context db = new Context();
            User user = db.Users.FirstOrDefault(u => u.Name == "admin");

            if (user == null)
            {
                user = new User("admin", "admin");
                user.Role = UserType.Admin;
                user.Votes = 10;
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}
