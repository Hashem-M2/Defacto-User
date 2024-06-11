using Application.Service.ItemF;
using DTO_s.Product;
using DTOs.Item;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace AdminAndVendordashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {

        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;

        }
        [HttpGet]
        [Route("GetItemtByID/{ID}")]
        public async Task<IActionResult> GetByIDAcync(int ID)
        {
            var product = await _itemService.GetByID(ID);

            if (product == null)
            {
                return Empty;
            }
            return Ok(product);
        }
        [HttpGet]
        [Route("getAllItems")]
        public async Task<IActionResult> getAllAcync()
        {
            var result = await _itemService.GetAll();
            if (result == null || result.Count == 0)
            {
                return Empty;
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("getAllItemsWithPaging/{pagenumber}/{items}")]
        public async Task<IActionResult> getAllPagingAcync(int items = 12, int pagenumber = 1)
        {
            var result = await _itemService.GetAllPagination(items, pagenumber);//16  1
            if (result == null || result.Count == 0)
            {
                return Empty;
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("GetItemlistFotProduct/{proId}")]
        public async Task<IActionResult> GetItemListForProduct(int proId, int items = 12, int pagenumber = 1)
        {
            var result = await _itemService.GetListOfItemForProduct(items, pagenumber, proId);
            if (result == null || result.Count == 0) {
                return Empty;
            }
            return Ok(result);
        }

        [HttpPost]
       // [Authorize]
        public async Task<IActionResult> CreateAcync(ItemDto itemDto)
        {

            if (ModelState.IsValid)
            {
              /*  var role = User?.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
*/
              /*  if (role.ToLower() == "admin")
                {
                    itemDto.IsForDefacto = true;
                }
*/
                var result = await _itemService.Create(itemDto);
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
   //     [Authorize]
        public async Task<IActionResult> updateAcync(ItemDto itemDto)
        {


            if( itemDto.Id == 0)
            {
                return BadRequest("Can't Update");
            }


            if (ModelState.IsValid)
            {
                
             
                var result = await _itemService.Update(itemDto);
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
        [Route("HardDelete")]
        [Authorize]
        public async Task<IActionResult> HdeleteAcync(int itemID)
        {
            if (itemID== 0)
            {
                return BadRequest("Can't Deleted");
            }

            if (ModelState.IsValid)
            {
              
                var result = await _itemService.HDelete(itemID);
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
        [Route("SoftDelete")]
        [Authorize]
        public async Task<IActionResult> SdeleteAcync(int itemID)
        {
            if (itemID == 0)
            {
                return BadRequest("Can't Delete");
            }
            if (ModelState.IsValid)
            {
              
                var result = await _itemService.SDelete(itemID);
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
