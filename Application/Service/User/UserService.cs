
using Application.Contract;
using Application.Contracts;
using AutoMapper;
using DTO_s.Category;
using DTO_s.Product;
using DTO_s.ViewResult;
using DTOs.Orders;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.User
{
    public class UserService : IuserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly UserManager<AppUser> _UserManager;
        private readonly SignInManager<AppUser> _SignInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private new List<string> _allowedExtensions = new List<string> { ".png", ".jpg", ".jpeg" };

        public UserService(IUserRepository userRepository
            , IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager, IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _SignInManager = signInManager;
            _roleManager = roleManager;
            _UserManager = userManager;
            _mapper = mapper;
            _addressRepository = addressRepository;

        }
        public async Task<bool> CheckOrCreateRole()
        {
            var result = false;


            bool ExistsAdminRole = await _roleManager.RoleExistsAsync("admin");



            if (!ExistsAdminRole)
            {
                await _roleManager.CreateAsync(new IdentityRole("admin"));
                result = true;
            }
            bool ExistsUserRole = await _roleManager.RoleExistsAsync("Vendor");
            if (!ExistsUserRole)
            {
                await _roleManager.CreateAsync(new IdentityRole("Vendor"));
                result = true;
            }
            bool ExistsVendorRole = await _roleManager.RoleExistsAsync("user");
            if (!ExistsVendorRole)
            {
                await _roleManager.CreateAsync(new IdentityRole("user"));
                result = true;
            }

            return result;
        }

        public async Task<ResultView<UserRegisterDTO>> Registration(UserRegisterDTO account, string? RoleName = "user")
        {

            // function check if the role(vendor - user  - Admin ) not created  will create it 
            await CheckOrCreateRole();
            var existUserEmail = await _UserManager.FindByEmailAsync(account.Email);
            var existUserName = await _UserManager.FindByNameAsync(account.UserName);

            if (existUserName != null || existUserEmail != null)
            {
                return new ResultView<UserRegisterDTO>()
                {
                    Entity = account,
                    IsSuccess = false,
                    Message = " Already Exist"
                };
            }
            var userModel = _mapper.Map<AppUser>(account);


            // create address _userRepository
            var userAddress = _mapper.Map<Address>(account);

            var address = await _addressRepository.CreateAsync(userAddress);
            await _addressRepository.SaveChangesAsync();
            userModel.addressID = address.Id;
            /*if (!_allowedExtensions.Contains(Path.GetExtension(account.userImage.FileName).ToLower()) && account.userImage != null)
            {
                return new ResultView<UserRegisterDTO> { Entity = null, IsSuccess = false, Message = "The Allowed Extensions is .png and .jpg" };

            }
            using var datastream = new MemoryStream();
            await account.userImage.CopyToAsync(datastream);
            var ProfileByts = datastream.ToArray();
            string ProfileByts64String = Convert.ToBase64String(ProfileByts);
            userModel.ProfileImage = ProfileByts64String;*/

            var result = _UserManager.CreateAsync(userModel, account.password);
            if (result.Result.Succeeded == false)
            {
                return new ResultView<UserRegisterDTO>()
                {
                    Entity = account,
                    IsSuccess = false,


                    Message = result.Result.ToString()
                };
            }



            //if role name == "user" create user account/
            if (RoleName.ToLower() == "user")
            {
                await _UserManager.AddToRoleAsync(userModel, "user");
            }
            else if (RoleName.ToLower() == "admin")
            {
                await _UserManager.AddToRoleAsync(userModel, "admin");
            }
            else if (RoleName.ToLower() == "vendor")
            {
                await _UserManager.AddToRoleAsync(userModel, "vendor");
            }

            return new ResultView<UserRegisterDTO>()
            {
                Entity = account,
                IsSuccess = true,
                Message = " Successfully create Account "

            };



        }

        public async Task<ResultView<GetAllUserDTO>> LoginAsync(UserLoginDTO userDto)
        {
            var oldUser = await _UserManager.FindByEmailAsync(userDto.Email);

            if (oldUser == null)
            {
                return new ResultView<GetAllUserDTO> { Entity = null, Message = "Email not found", IsSuccess = false };
            }
            if (oldUser.IsBlocked == true)
            {
                return new ResultView<GetAllUserDTO> { Entity = null, Message = "Blocked User", IsSuccess = false };
            }

            var result = await _SignInManager.CheckPasswordSignInAsync(oldUser, userDto.password, lockoutOnFailure: false);
         
            if (result.Succeeded)
            {
                var userRoles = await _UserManager.GetRolesAsync(oldUser);
                var roleName = userRoles.FirstOrDefault();
                GetAllUserDTO userObj = new GetAllUserDTO()
                {
                    Email = userDto.Email,
                    Id = oldUser.Id,
                    IsBlocked = oldUser.IsBlocked,
                    Role = roleName,
                    UserName = oldUser.UserName
                };
                 await _SignInManager.SignInAsync(oldUser, userDto.rememberMe);
                return new ResultView<GetAllUserDTO> { Entity = userObj, Message = "Login Successfully", IsSuccess = true };
            }

            return new ResultView<GetAllUserDTO> { Entity = null, Message = "Invalid password", IsSuccess = false };
        }





        public async Task<bool> LogoutUser()
        {
            await _SignInManager.SignOutAsync();
            return true;
        }
        public async Task<ResultView<BlockUserDTO>> BlockOrUnBlockUser(BlockUserDTO blockUserDTO)
        {
            var user = await _UserManager.FindByIdAsync(blockUserDTO.Id);

            if (user == null)
            {
                return new ResultView<BlockUserDTO> { Entity = null, IsSuccess = false, Message = "Unable to find the user." };
            }

            if (user.IsBlocked == blockUserDTO.IsBlocked)
            {
                return new ResultView<BlockUserDTO>
                {
                    Entity = blockUserDTO, IsSuccess = false, Message = user.IsBlocked ? "The user is already blocked." : "The user is already unblocked."
                };
            }

            user.IsBlocked = blockUserDTO.IsBlocked;

            var result = await _UserManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return new ResultView<BlockUserDTO>
                {
                    Entity = blockUserDTO, IsSuccess = true, Message = user.IsBlocked ? "User blocked successfully." : "User unblocked successfully."
                };
            }
            return new ResultView<BlockUserDTO> { Entity = null, IsSuccess = false, Message = "Failed to update user." };

        }

        public async Task<ResultView<List<GetAllUserDTO>>> GetAllUsers()
        {
            var users = _UserManager.Users;


            if (users == null)
            {
                return new ResultView<List<GetAllUserDTO>>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "No users found."
                };
            }
            var userlist = await users.ToListAsync();
            var userDTOs = _mapper.Map<List<GetAllUserDTO>>(userlist);

            return new ResultView<List<GetAllUserDTO>>
            {
                Entity = userDTOs,
                IsSuccess = true,
                Message = "Successfully retrieved all users."
            };
        }

        public async Task<ResultView<List<GetAllUserDTO>>> GetAllUsersPaging(int items, int pagenumber) // 12  -1
        {
            var AlldAta = (_UserManager.Users);
            if (AlldAta == null)
            {
                return new ResultView<List<GetAllUserDTO>>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "No users found."
                };
            }
            var userlist = await AlldAta/*.Where(x => x.IsBlocked == false || x.IsBlocked == null)*/.Skip(items * (pagenumber - 1)).Take(items).ToListAsync();
            var userDTOs = _mapper.Map<List<GetAllUserDTO>>(userlist);

            return new ResultView<List<GetAllUserDTO>>
            {
                Entity = userDTOs,
                IsSuccess = true,
                Message = "Successfully retrieved all users."
            };


        }
        public async Task<ResultDataList<GetAllVendorsDTO>> GetAllVendor()
        {
            var allVendors = await _userRepository.GetAllVendors("vendor");

            if (allVendors == null || !allVendors.Any())
            {
                return new ResultDataList<GetAllVendorsDTO>
                {
                    Count = 0,
                    Entities = null
                };
            }

            return new ResultDataList<GetAllVendorsDTO>
            {
                Count = allVendors.Count,
                Entities = allVendors
            };
        }

        public async Task<ResultDataList<GetAllitemsOfOrder>> GetAllItemsAsyncBy(int OrderID)
        {
            var AllItems = await _userRepository.GetAllItemsAsyncBy(OrderID);
            if (AllItems is null)
            {
                return new ResultDataList<GetAllitemsOfOrder>
                {
                   Count = 0,
                   Entities = AllItems
                };
            }



            return new ResultDataList<GetAllitemsOfOrder>
            {
                Count = AllItems.Count,
                Entities = AllItems
            };

        }

        public async Task<ResultView<List<GetAllOrders>>> GetOrdersAsyncBy(string VendorName)
        {
            var vendor = await _UserManager.FindByNameAsync(VendorName);
            if ( vendor is null)
            {
                return new ResultView<List<GetAllOrders>>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "No Vendor found By This Name."
                };
            }
            var Orders = await _userRepository.GetAllOrdersAsyncBy(VendorName);

            return new ResultView<List<GetAllOrders>>
                
            {
                Entity = Orders,
                IsSuccess = true,
                Message = "Successfully retrieved Orders."
            };

        }
    }
} 

