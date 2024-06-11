using Application.Contract;
using Context;
using DTOs.Orders;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class OrderRepository:Repository<Order, int>, IOrderRepository
    {
   
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }
       
    }
}
