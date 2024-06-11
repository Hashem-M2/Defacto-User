using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public enum OrderStatus
    {
        Shipped =0,
        Delivered=1,
        Processing=2,
        Cancelled=3,
        Placed,
        PendingPayment,
        Returned
    }
}
