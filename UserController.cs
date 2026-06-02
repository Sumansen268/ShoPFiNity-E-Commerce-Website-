using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject_ECommerce.Migrations;
using WebProject_ECommerce.Models;
using static System.Net.Mime.MediaTypeNames;
using User = WebProject_ECommerce.Models.User;

namespace WebProject_ECommerce.Controllers
{
    public class UserController : Controller
    {
        ProjectContext _db;
        IWebHostEnvironment _env;
        private object tblcartItem;
        private int total;

        public UserController(ProjectContext db, IWebHostEnvironment env)
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
        public async Task<IActionResult> Login(User obj)
        {
            var result = await _db.tblUser.
                Where(a => a.Email == obj.Email && a.Password == obj.Password).FirstOrDefaultAsync();
            if (result == null)
            {
                ViewBag.Message = "Incorrect Email or Password. Please try again.";
                return View();
            }
            else
            {

                HttpContext.Session.SetString("Email", obj.Email);

                return RedirectToAction("ShowProduct");
            }
        }


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(User obj)
        {
            try
            {
                var exist = await _db.tblUser.Where(a => a.Email == obj.Email).FirstOrDefaultAsync();
                if (exist != null)
                {
                    ViewBag.Message = "This Email already exist.Please try with another one!!!";
                    return View();
                }
                else
                {

                    await _db.tblUser.AddAsync(obj);
                    await _db.SaveChangesAsync();
                    TempData["Message"] = "Registration Success";
                }
            }
            catch
            {
                TempData["Message"] = "Error ocurred";
            }
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> ShowProduct(string? searchString)
        {
            var propList = await (from t1 in _db.tblProducts
                                  join
                                  t2 in _db.tblCategory
                                  on t1.CategoryId equals t2.CatId
                                  select new Product
                                  {
                                      Id = t1.Id,
                                      Name = t1.Name,
                                      Price = t1.Price,
                                      Description = t1.Description,
                                      CategoryId = t1.CategoryId,
                                      CategoryName = t2.CatName,
                                      PictureName = t1.PictureName
                                  }
                                  ).ToListAsync();

            if (searchString != null)
            {


                propList = await (from t1 in _db.tblProducts
                                  join
                                      t2 in _db.tblCategory
                on t1.CategoryId equals t2.CatId
                                  where t1.Name.Contains(searchString) || t1.Description.Contains(searchString)
                                  select new Product
                                  {
                                      Id = t1.Id,
                                      Name = t1.Name,
                                      Price = t1.Price,
                                      Description = t1.Description,
                                      CategoryId = t1.CategoryId,
                                      CategoryName = t2.CatName,
                                      PictureName = t1.PictureName
                                  }
                                  ).ToListAsync();

                ViewBag.SearchText = searchString;
            }

            return View(propList);

        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(
             string email,
             string securityQuestion,
             string answer,
             string newPassword)
        {
            var user = await _db.tblUser
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                ViewBag.Message = "Email not found";
                return View();
            }

            if (user.SecurityQuestion == securityQuestion
                && user.Answer == answer)
            {
                user.Password = newPassword;

                await _db.SaveChangesAsync();

                TempData["Message"] = "Password changed successfully";

                return RedirectToAction("Login");
            }

            ViewBag.Message = "Wrong Security Question or Answer";

            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

    }

}








