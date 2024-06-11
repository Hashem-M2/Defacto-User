
using Application.Contracts;
using Context;
using DTOs.Orders;
using DTOs.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ProductRepository : Repository<Product, int>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }

        public async Task<Product> GetProductAndCatgoryByIDAsync(int proID)
        {
            return await _context.products.Include(x=>x.category).FirstOrDefaultAsync(pro=>pro.Id == proID);
        
        }public async Task<Product> GetProductWithImagesByIDAsync(int proID)
        {
            return await _context.products.Include(x => x.category).Include(c => c.User).Include(x=>x.images).Include(x=>x.items).FirstOrDefaultAsync(pro=>pro.Id == proID);
        }



        public async Task<List<ProductFavoriteDTO>> GetAllProductinFavoriteAsyncBy(List<int> ProductId)
        {
            var productsInFavorites = await _context.products
                .Include(p => p.images)
                .Include(p => p.items)
                .Where(p => ProductId.Contains(p.Id))
                .Select(p => new ProductFavoriteDTO
                {
                    Id = p.Id,
                    Title = p.Title,
                    Image = p.images.Select(i => i.imagepath).FirstOrDefault(), 
                    price = p.items.Select(i => i.Price).FirstOrDefault()      
                })
                .ToListAsync();

            return productsInFavorites;
        }


        public async Task<List<GetCart>> GetAllProductsInCartAsyncBy(List<int> ProductId)
        {
            var productsInCart = await _context.products
                .Include(p => p.images)
                .Include(p => p.items)
                .Where(p => ProductId.Contains(p.Id))
                .Select(p => new GetCart
                {
                    Id = p.Id,
                    Title = p.Title,
                    Image = p.images.Select(i => i.imagepath).FirstOrDefault(),
                    price = p.items.Select(i => i.Price).FirstOrDefault(),
                    ColorsName = p.items.Select(p => p.color.Name).Distinct().ToList(),
                })
                .ToListAsync();

            return productsInCart;
        }
        public async Task<List<string>> GetAllSizesBy(int ProductId, string ColorName)
        {
            var colorId = await _context.colors
                .Where(c => c.Name == ColorName)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            var sizes = (from p in _context.products
                         join i in _context.items on p.Id equals i.productID
                         join c in _context.colors  on i.ColorID equals c.Id
                         join s in _context.sizes on i.SizeID equals s.Id
                         where(i.productID == ProductId && i.ColorID == colorId)
                         select i.size.Name)
             .Distinct()
             .ToList();

            return sizes;
        }
        public async Task<GetQuntityAndPrice> GetQuantityBy(int ProductId, string ColorName, string SizeName)
        {
            var item = await _context.items
      .Include(i => i.color)
      .Include(i => i.size)
      .FirstOrDefaultAsync(i =>
          i.productID == ProductId &&
          i.color.Name == ColorName &&
          (SizeName == null || i.size.Name == SizeName));

            if (item != null)
            {
                return new GetQuntityAndPrice
                {
                    price = item.Price,
                    Quantity = item.Quantity,
                    ItemId = item.Id
                };
            }
            else
            {
                return null;
            }
        }


    }
}
