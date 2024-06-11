using Application.Service.Order;
using DTO_s.Product;
using DTO_s.ViewResult;
using DTOs.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.ItemF
{
    public interface IItemService
    {

        Task<ResultView<ItemDto>> GetByID(int ID);
        Task<ResultDataList<ItemListDTO>> GetAll();
        Task<ResultDataList<ItemListDTO>> GetAllPagination(int items, int pagenumber);
        Task<ResultDataList<ItemListDTO>> GetListOfItemForProduct(int items, int pagenumber, int ProID);
        Task<ResultDataList<itemForOrderDTO>> GetListOfItemForOrder(int OrderID, int items, int pagenumber);
        Task<ResultView<ItemDto>>  Create (ItemDto Item);
        Task<ResultView<ItemDto>> Update(ItemDto Item);
        Task<ResultView<ItemDto>> HDelete(int Itemid);
        Task<ResultView<ItemDto>> SDelete(int Itemid);
    }
}
