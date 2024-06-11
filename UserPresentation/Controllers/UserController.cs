using Microsoft.AspNetCore.Mvc;

namespace UserPresentation.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
