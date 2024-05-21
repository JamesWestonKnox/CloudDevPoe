using CloudDevPoe.Models;
using Microsoft.AspNetCore.Mvc;
using CloudDevPoe.Models;

namespace CloudDevPoe.Controllers
{
    public class LoginController : Controller
    {

        private readonly LoginModel login;

        public LoginController()
        {
            login = new LoginModel();
        }

        [HttpPost]
        public IActionResult Login(string email, string name)
        {
            var LoginModel = new LoginModel();
            int userID = LoginModel.SelectUser(email, name);
            if (userID != -1) 
            {
                HttpContext.Session.SetInt32("UserID", userID);
                HttpContext.Session.SetString("UserName", name);

                return RedirectToAction("Index", "Home");
            }
            else 
            {
                return View("LoginFailed");
            }
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear the user's session

            return RedirectToAction("Index", "Home");
        }
    }
}
