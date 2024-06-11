using Application.Service.ItemF;
using Application.Service.Order;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace AdminAndVendordashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IItemService _itemService;
        public OrderController(IOrderService orderService,IItemService itemService)
        {
            this._orderService = orderService;
            this._itemService = itemService;
        }
        [HttpGet]
        [Route("getDefactoOrders")]
        public async Task<IActionResult> GetDefactoOrders(int items, int pageNo)
        {
             var orders = await _orderService.getallDefactoOrdersAsync(items, pageNo);
            if (orders.Count ==0)
            {
                return Empty;
            }

            return Ok(orders);
        } 
        
        [HttpGet]
        [Route("getItemForOrder/{OrderID}")]
        public async Task<IActionResult> GetDefactoOrders( int OrderID,int items, int pageNo)
        {
             var itemsModel = await _itemService.GetListOfItemForOrder(OrderID,items, pageNo);
            if (itemsModel.Count ==0)
            {
                return Empty;
            }

            return Ok(itemsModel);
        }
        [HttpGet]
        [Route("changeOrderState/{OrderID}")]
        public async Task<IActionResult> changeState( int OrderID , OrderStatus Status)
        {
             var itemsModel = await _orderService.changeOrderState(OrderID, Status);
            if(itemsModel.Item1 == true)
            {
                return Ok(itemsModel.Item2);
            }

            return BadRequest(itemsModel.Item2);
        }

        


    }
}
