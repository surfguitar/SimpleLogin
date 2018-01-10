using DomainModels;
using LoginManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginExampleSimple.Controllers
{
    public class RegisterController : Controller
    {
        UserManager _userManager = new UserManager();
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string name, string password, string email)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("Error", "The values are not valid!");
               
            }
            else
            {
                try
                {
                    User user = new User()
                    {
                        Name = name,
                        Password = password,
                        Email = email
                    };

                    int id = _userManager.AddUser(user);

                    if (id > 0)
                    {

                        ViewBag.message = "Your user was created, Hurray!";
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Ex: This login failed");
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
           


            return View();
        }
    }
}