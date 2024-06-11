using Application.Contract;
using Context;
using DTO_s.ViewResult;
using DTOs.UserDTOs;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AddressRepository : Repository<Address, int>, IAddressRepository
    {
        private readonly ApplicationDbContext _context;
        public AddressRepository(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }

        public async Task<Images> GetfirstImageForProduct(int proID)
        {
            var image =   _context.images.FirstOrDefault(x=>x.productID== proID);
            return image;
        }
    }
}
