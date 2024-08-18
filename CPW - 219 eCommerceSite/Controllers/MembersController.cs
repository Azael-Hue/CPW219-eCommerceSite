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

                LogUserIn(newMember.Email);

                // Redirect to home page
                return RedirectToAction("Index", "Home");
            }

            return View(regModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Check DB for credentials
                Member? m = (from member in _itemContext.Members
                           where member.Email == loginModel.Email &&
                                 member.Password == loginModel.Password
                           select member).SingleOrDefault();
                // If exists, send to homepage
                if (m != null)
                {
                    LogUserIn(loginModel.Email);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Credentials not found!");
            }
            // If no record matches or ModelState is invalid, display error
            return View(loginModel);
        }

        private void LogUserIn(string email)
        {
            HttpContext.Session.SetString("Email", email);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
