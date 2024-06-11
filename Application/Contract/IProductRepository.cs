using DTO_s.ViewResult;
using DTOs.Orders;
using DTOs.Product;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<Product> GetProductWithImagesByIDAsync(int proID);
        Task<List<ProductFavoriteDTO>> GetAllProductinFavoriteAsyncBy(List<int> ProductId);
        Task<Product> GetProductAndCatgoryByIDAsync(int proID);
        Task<List<GetCart>> GetAllProductsInCartAsyncBy(List<int> ProductId);
        Task<GetQuntityAndPrice> GetQuantityBy(int ProductId, string ColorName, string SizeName);
        Task<List<string>> GetAllSizesBy(int ProductId, string ColorName);
    }
}
