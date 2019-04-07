using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TestSolutionSmartHead.Models;
using TestSolutionSmartHead.Models.Account;
using TestSolutionSmartHead.Models.Enums;

namespace TestSolutionSmartHead.Controllers
{
    public class AccountController : Controller
    {
        Context db = new Context();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {

                User appUser = db.Users.FirstOrDefault(u => u.Name == model.Name);

                if (appUser == null || !appUser.CheckPassword(model.Password))
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return appUser.Role == UserType.User ? RedirectToAction("Index", "Home") : RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault(u => u.Name == model.Name);

                if (user != null)
                    ModelState.AddModelError("", "Пользователь уже зарегистрирован");
                else
                {
                    user = new User(model.Name, model.Password);
                    user.Role = UserType.Admin;
                    user.Votes = 10;
                    db.Users.Add(user);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}