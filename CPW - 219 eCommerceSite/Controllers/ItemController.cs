using CPW___219_eCommerceSite.Data;
using CPW___219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW___219_eCommerceSite.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemContext _context;

        public ItemController(ItemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(item newItem)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(newItem); // prepares insert
                _context.SaveChanges(); // executes pending insert
                ViewData["Message"] = $"{newItem.Name} was added successfully!";
                // Show success message on page
                return View();
            }

            return View(newItem);
        }
    }
}
