using CloudDevPoe.Models;
using Microsoft.AspNetCore.Mvc;
using CloudDevPoe.Models;


namespace CloudDevPoe.Controllers
{
    public class ProductDisplayController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var products = ProductDisplayModel.SelectProducts();
            return View(products);
        }
    }
}
