using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Orders
{

    [NotMapped]
    public record GetAllOrders
    {
        public int Id { get; init; }
        public DateTime DateTime { get; init; }
        public int Quantity { get; init; }
        public decimal TotalPrice { get; init; }
        public OrderStatus State { get; init; }
        public string CustomerName { get; set; }
    }
}
