using Microsoft.AspNetCore.Mvc;
using Tech_Mart.Data;
using Tech_Mart.Models;
using Tech_Mart.Models.ViewModel;
using Tech_Mart.Settings;

namespace Tech_Mart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public IActionResult Index()
        {
            var product = dbContext.products.AsNoTracking();
            return View(product.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var category = dbContext.categories;

            ViewBag.Category = category;


            return View(new Product());
        }
        [HttpPost]
        public IActionResult Create(Product product, IFormFile file)
        {
            ModelState.Remove("Img");
            ModelState.Remove("file");

            if (file != null && file.Length > 0)
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (!FileSettings.AllowedExtensions.Split(',').Contains(fileExtension))
                {
                    ModelState.AddModelError("Img", "Only image files (JPG, JPEG, PNG, GIF) are allowed.");
                }

                if (file.Length > FileSettings.FileMaxSizeInBytes)
                {
                    ModelState.AddModelError("Img", $"File size must not exceed {FileSettings.FileMaxSizeInMB} MB.");
                }
            }
            else
            {
                ModelState.AddModelError("Img", "Please upload an image file.");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Category = dbContext.categories;
                return View(product);
            }

            #region Save img into wwwroot
            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{FileSettings.ImagesPath}", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                product.Img = fileName;
            }
            #endregion

            dbContext.products.Add(product);
            dbContext.SaveChanges();
            TempData["notifation"] = "Add product successfully";
            return RedirectToAction(nameof(Index));
        }

        //todo : Here 
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var product = dbContext.products.FirstOrDefault(p => p.Id == Id);
            var categories = dbContext.categories.ToList();

            if (product == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            var model = new ProductWithModel
            {
                Product = product,
                Categories = categories
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Product product, IFormFile file)
        {
            #region Save img into wwwroot
            var productInDb = dbContext.products.AsNoTracking().FirstOrDefault(p => p.Id == product.Id);

            if (file != null && file.Length > 0)
            {
                // File Name, File Path
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{FileSettings.ImagesPath}", fileName);

                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{FileSettings.ImagesPath}", productInDb.Img);
                if (System.IO.File.Exists(oldPath))
                {
                    System.IO.File.Delete(oldPath);
                }

                // Copy Img to file
                using (var stream = System.IO.File.Create(filePath))
                {
                    file.CopyTo(stream);
                }

                // Save img into db
                product.Img = fileName;
            }
            else
            {
                product.Img = productInDb.Img;
            }
            #endregion

            if (product != null)
            {
                dbContext.products.Update(product);

                dbContext.SaveChanges();
                TempData["notifation"] = "Update product successfuly";

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("NotFoundPage", "Home");

        }


        public IActionResult Delete(int Id)
        {
            // البحث عن المنتج باستخدام المعرف (ID)
            var product = dbContext.products.FirstOrDefault(p => p.Id == Id);
            if (product != null)
            {
                // تحديد المسار الذي سيتم حذف الصورة منه
                var oldPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{FileSettings.ImagesPath}", product.Img);

                // تحقق من وجود الصورة في المسار
                if (System.IO.File.Exists(oldPath))
                {
                    // حذف الصورة إذا كانت موجودة
                    System.IO.File.Delete(oldPath);
                }

                // حذف المنتج من قاعدة البيانات
                dbContext.Remove(product);
                dbContext.SaveChanges();

                // إرسال رسالة نجاح في TempData
                TempData["notifation"] = "Delete product successfully";

                // إعادة التوجيه إلى صفحة المنتجات (Index)
                return RedirectToAction(nameof(Index));
            }

            // في حال عدم العثور على المنتج، إعادة التوجيه إلى صفحة NotFound
            return RedirectToAction("NotFoundPage", "Home");
        }

    }



}

