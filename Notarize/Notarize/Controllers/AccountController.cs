using Microsoft.Owin.Security;
using Notarize.BusinessLogic.Interfaces;
using Notarize.BusinessLogic.Models;
using Notarize.Core.Enumerations;
using Notarize.Models.Account;
using System.Web;
using System.Web.Mvc;

namespace Notarize.Controllers
{
    public class AccountController : Controller
    {
        IUserManager UserManager;

        public AccountController(IUserManager usermanager)
        {
            UserManager = usermanager;
        }

        [HttpGet]
        public ActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            LoginResult result = UserManager.Login(model.Username, model.Password);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Error);
                return View();
            }

            IAuthenticationManager authManager = Request.GetOwinContext().Authentication;
            authManager.SignIn(new AuthenticationProperties() { IsPersistent = model.RememberMe }, result.Identity);
            return RedirectToAction("Index", "Home", new { area = GetArea(result.User.Role) });
        }

        [HttpPost]
        public ActionResult Logout()
        {
            IAuthenticationManager authManager = Request.GetOwinContext().Authentication;
            authManager.SignOut("AuthorizationCookie");
            return RedirectToAction("Login");
        }

        private string GetArea(UserRole role)
        {
            switch (role)
            {
                case UserRole.Notary:
                    return "Notary";
                default:
                    return "Notary";
            }
        }
    }
}