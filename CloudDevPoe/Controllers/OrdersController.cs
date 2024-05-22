using CloudDevPoe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Data.SqlClient;

namespace CloudDevPoe.Controllers
{
    public class OrdersController : BaseController
    {
        public OrdersController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        [HttpGet]
        public IActionResult Orders()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            if (userID == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var orders = OrdersModel.ShowOrders(userID);
            return View(orders);
        }
    }
}
