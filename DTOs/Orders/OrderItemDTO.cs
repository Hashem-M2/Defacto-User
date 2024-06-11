using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Orders
{
    public class OrderItemDTO
    {
        public int itemID { get; init; }
        public int QuantityOfItem { get; init; }
        public bool IsSelected { get; set; }

    }
}
