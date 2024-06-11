using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    public class Order : BaseEntity
    {
        public DateTime dateTime { get; set; }
        public int Quantity { get; set; }
        // bool  state

        public OrderStatus State { get; set; } = OrderStatus.Processing;
        public decimal Totalprice { get; set; } = 0;
        [ForeignKey("User")]
        public string CustomerID { get; set; }
        public AppUser User { get; set; }
        public ICollection<itemsInOrdercs> itemsInOrdercs { get; set; }


        public Order()
        {
            itemsInOrdercs = new List<itemsInOrdercs>();
        }
    }
}
