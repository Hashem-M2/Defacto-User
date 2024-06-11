using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class itemsInOrdercs :BaseEntity
    {
       
        public int QuantityOfItem { get; set; }
        public decimal TotalpriceForItem { get; set; }


        [ForeignKey("Order")]
        public int orderID { get; set; }
        public Order Order { get; set; }

        [ForeignKey("item")]
        public int itemID { get; set; }
        public Item item { get; set; }

    }
}
