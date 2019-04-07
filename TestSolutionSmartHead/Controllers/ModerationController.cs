using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSolutionSmartHead.Models;

namespace TestSolutionSmartHead.Controllers
{
    public class ModerationController : Controller
    {
        Context db = new Context();
        // GET: Moderation

        public ActionResult Panel()
        {
            ViewBag.UserName = User.Identity.Name;
            ViewBag.UserRole = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name).Role;
            return View();
        }
        public ActionResult Users()
        {
            ViewBag.UserName = User.Identity.Name;
            ViewBag.UserRole = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name).Role;
            List<User> users = db.Users.ToList();
            return View(users);
        }
        public ActionResult Ideas()
        {
            ViewBag.UserName = User.Identity.Name;
            ViewBag.UserRole = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name).Role;
            List<Idea> ideas = db.Ideas.ToList();
            return View(ideas);
        }

    }
}