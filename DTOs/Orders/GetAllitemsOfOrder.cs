using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Orders
{
    public record GetAllitemsOfOrder
    {
        public int QuantityOfItem { get; set; }
        public decimal TotalpriceForItem { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }




    }
}
