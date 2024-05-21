using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CloudDevPoe.Controllers
{
    public class BaseController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            string userName = _httpContextAccessor.HttpContext.Session.GetString("UserName");
            ViewData["userID"] = userID;
            ViewData["userName"] = userName;
        }
    }
}
