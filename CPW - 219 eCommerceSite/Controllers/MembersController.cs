using Microsoft.AspNetCore.Mvc;
using CPW___219_eCommerceSite.Models;
using CPW___219_eCommerceSite.Data;

namespace CPW___219_eCommerceSite.Controllers
{
    public class MembersController : Controller
    {
        private readonly ItemContext _itemContext;

        public MembersController(ItemContext itemContext)
        {
            _itemContext = itemContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                // Map RegiserViewModel data to Member object
                Member newMember = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };

                _itemContext.Members.Add(newMember);
                await _itemContext.SaveChangesAsync();

                // Redirect to home page
                return RedirectToAction("Index", "Home");
            }

            return View(regModel);
        }
    }
}
