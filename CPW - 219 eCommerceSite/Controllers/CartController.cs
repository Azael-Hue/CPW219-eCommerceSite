using Microsoft.AspNetCore.Mvc;
using CPW___219_eCommerceSite.Data;
using CPW___219_eCommerceSite.Models;
using Newtonsoft.Json;

namespace CPW___219_eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ItemContext _context;
        private const string Cart = "ShoppingCart";

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

            CartItemViewModel cartItem = new()
            {
                ItemId = itemToAdd.ItemId,
                Name = itemToAdd.Name,
                Price = itemToAdd.Price
            };

            List<CartItemViewModel> cartItems = GetExistingCartData();
            cartItems.Add(cartItem);
            WriteShoppingCartCookies(cartItems);

            TempData["Message"] = "Item added to cart";
            return RedirectToAction("Index", "Item");
        }

        private void WriteShoppingCartCookies(List<CartItemViewModel> cartItems)
        {
            string cookieData = JsonConvert.SerializeObject(cartItems);


            HttpContext.Response.Cookies.Append(Cart, cookieData, new CookieOptions()
            {
                Expires = DateTimeOffset.Now.AddDays(1)
            });
        }

        /// <summary>
        /// Return the current list of Items in the user's shopping
        /// cart cookie. If there is no cookie, an empty list will be returned.
        /// </summary>
        /// <returns></returns>
        private List<CartItemViewModel> GetExistingCartData()
        {
            string? cookie = HttpContext.Request.Cookies[Cart];
            if (string.IsNullOrEmpty(cookie))
            {
                return new List<CartItemViewModel>();
            }

            return JsonConvert.DeserializeObject<List<CartItemViewModel>>(cookie);
        }

        public IActionResult Summary()
        {
            List<CartItemViewModel> cartItems = GetExistingCartData();
            return View(cartItems);
        }

        public IActionResult Remove(int id)
        {
            List<CartItemViewModel> cartItems = GetExistingCartData();

            CartItemViewModel? targetItem =
                cartItems.Where(i => i.ItemId == id).FirstOrDefault();

            cartItems.Remove(targetItem);

            WriteShoppingCartCookies(cartItems);

            return RedirectToAction(nameof(Summary));
        }
    }
}
