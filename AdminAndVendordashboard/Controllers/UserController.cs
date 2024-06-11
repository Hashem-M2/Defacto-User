using Application.Service.User;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminAndVendordashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IuserService _userService;
      

        public UserController(IuserService userService)
        {
            _userService = userService;
          
        }
      
        [HttpGet("GetAllUsers")]
     //   [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> GetAllUsers()
        {
            var Users = await _userService.GetAllUsers();
            if (Users.IsSuccess)
            {
                return Ok(Users);
            }
            else
            {
                return BadRequest(Users.Message);
            }

        } 
        
        [HttpGet("AllUsersPaging/{items}/{pagenumber}")]

   
        public async Task<IActionResult> AllUsersPaging(int items, int pagenumber = 1)
        {
            var Users = await _userService.GetAllUsersPaging( items , pagenumber);
            if (Users.IsSuccess)
            {
                return Ok(Users);
            }
            else
            {
                return BadRequest(Users.Message);
            }

        }
        [HttpPatch("BlockOrUnBlock")]
        public async Task<IActionResult> BlockOrUnBlockUser(BlockUserDTO blockUserDTO)
        {

            var resultView = await _userService.BlockOrUnBlockUser(blockUserDTO);
            if(resultView.IsSuccess)
            {
                return Ok(resultView);
            }
             return BadRequest(resultView.Message);
        }
        [HttpGet("GetAllVendors")]
        public async Task<IActionResult> GetAllVendors( )
        {

            var resultView = await _userService.GetAllVendor();
            if (resultView.Count >0)
            {
                return Ok(resultView);
            }
            return BadRequest();
        }

        [HttpGet("GetOrdersByVindorName")]
        public async Task<IActionResult> GetOrdersBy(string VindorName)
        {

            var resultView = await _userService.GetOrdersAsyncBy(VindorName);
            if (resultView.IsSuccess)
            {
                return Ok(resultView);
            }
            return NotFound(resultView.Message);
        }
        [HttpGet("GetItemsOfOrder")]
        public async Task<IActionResult> GetItemsOfOrder(int OrderId)
        {

            var resultView = await _userService.GetAllItemsAsyncBy(OrderId);
            if (resultView.Count >0)
            {
                return Ok(resultView);
            }
            return BadRequest();
        }



    }
}
