using Microsoft.AspNetCore.Mvc;
using CPW___219_eCommerceSite.Data;
using CPW___219_eCommerceSite.Models;

namespace CPW___219_eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ItemContext _context;

        public CartController(ItemContext context)
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            item? itemToAdd = _context.Items.Where(i => i.ItemId == id).SingleOrDefault();

            if (itemToAdd == null)
            {
                // item with that id not found
                TempData["Message"] = "Sorry that item was not found";
                return RedirectToAction("Index", "Item");
            }

            // Todo: Add item to a shopping cart cookie
            TempData["Message"] = "Item added to cart";
            return RedirectToAction("Index", "Item");
        }
    }
}
