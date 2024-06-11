using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace UserPresentation.Controllers
{
    public class ItemController : Controller
    {

        private readonly ICategoryService _categoryService;
        public ItemController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

    }
}
