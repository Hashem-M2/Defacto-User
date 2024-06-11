using Application.Contract;
using Application.Service.User;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace UserPresentation.Controllers
{
  
    public class AccountController : Controller
    {

        private readonly IuserService _userService;
        private readonly IConfiguration _configuration;
        public AccountController(IuserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        // Login 

        public async Task<IActionResult> Login(UserLoginDTO loginDTO, string returnUrl)
        {
    
            ViewBag.message = "";
            var result = await _userService.LoginAsync(loginDTO);
            if (result.IsSuccess)
            {
                ViewBag.message = result.Message;
                return Redirect(returnUrl);
            }
            else
            {
                ViewBag.message = result.Message;
                return Redirect(returnUrl);
            }
         
        }
            // Register 
        public async Task<IActionResult> Register(UserRegisterDTO registerDTO,string firstName, string lastName, string returnUrl)
        {
            ViewBag.message = "";
            registerDTO.UserName = firstName+lastName;
            var result = await _userService.Registration(registerDTO,"user");
         
            if (result.IsSuccess)
            {
                ViewBag.message = result.Message;
                return Redirect(returnUrl);
            }
            else
            {
                ViewBag.message = result.Message;
                return Redirect(returnUrl);
            }
        }
        public async Task<IActionResult> logOut()
        {
          await _userService.LogoutUser();
            return RedirectToAction("index","home");
        }
    }
}
