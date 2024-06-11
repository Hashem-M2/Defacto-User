using Application.Contract;
using AutoMapper;
using Context;
using DTO_s.ViewResult;
using DTOs.Orders;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<GetAllVendorsDTO>> GetAllVendors(string role)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(role);

            var vendors = usersInRole
                .Select(u => new GetAllVendorsDTO
                {
                    UserName = u.UserName,
                    Email = u.Email,
                    IsBlocked = u.IsBlocked
                }).ToList();

            return vendors;
        }


        public async Task<List<GetAllOrders>> GetAllOrdersAsyncBy(string AdminOrVendorName)
        {
            var adminOrVendor = await _userManager.FindByNameAsync(AdminOrVendorName);

            var orderUserNamesQuery = from order in _context.orders
                                      join iteminorder in _context.ItemsInOrdercs on order.Id equals iteminorder.orderID
                                      join item in _context.items on iteminorder.itemID equals item.Id
                                      join product in _context.products on item.productID equals product.Id
                                      where product.VendorOrAdminID == adminOrVendor.Id
                                      select new GetAllOrders
                                      {
                                          Id = order.Id,
                                          DateTime = order.dateTime,
                                          Quantity = iteminorder.QuantityOfItem,
                                          TotalPrice = iteminorder.TotalpriceForItem,
                                          State = order.State,
                                          CustomerName = order.CustomerID
                                      };
            var distinctOrders = orderUserNamesQuery.Distinct().ToList();


            foreach (var order in distinctOrders)
            {
                var user = await _userManager.FindByIdAsync(order.CustomerName);
                if (user != null)
                {
                    order.CustomerName = user.UserName;
                }
            }

            return distinctOrders;
        }



        public Task<ResultView<UserRegisterDTO>> softDelete(string userID)
        {
            throw new NotImplementedException();
        }

        public Task<ResultView<UserRegisterDTO>> UserUpdate(UserRegisterDTO user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetAllitemsOfOrder>> GetAllItemsAsyncBy(int OrderId)
        {
            var items = from order in _context.orders
                        join tems in _context.ItemsInOrdercs on order.Id equals tems.orderID
                        join ite in _context.items on tems.itemID equals ite.Id
                        join color in _context.colors on ite.ColorID equals color.Id
                        join size in _context.sizes on ite.SizeID equals size.Id
                        join prod in _context.products on ite.productID equals prod.Id
                        where OrderId == order.Id
                        select new GetAllitemsOfOrder
                        {
                            Color = color.Name,
                            ProductName = prod.Title,
                            QuantityOfItem = tems.QuantityOfItem,
                            TotalpriceForItem = tems.TotalpriceForItem,
                            Size = size.Name
                        };
            return await items.ToListAsync();
        }
    }
}
