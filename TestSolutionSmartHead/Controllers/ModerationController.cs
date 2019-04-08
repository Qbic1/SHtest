using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSolutionSmartHead.Models;
using TestSolutionSmartHead.Models.Enums;

namespace TestSolutionSmartHead.Controllers
{
    [Authorize(Users = "admin")]
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

        [HttpPost]
        public JsonResult Ideas(int id)
        {
            Idea idea = db.Ideas.FirstOrDefault(i => i.Id == id);
            if(idea!=null)
            {
                idea.Blocked = !idea.Blocked;
                db.SaveChanges();
                return Json("ok");
            }
            return Json("error");
        }

        [HttpPost]
        public JsonResult Users(int id)
        {
            User user = db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.Blocked = !user.Blocked;
                db.SaveChanges();
                return Json("ok");
            }
            return Json("error");
        }
    }
}