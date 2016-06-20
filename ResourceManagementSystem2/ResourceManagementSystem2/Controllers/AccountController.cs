using ResourceManagementSystem2.Models;
using ResourceManagementSystem2.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ResourceManagementSystem2.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            bool isAllowedUser;
            try
            {
                isAllowedUser = LoginChecker.IsAllowedUser(model.Username, model.Password);
            }
            catch (UserNotFoundException)
            {
                ModelState.AddModelError("", "Неверное имя пользователя.");
                return View(model);
            }
            catch (IncorrectPasswordException)
            {
                ModelState.AddModelError("", "Неверный пароль.");
                return View(model);
            }

            if (!isAllowedUser)
            {
                ViewBag.Message = "У Вас нет прав для доступа к сайту.";
                return RedirectToAction("Error", "Error");
            }

            FormsAuthentication.SetAuthCookie(model.Username, false);
            return RedirectToAction("Index", "Scheduler");
        }

        
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            if (Request.IsAuthenticated)
            {
                FormsAuthentication.SignOut();
            }

            return RedirectToAction("LogIn", "Account");
        }
    }
}