using Application.Contract;
using AutoMapper;
using DTO_s.ViewResult;
using DTOs.Orders;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model;
using DTOs.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Order
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;

       
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IItemRepository itemRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public async Task<OrderCreationResult> CreateOrder(List<OrderItemDTO> items, string customerId)
        {
            var totalQuantity = 0;
            decimal totalPrice = 0;

            var selectedItems = await _itemRepository.FilterBySelected(items);
            var itemsInOrder = new List<itemsInOrdercs>();

            foreach (var selectedItem in selectedItems)
            {
                var item = await _itemRepository.GetByIdAsync(selectedItem.itemID);
                var itemTotalPrice = selectedItem.QuantityOfItem * item.Price;

                var itemInOrder = new itemsInOrdercs
                {
                    itemID = item.Id,
                    QuantityOfItem = selectedItem.QuantityOfItem,
                    TotalpriceForItem = itemTotalPrice,
                    item = item
                   

                };
                var edititem = await _itemRepository.GetByIdAsync(itemInOrder.itemID);
                edititem.Quantity -= itemInOrder.QuantityOfItem;
                totalQuantity += selectedItem.QuantityOfItem;
                totalPrice += itemTotalPrice;

                itemsInOrder.Add(itemInOrder);
            }
            var x = await _itemRepository.SaveChangesAsync();
            var order = new Model.Order()
            {
                CustomerID = customerId,
                dateTime = DateTime.Now,
                itemsInOrdercs = itemsInOrder,
                Quantity = totalQuantity,
                Totalprice = totalPrice
            };
         await   _orderRepository.CreateAsync(order);
     var X=     await  _orderRepository.SaveChangesAsync();
            return new OrderCreationResult
            {
                Order = order,
                TotalQuantity = totalQuantity,
                TotalPrice = totalPrice
            };
        }


       
        public async Task<ResultDataList<getallOrdersDTO>> getallDefactoOrdersAsync(int items =20, int pagenumber = 1)
        {
             var allOrders =   (await _orderRepository.GetAllAsync()).Include(o => o.User).Include(o => o.itemsInOrdercs).ThenInclude(i=>i.item);
            var orders = allOrders.Where(o=>o.IsDeleted==false|| o.IsDeleted==null)
                .Skip(items * (pagenumber - 1)).Take(items).ToList();



            if (!allOrders.Any())
            {
                return new ResultDataList<getallOrdersDTO> { Count = 0, Entities = null };
            }
             var ordersDTO =_mapper.Map<List<getallOrdersDTO>>(allOrders);
            return new ResultDataList<getallOrdersDTO>
            {
                Count = orders.Count(),
                Entities = ordersDTO
            };
        }

        public async Task<(bool, string)> changeOrderState(int OrderID, OrderStatus Status)
        {
            var order = await _orderRepository.GetByIdAsync(OrderID);
            if (order == null)
            {
                return (false ,"Not correct order number");
            }
            order.State = Status;
            await _orderRepository.SaveChangesAsync();
            return (true, "order Status changed ");
        }
    }

}

    

