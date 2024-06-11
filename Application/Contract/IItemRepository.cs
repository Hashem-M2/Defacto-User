using Application.Contracts;
using DTOs.Item;
using DTOs.Orders;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract
{
    public interface IItemRepository :IRepository<Item, int>
    {
        Task<(bool, Item?)> checkExistItem(ItemDto itemDto);
        Task<IQueryable<Item>> getListWithColorAndSix();
        Task<List<OrderItemDTO>> FilterBySelected(List<OrderItemDTO> OrderItemDTO);
        Task<List<Item>> GetAllItemsBy(List<OrderItemDTO> OrderItemDTO);
    }
}
