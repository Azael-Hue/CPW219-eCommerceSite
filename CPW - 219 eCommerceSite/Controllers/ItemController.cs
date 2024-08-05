using Microsoft.AspNetCore.Mvc;

namespace CPW___219_eCommerceSite.Controllers
{
    public class ItemController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
