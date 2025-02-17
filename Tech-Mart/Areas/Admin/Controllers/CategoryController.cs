using Microsoft.AspNetCore.Mvc;
using Tech_Mart.Data;
using Tech_Mart.Models;

namespace Tech_Mart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        [HttpGet]
        public IActionResult Index()
        {
            var categories = dbContext.categories.AsNoTracking();
            return View(categories.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View(new Category());
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                dbContext.categories.Add(category);
                dbContext.SaveChanges();

                TempData["Notifation"] = "Add Category Successfuly";

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("NotFoundPage", "Home");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var category = dbContext.categories.Find(Id);
            if (category != null)
            {
                return View(category);
            }

            return RedirectToAction("NotFoundPage", "Home");
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category != null)
            {
                dbContext.categories.Update(new Category()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                    
                });
                dbContext.SaveChanges();

                TempData["notifation"] = "Update category successfuly";

                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("NotFoundPage", "Home");
        }



        public IActionResult Delete(int Id)
        {
            var CategoryDelete = dbContext.categories.Find(Id);
            if (CategoryDelete != null)
            {
                dbContext.categories.Remove(CategoryDelete);
                dbContext.SaveChanges();

                TempData["Notifation"] = "Delete Category Successfuly";

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("NotFoundPage", "Home");

        }

    }
}
