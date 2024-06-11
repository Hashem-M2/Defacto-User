using DTO_s.Product;
using DTO_s.ViewResult;
using DTOs.Orders;
using DTOs.Product;
using DTOs.ProductFilter;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IProductService
    {
        
        Task<ResultDataList<ProductDTO>> GetAll();
        Task<ResultDataList<ProductDTO>> GetAllPagination(int items, int pagenumber);
        Task<ResultDataList<ProductDTO>> GetAllByCatIdwithPaging(int catId, int pagenumber, int items);
        // get all by filter model 
        Task<ResultDataList<ProductForFitlter>> GetAllProductByFilterModel(AjaxFilterDTO filterDTO, string searchTxt, int pagenumber, int items = 12);
        Task<ResultDataList<ProductForFitlter>> GetAllProductBySubcategory(SubCategory subCategory,string searchTxt, string catName, int pagenumber, int items = 12);
        Task<ResultDataList<ProductDTO>> GetProductListByIdList(List<int> proids);
        Task<ResultDataList<ProductDTO>> GeProductsByVendorID(int items, int pagenumber, string VendorOrAdminID);
        Task<ResultView<ProductDTO>> GetOne(int ID);
        Task<ResultView<ProductDTO>> Create(ProductDTO product);
        Task<ResultView<CreateOrUpdateProductDTO>> CreateWithImage(CreateOrUpdateProductDTO product);
        //  Search By product Name 
        Task<ResultView<CreateOrUpdateProductDTO>> UpdateProduct(CreateOrUpdateProductDTO product);
        Task<ResultView<ProductDTO>> acceptVendorProduct(ProductDTO product);
        Task<ResultView<ProductDTO>> HardDelete(ProductDTO product);
        Task<ResultView<ProductDTO>> SoftDelete(int productID);
        Task<ResultDataList<ProductFavoriteDTO>> ProductsInFavorite(List<string> ProductIds);
        Task<List<ProductFavoriteDTO>> GetFilteredProducts(string searchText, List<ProductFavoriteDTO> productFavoriteDTOs);
        Task<ResultDataList<GetCart>> ProductsInCart(List<string> ProductIds);
        Task<GetQuntityAndPrice> GetProductsQuantityBy(int ProductId, string ColorName, string SizeName);
        Task<List<string>> GetProductsSizesBy(int ProductId, string ColorName);
    }
}
