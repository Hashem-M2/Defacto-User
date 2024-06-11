using Application.Services;
using DTO_s.Category;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Model;
using System.Diagnostics;
using UserPresentation.Models;

namespace UserPresentation.Controllers
{

    public class HomeController : Controller
    { 
        private readonly ICategoryService _categoryService;

        public HomeController(ICategoryService categoryService)
        {
             _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> IndexMan()
        {
            var result = await _categoryService.GetCategoriesByGender(SubCategory.Men);
            ViewBag.Categories = result.Entities;
            return View();
        }

        public async Task<IActionResult> IndexWoman()
        {
            var result = await _categoryService.GetCategoriesByGender(SubCategory.Women);
            ViewBag.Categories = result.Entities;
            return View();
        }
        public async Task<IActionResult> IndexKids()
        {
            var result = await _categoryService.GetCategoriesByGender(SubCategory.kids);
            ViewBag.Categories = result.Entities;
            return View();
        }

        public async Task<IActionResult> IndexBaby() {
            var result = await _categoryService.GetCategoriesByGender(SubCategory.Baby);
            ViewBag.Categories = result.Entities;
            return View();
        }
        public async Task<IActionResult> IndexBefit()
        {
            return View();
        }
      

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddMonths(1) });
            return LocalRedirect(returnUrl);
        }


    }
}
