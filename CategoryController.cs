using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject_ECommerce.Models;

namespace WebAppCategory_Product.Controllers
{
    public class CategoryController : Controller

    {
        ProjectContext _db;

        public CategoryController(ProjectContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            string email = HttpContext.Session.GetString("Email_Admin");
            if (email == null || email == "")
            {
                TempData["Message"] = "You have to login First";
                return RedirectToAction("Login", "Admin");
            }
            else
            {

                var CategoryList = await _db.tblCategory.ToListAsync();

                return View(CategoryList);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var row = await (from test in _db.tblCategory
                             where test.CatId == id
                             select test).FirstOrDefaultAsync();
            return View(row);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var row = await _db.tblCategory.Where(_ => _.CatId == id).FirstOrDefaultAsync();
            try
            {
                _db.tblCategory.Remove(row);
                await _db.SaveChangesAsync();
                TempData["Message"] = "Delete Success";
            }

            catch
            {
                TempData["Message"] = "Error";

            }
            return RedirectToAction("Index");

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Category obj)
        {

            try
            {
                await _db.tblCategory.AddAsync(obj);
                await _db.SaveChangesAsync();
                TempData["Message"] = "Insert success";

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error";

            }
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var row = await (from test in _db.tblCategory
                             where test.CatId == id
                             select test).FirstOrDefaultAsync();
            return View(row);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Category obj)
        {

            try
            {
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                TempData["Message"] = "Update success";

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error";

            }
            return RedirectToAction("Index");
        }

    }
}
