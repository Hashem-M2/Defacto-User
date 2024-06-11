using Application.Contracts;
using DTO_s.ViewResult;
using DTOs.Orders;
using DTOs.UserDTOs;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract
{
    public interface IUserRepository 
    {
    
        // update Account
        Task<ResultView<UserRegisterDTO>> UserUpdate(UserRegisterDTO user);
        // soft delete Account
        Task<ResultView<UserRegisterDTO>> softDelete(string userID);
        Task<List<GetAllVendorsDTO>> GetAllVendors(string role);
        Task<List<GetAllOrders>> GetAllOrdersAsyncBy(string AdminOrVendorName);
        Task<List<GetAllitemsOfOrder>> GetAllItemsAsyncBy(int OrderId);



    }
}
