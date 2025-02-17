using Microsoft.AspNetCore.Mvc;

namespace Tech_Mart.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult NotFoundPage()
        {
            return View();

        }
    }
}
