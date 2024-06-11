using Application.Service.Order;
using Application.Services;
using DTOs.Orders;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace UserPresentation.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(List<OrderItemDTO> selectedItems)
        {
            await _orderService.CreateOrder(selectedItems, "161a8438-3e24-4cec-94fa-eba90a88d628");
            return Ok();
        }
    }
}
