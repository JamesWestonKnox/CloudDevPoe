using CloudDevPoe.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudDevPoe.Controllers
{
    public class UserController : Controller
    {

        public userTable usrtbl = new userTable();

        [HttpPost]
        public ActionResult SignUp(userTable Users)
        {
            var actionResult = usrtbl.InsertUser(Users);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View(usrtbl);
        }
    }
}
