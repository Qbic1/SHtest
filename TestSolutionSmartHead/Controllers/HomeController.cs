using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestSolutionSmartHead.Models;
using TestSolutionSmartHead.Models.Vote;

namespace TestSolutionSmartHead.Controllers
{
    public class HomeController : Controller
    {
        Context db = new Context();

        public ActionResult Index()
        {
            List<Idea> ideas = db.Ideas.ToList();
            User user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            }

            if (user != null)
            {
                ViewBag.UserName = user.Name;
                ViewBag.UserRole = user.Role;
            }

            return View(ideas);
        }

        [HttpPost]
        [Authorize]
        public ActionResult Index(Idea idea)
        {
            User user = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            idea.User = user;
            db.Ideas.Add(idea);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult AddVote(Vote vote)
        {
            Idea idea = db.Ideas.FirstOrDefault(i => i.Id == vote.Id);
            if (idea != null)
            {
                if (vote.IsPos == 1)
                    idea.Positive++;
                else
                    idea.Negative++;
                db.SaveChanges();   
            }
            return RedirectToAction("Index", "Home");
        }
    }
}