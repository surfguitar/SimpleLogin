using DomainModels;
using LoginManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginExampleSimple.Controllers
{
    public class LoginController : Controller
    {
        UserManager _userManager = new UserManager();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string name, string password)
        {
            User user = new User();

            user = _userManager.GetUser(name, password);

            if (user != null)
            {
                Session["user"] = user;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("Error", "Ex: This login failed");
            }

            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}