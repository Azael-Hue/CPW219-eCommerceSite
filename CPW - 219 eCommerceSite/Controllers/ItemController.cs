using CPW___219_eCommerceSite.Data;
using CPW___219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPW___219_eCommerceSite.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemContext _context;

        public ItemController(ItemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // List<item> items = _context.Items.ToList(); // Get all items from the database
            List<item> items = await (from item in _context.Items
                                select item).ToListAsync();

            return View(items); // Show them on the page
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

        public async Task<IActionResult> Edit(int id)
        {
            item? itemToUpdate = await _context.Items.FindAsync(id);

            if(itemToUpdate == null)
            {
                return NotFound();
            }

           return View(itemToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(item itemModel)
        {
            if (ModelState.IsValid)
            {
                _context.Items.Update(itemModel);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{itemModel.Name} was updated successfully!";
                return RedirectToAction("Index");
            }

            return View(itemModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            item? itemToDelete = await _context.Items.FindAsync(id);

            if( itemToDelete == null)
            {
                return NotFound();
            }

            return View(itemToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            item itemToDelete = await _context.Items.FindAsync(id);

            if (itemToDelete != null)
            {
                _context.Items.Remove(itemToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = itemToDelete.Name + " was deleted successfully!";
                return RedirectToAction("Index");
            }

            TempData["Message"] = "This item was already deleted!";
            return RedirectToAction("Index");
        }
    }
}
