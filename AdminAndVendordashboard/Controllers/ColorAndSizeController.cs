using Application.Service.ColotAndSize;
using DTOs.ColorAndSize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminAndVendordashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorAndSizeController : ControllerBase
    {
        private readonly ICorlorAndSIzeService _corlorAndSIzeService;


        public ColorAndSizeController(ICorlorAndSIzeService corlorAndSIzeService)
        {
            _corlorAndSIzeService = corlorAndSIzeService;
        }

        [HttpGet]
        [Route("GetAllcolor")]
        public async Task<IActionResult> GetAllColorasynce()
        {
            var result = await _corlorAndSIzeService.GetAllColor();
            if(result.Count == 0 )
            {
                return Empty ;
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAllSize")]
        public async Task<IActionResult> GetAllSizeasynce()
        {
            var result = await _corlorAndSIzeService.GetAllSize();
            if(result.Count == 0 )
            {
                return Empty ;
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("AddColor")]
        public async Task<IActionResult> addColorasynce(ColorDTO colotDto)
        {
            if(ModelState.IsValid)
            {
                var result =await _corlorAndSIzeService.addColor(colotDto);
                if (result.IsSuccess) { 
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
        [Route("AddSize")]
        public async Task<IActionResult> addColorasynce(SizeDTO sizeDto)
        {
            if(ModelState.IsValid)
            {
                var result =await _corlorAndSIzeService.addSize(sizeDto);
                if (result.IsSuccess) { 
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
        [Route("sDeleteColor")]
        public async Task<IActionResult> SdeleteColor(ColorDTO colorDTO)
        {
            var result = await _corlorAndSIzeService.DeleteColorS(colorDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        } 
        [HttpDelete]
        [Route("sDeleteSize")]
        public async Task<IActionResult> SdeleteSize(SizeDTO sizeDTO)
        {
            var result = await _corlorAndSIzeService.DeleteSizeS(sizeDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        
    }
}
