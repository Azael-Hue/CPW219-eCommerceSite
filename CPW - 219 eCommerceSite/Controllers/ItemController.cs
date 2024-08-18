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

        public async Task<IActionResult> Index(int? id)
        {
            const int NumGamesToDisplayPerPage = 3;
            const int PageOffset = 1; // Need a page offset to use current page and figure out, num games to skip

            int currPage = id ?? 1; // Set currPage to id if it has value, otherwise use 1

            int totalNumOfProducts = await _context.Items.CountAsync(); // Get total number of products
            double maxNumPages = Math.Ceiling((double)totalNumOfProducts / NumGamesToDisplayPerPage);
            int lastPage = Convert.ToInt32(maxNumPages); // Rounding pages up to the nearest whole page number

            List<item> items = await (from item in _context.Items
                                    select item)
                                    .Skip(NumGamesToDisplayPerPage * (currPage - PageOffset))
                                    .Take(NumGamesToDisplayPerPage)
                                    .ToListAsync();

            ItemCatalogViewModel catalogModel = new (items, lastPage, currPage);
            return View(catalogModel); // Show them on the page
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

        public async Task<IActionResult> Details(int id)
        {
            item? detailedItem = await _context.Items.FindAsync(id);

            if( detailedItem == null)
            {
                return NotFound();
            }

            return View(detailedItem);
        }
    }
}
