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
            int daysWithoutRefresh;
            if (user != null)
            {
                ViewBag.UserName = user.Name;
                ViewBag.UserRole = user.Role;
                ViewBag.VoteList = user.VotesList ?? string.Empty;
                if (user.RefreshDate == null)
                {
                    user.RefreshDate = DateTime.Now;
                }
                daysWithoutRefresh = DateTime.Now.Day - user.RefreshDate.Value.Day;
                int i = daysWithoutRefresh / 3;
                int votes = i + user.Votes;
                user.Votes = votes > 10 ? (byte)10 : (byte)votes;
                ViewBag.VoteCount = user.Votes;
                if (i > 0)
                    user.RefreshDate = DateTime.Now;
                db.SaveChanges();
            }
            else
            {
                ViewBag.VoteList = HttpContext.Request.Cookies["voteList"] != null ? HttpContext.Request.Cookies["voteList"].Value : string.Empty;

                if (HttpContext.Request.Cookies["voteCount"] != null)
                {
                    daysWithoutRefresh = DateTime.Now.Day - DateTime.Parse(HttpContext.Request.Cookies["refreshDate"].Value).Day;
                    int i = daysWithoutRefresh / 3;
                    if (i > 0)
                        HttpContext.Response.Cookies["refreshDate"].Value = DateTime.Now.ToString();
                    int votes = i + byte.Parse(HttpContext.Request.Cookies["voteCount"].Value);
                    votes = votes > 10 ? (byte)10 : (byte)votes;
                    HttpContext.Response.Cookies["voteCount"].Value = votes.ToString();
                    ViewBag.VoteCount = votes;
                }
                else
                {
                    HttpContext.Response.Cookies["refreshDate"].Value = DateTime.Now.ToString();
                    HttpContext.Response.Cookies["voteCount"].Value = "10";
                    ViewBag.VoteCount = 10;
                }
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
        public JsonResult AddVote(Vote vote)
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
            User user = null;
            if (User.Identity.IsAuthenticated)
            {
                user = db.Users.FirstOrDefault(u => u.Name == User.Identity.Name);
            }
            if (user != null)
            {
                user.Votes--;
                if (string.IsNullOrWhiteSpace(user.VotesList))
                    user.VotesList = "/" + idea.Id + "/";
                else
                    user.VotesList += idea.Id + "/";
                db.SaveChanges();
            }
            else
            {
                if (HttpContext.Request.Cookies["voteList"] == null)
                    HttpContext.Response.Cookies["voteList"].Value = "/" + idea.Id + "/";
                else
                    HttpContext.Response.Cookies["voteList"].Value = HttpContext.Request.Cookies["voteList"].Value + idea.Id + "/";

                if (HttpContext.Request.Cookies["voteCount"] != null)
                {
                    byte i = byte.Parse(HttpContext.Request.Cookies["voteCount"].Value);
                    HttpContext.Response.Cookies["voteCount"].Value = (i--).ToString();
                }
            }
            return Json("ok");
        }
    }
}