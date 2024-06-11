using DTO_s.ViewResult;
using DTOs.Orders;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Model;

namespace Application.Service.User
{
    public interface IuserService
    {
   
        Task<ResultView<GetAllUserDTO>> LoginAsync(UserLoginDTO userDto);
        Task<ResultView<UserRegisterDTO>> Registration(UserRegisterDTO account, string? RoleName);
        Task<bool> LogoutUser();
        Task<ResultView<BlockUserDTO>> BlockOrUnBlockUser(BlockUserDTO blockUserDTO);
         Task<ResultView<List<GetAllUserDTO>>> GetAllUsers();
         Task<ResultView<List<GetAllUserDTO>>> GetAllUsersPaging(int items, int pagenumber);
        Task<ResultDataList<GetAllVendorsDTO>> GetAllVendor();
        Task<ResultView<List<GetAllOrders>>> GetOrdersAsyncBy(string VendorName);
        Task<ResultDataList<GetAllitemsOfOrder>> GetAllItemsAsyncBy(int OrderID);

        




    }
}