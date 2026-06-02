using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject_ECommerce.Models;

namespace WebProject_ECommerce.Controllers
{
    public class CartController : Controller
    {
        ProjectContext _db;

        public CartController(ProjectContext db)
        {
            _db = db;
        }

        // ADD TO CART
        public IActionResult AddToCart(int productId)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                TempData["Message"] = "Please Login First";
                return RedirectToAction("Login", "User");
            }

            var product = _db.tblProducts
                .FirstOrDefault(p => p.Id == productId);

            if (product == null)
                return NotFound();

            //string userEmail = User.Identity.Name;
            string userEmail = HttpContext.Session.GetString("Email");

            var cartItem = _db.tblCartItem
                .FirstOrDefault(c =>
                    c.ProductId == productId &&
                    c.UserEmail == userEmail);

            if (cartItem != null)
            {
                cartItem.Quantity += 1;
            }
            else
            {
                _db.tblCartItem.Add(new CartItem
                {
                    ProductId = product.Id,
                    Price = product.Price,
                    Quantity = 1,
                    UserEmail = userEmail
                });
            }

            _db.SaveChanges();
            TempData["Message"] = "Product Added To Cart Successfully";
            return RedirectToAction("ShowProduct", "User");
        }

        public IActionResult BuyNow(int productId)
        {

            if (HttpContext.Session.GetString("Email") == null)
            {
                TempData["Message"] = "Please Login First";
                return RedirectToAction("Login", "User");
            }

            var product = _db.tblProducts
                .FirstOrDefault(p => p.Id == productId);

            if (product == null)
                return NotFound();

            string userEmail = HttpContext.Session.GetString("Email");

            //Previous Buy Now item remove
            var oldItems = _db.tblCartItem
                .Where(c => c.UserEmail == userEmail)
                .ToList();

            _db.tblCartItem.RemoveRange(oldItems);

            // New item add
            _db.tblCartItem.Add(new CartItem
            {
                ProductId = product.Id,
                Price = product.Price,
                Quantity = 1,
                UserEmail = userEmail
            });

            _db.SaveChanges();

            return RedirectToAction("FakePayment");
        }


        // VIEW CART
        public IActionResult ViewCart()
        {

            if (HttpContext.Session.GetString("Email") == null)
            {
                return RedirectToAction("Login", "User");
            }

            string userEmail = HttpContext.Session.GetString("Email");

            var cartItems = _db.tblCartItem
                .Include(c => c.product)
                .Where(c => c.UserEmail == userEmail)
                .ToList();

            return View(cartItems);
        }


        // REMOVE ITEM
        public IActionResult Remove(int id)
        {
            var item = _db.tblCartItem.Find(id);

            if (item != null)
            {
                _db.tblCartItem.Remove(item);
                _db.SaveChanges();
            }

            return RedirectToAction("ViewCart");
        }

        public IActionResult IncreaseQty(int id)
        {
            var item = _db.tblCartItem.Find(id);

            if (item != null)
            {
                item.Quantity += 1;
                _db.SaveChanges();
            }

            return RedirectToAction("ViewCart");
        }

        public IActionResult DecreaseQty(int id)
        {
            var item = _db.tblCartItem.Find(id);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity -= 1;
                }
                else
                {
                    _db.tblCartItem.Remove(item);
                }

                _db.SaveChanges();
            }

            return RedirectToAction("ViewCart");
        }


        // FAKE PAYMENT
        [HttpGet]
        public IActionResult FakePayment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult FakePayment(string paymentMethod)
        {
            string userEmail = HttpContext.Session.GetString("Email");

            var cartItems = _db.tblCartItem
                .Where(c => c.UserEmail == userEmail)
                .ToList();

            foreach (var item in cartItems)
            {
                Order order = new Order()
                {
                    ProductId = item.ProductId,
                    UserEmail = item.UserEmail,
                    Quantity = item.Quantity,
                    Total_Amount = item.Price * item.Quantity,
                    DOP = DateTime.Now
                };

                _db.tblOrder.Add(order);
            }

            // CLEAR CART
            _db.tblCartItem.RemoveRange(cartItems);

            _db.SaveChanges();

            ViewBag.PaymentMethod = paymentMethod;

            return View("PaymentSuccess");
        }
    }
}