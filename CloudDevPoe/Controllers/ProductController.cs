using CloudDevPoe.Models;
using Microsoft.AspNetCore.Mvc;


namespace CloudDevPoe.Controllers
{
    public class ProductController : BaseController
    {
        public productTable prodtbl = new productTable();

        public ProductController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        [HttpPost]
        public ActionResult MyWork(productTable products)
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            var result2 = prodtbl.InsertProduct(products, userID);

            return RedirectToAction("MyWork", "Home");
        }

        [HttpGet]
        public IActionResult MyWork()
        {
            int? userID = HttpContext.Session.GetInt32("UserID");
            if (userID == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var userProducts = productTable.GetUserProducts(userID);
            return View(userProducts);
        }
    }
}
