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
        public async Task<IActionResult> Create(item newItem)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Add(newItem);        // prepares insert
                // For async code info in the tutorial
                // https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-8.0#asynchronous-code
                await _context.SaveChangesAsync();  // executes pending insert

                ViewData["Message"] = $"{newItem.Name} was added successfully!";
                return View();
            }

            return View(newItem);
        }
    }
}
