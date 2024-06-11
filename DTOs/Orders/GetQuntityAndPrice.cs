using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Orders
{
    public class GetQuntityAndPrice
    {
        public int ItemId { get; init; }
        public decimal price { get; init; }
        public int Quantity { get; init; }

    }
}
