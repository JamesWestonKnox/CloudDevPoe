using CloudDevPoe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace CloudDevPoe.Controllers
{
    public class UserController : BaseController
    {

        public userTable usrtbl = new userTable();

        public UserController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        [HttpPost]
        public ActionResult SignUp(userTable Users)
        {
            var actionResult = usrtbl.InsertUser(Users);
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View(usrtbl);
        }
    }
}
