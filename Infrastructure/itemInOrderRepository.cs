using Application.Contract;
using Application.Contracts;
using Context;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class itemInOrderRepository :Repository<itemsInOrdercs, int> , IitemInOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public itemInOrderRepository(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }
    }
}
