using Microsoft.Owin.Security;
using Notarize.BusinessLogic.Interfaces;
using Notarize.BusinessLogic.Models;
using Notarize.Core.Enumerations;
using System.Web;
using System.Web.Mvc;

namespace Notarize.Controllers
{
    public class AccountController : Controller
    {
        IUserManager UserManager;
        IAuthenticationManager AuthManager;

        public AccountController(IUserManager usermanager)
        {
            UserManager = usermanager;
            AuthManager = Request.GetOwinContext().Authentication;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            LoginResult result = UserManager.Login(username, password);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.Error);
                return View();
            }
            
            AuthManager.SignIn(result.Identity);
            return RedirectToAction("Index", "Home", new { area = GetArea(result.User.Role) });
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