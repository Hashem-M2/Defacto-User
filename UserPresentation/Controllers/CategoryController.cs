using Microsoft.AspNetCore.Mvc;

namespace UserPresentation.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
