using Application.Services;
using DTO_s.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminAndVendordashboard.Controllers
{
    [Route("api/[controller]")]
   
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService )
        {
            _categoryService = categoryService;    
        }

        [HttpGet]
        [Route("GetAllWithPaging")]
        public async Task<IActionResult> GetAllWithPaging(int items , int pageNumber=1)
        {
            var model = await _categoryService.GetAllPagination(items, pageNumber);
            if(model == null)
            {
                return BadRequest("Empty ");
            }
            return Ok(model);
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var model = await _categoryService.GetAllCategories();
            if(model == null)
            {
                return BadRequest("Empty ");
            }
            return Ok(model.ToList());
        }  
        [HttpGet]
        [Route("GetCategoryByID/{ID}")]
        public async Task<IActionResult> GetAll(int ID)
        {
            var model = await _categoryService.GetCategoryByID(ID);
            if(model == null)
            {
                return BadRequest(" not found");
            }
            return Ok(model);
        }

        [HttpPost]

        /*   [Authorize(Roles = "admin")]*/
        //[Authorize]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
         /*   var user = User?.Claims;*/
            var id = User?.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;
            var result =  await _categoryService.CreateCategory(category);
            if(!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
        // update category
        [HttpPut]
        public async Task<IActionResult> Update(CategoryDTO category)
        {
            var result = await _categoryService.UpdateCategory(category);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }

        //delete category
        [HttpDelete]
        [Route("SoftDelete/{catID}")]
   
        public async Task<IActionResult> SoftDelete(int catID)
        {
            
            var result = await _categoryService.SoftDelete(catID);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
        [HttpDelete]
  
        [Route("HardDelete/{catID}")]
        public async Task<IActionResult> HardDelete(int catID)
        {
            var result = await _categoryService.HardDelete(catID);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Message);
        }
        // add new color  or delete exist color 

    }
}
