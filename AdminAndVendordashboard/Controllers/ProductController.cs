using Application.Services;
using DTO_s.Product;
using DTO_s.ViewResult;
using DTOs.Item;
using DTOs.Product;
using DTOs.ProductFilter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace AdminAndVendordashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
    
        [HttpGet]
        [Route("GetProductByID/{ID}")]
        public async Task<IActionResult> GetByIDAsync(int ID)
        {
            var product = await _productService.GetOne(ID);

            if(product.IsSuccess) {
                return Ok(product);
               
            }
            return Empty;
        }
        [HttpGet]
        [Route("getAllProducts")]
        public async Task<IActionResult> getAllAsync()
        {
            var result =await _productService.GetAll();
            if(result== null ||result.Count ==0)
            {
                return Empty;
            }
            return Ok(result);
        }  
        [HttpGet]
        [Route("getAllProductsWithPaging/{pagenumber}/{items}")]
        public async Task<IActionResult> getAllPagingAsync(int items=12 ,int pagenumber=1) 
        {
            var result =await _productService.GetAllPagination(items,pagenumber);//16  1
            if(result== null || result.Count == 0)
            {
                return Empty;
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("getProductsForVendor/{pagenumber}")]
        public async Task<IActionResult> getProductsForVendorAsync( string VendorID,int items= 12, int pagenumber = 1)
        {
            var result =await _productService.GeProductsByVendorID(items,pagenumber, VendorID);//16 - 1
            if(result== null || result.Count == 0)
            {
                return Empty;
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("getProductsByCategoryID/{pagenumber}")]
        public async Task<IActionResult> getProductsByCategoryIDAsync(int catID,int items= 12, int pagenumber = 1)
        {
            var result =await _productService.GetAllByCatIdwithPaging(catID,pagenumber, items); //16 - 1
            if(result== null || result.Count == 0)
            {
                return Empty;
            }
            return Ok(result);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                var id = User?.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;

                productDTO.VendorId = id;
                var result = await _productService.Create(productDTO);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            else
            {
                var errors = new List<string>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                return BadRequest(errors);
            }

            
        }
        [HttpPost]
        [Route("CreateProductWithImage")]
        public async Task<IActionResult> CreateProductAsync(CreateOrUpdateProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
              /*  var id = User?.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;
          


                productDTO.VendorId = id;*/
                var result = await _productService.CreateWithImage(productDTO);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            else
            {
                var errors = new List<string>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                return BadRequest(errors);
            }

            
        }
    
        [HttpPut]
     //  [Authorize]
        public async Task<IActionResult> updateAsync(CreateOrUpdateProductDTO productDTO)
        {

            if (productDTO.Id == 0)
            {
                return BadRequest("Can't Update");
            }

            if (ModelState.IsValid)
            {
               /* var id = User?.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;
                productDTO.VendorId = id;*/
                var result = await _productService.UpdateProduct(productDTO);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            else
            {
                var errors = new List<string>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                return BadRequest(errors);
            }

            
        }
    
        [HttpDelete]
        [Route("deleteProduct/{proID}")]
        [Authorize]
        public async Task<IActionResult> deleteAsync(int proID)
        {
            if (ModelState.IsValid)
            {
                var id = User?.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;
              
                var result = await _productService.SoftDelete(proID);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            else
            {
                var errors = new List<string>();
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(errors);
            }   
        }

    }
}
