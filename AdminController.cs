using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject_ECommerce.Models;

namespace WebProject_ECommerce.Controllers
{
    public class AdminController : Controller
    {
        ProjectContext _db;
        IWebHostEnvironment _env;

        public AdminController(ProjectContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Admin obj)
        {
            var result = await _db.tblAdmin.
                 Where(a => a.Email == obj.Email && a.Password == obj.Password).FirstOrDefaultAsync();
            if (result == null)
            {
                ViewBag.Message = "Incorrect Email or Password. Please try again.";
                return View();
            }
            else
            {

                HttpContext.Session.SetString("Email_Admin", obj.Email);

                return RedirectToAction("Index", "Category");
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }







    }
}
