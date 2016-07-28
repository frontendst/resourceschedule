using System.Web.Mvc;
using System.Web.Security;
using ResourceManagementSystem2.Models.Exceptions;
using ResourceManagementSystem2.Models;

namespace ResourceManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LoginViewModel model)
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
