using Microsoft.AspNetCore.Mvc;

namespace UserPresentation.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
