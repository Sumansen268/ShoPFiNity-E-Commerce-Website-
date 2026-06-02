using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject_ECommerce.Models;

namespace WebAppCategory_Product.Controllers
{
    public class ProductController : Controller
    {
        ProjectContext _db;
        IWebHostEnvironment _env;
        public ProductController(ProjectContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> ShowProduct(string? searchstring)
        {


            return View((List<Product>?)await (from t1 in _db.tblProducts
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
                                  ).ToListAsync());
        }


        public async Task<IActionResult> Details(int id)
        {
            var row = await (from test in _db.tblProducts
                             where test.Id == id
                             select test).FirstOrDefaultAsync();
            return View(row);
        }


        [HttpGet]

        public async Task<IActionResult> Create()
        {
            var CatList = await _db.tblCategory.ToListAsync();
            ViewBag.CatList = CatList;
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Product obj)
        {
            try
            {
                obj.PictureName = UploadImage(obj.PictureFile);
                await _db.tblProducts.AddAsync(obj);
                await _db.SaveChangesAsync();
                TempData["Message"] = "Insert Success";
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error Message";

            }
            return RedirectToAction("ShowProduct");
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var CatList = await _db.tblCategory.ToListAsync();
            ViewBag.CatList = CatList;
            var row = await (from test in _db.tblProducts
                             where test.Id == id
                             select test).FirstOrDefaultAsync();
            return View(row);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Product obj)
        {

            try
            {
                string oldimgPath = "";
                if (obj.PictureFile != null)
                {
                    oldimgPath = Path.Combine(_env.WebRootPath, "Photo", obj.PictureName);
                    obj.PictureName = UploadImage(obj.PictureFile);
                }
                _db.Entry(obj).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                if (oldimgPath != "")
                {

                    System.IO.File.Delete(oldimgPath);
                }
                TempData["Message"] = "Update success";

            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error";

            }
            return RedirectToAction("ShowProduct");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var row = await _db.tblProducts.Where(_ => _.Id == id).FirstOrDefaultAsync();
            string prop = row.PictureName;
            try
            {

                _db.tblProducts.Remove(row);
                await _db.SaveChangesAsync();
                if (prop != null)
                {
                    string FilePath = Path.Combine(_env.WebRootPath, "Photo", prop);
                    System.IO.File.Delete(FilePath);
                }
                TempData["Message"] = "Delete Success";
            }

            catch
            {
                TempData["Message"] = "Error";

            }
            return RedirectToAction("ShowProduct");

        }


        public string UploadImage(IFormFile imgFile)
        {
            string fileName = "";

            string filePath = Path.Combine(_env.WebRootPath, "Photo");
            fileName = Guid.NewGuid().ToString().Substring(0, 5) + "_" + imgFile.FileName;
            filePath = Path.Combine(filePath, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                imgFile.CopyTo(fileStream);
            }
            return fileName;
        }

    }



}

