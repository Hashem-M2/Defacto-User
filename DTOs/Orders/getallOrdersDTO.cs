using DTOs.Item;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Orders
{
    public record getallOrdersDTO
    {
        public int Id { get; init; }
        public DateTime DateTime { get; init; }
        public int Quantity { get; init; }
        public decimal TotalPrice { get; init; }
        public OrderStatus State { get; init; }
        public string CustomerName { get; set; }

        public List<ItemDto>? items { get; init; }
    }
}
