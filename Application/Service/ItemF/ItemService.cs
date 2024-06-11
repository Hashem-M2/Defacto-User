using Application.Contract;
using Application.Contracts;
using Application.Service.Order;
using Application.Services;
using AutoMapper;
using DTO_s.Category;
using DTO_s.ViewResult;
using DTOs.Item;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.ItemF
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;
        private readonly IitemInOrderRepository _itemInOrderRepo;
        private readonly IProductRepository _productRepository;
        public ItemService(IMapper mapper, IItemRepository repository, IitemInOrderRepository itemInOrderRepo, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _itemRepository = repository;
            _itemInOrderRepo = itemInOrderRepo;
        }

        public async Task<ResultView<ItemDto>> Create(ItemDto ItemDto)
        {
          var esixtItem   = await _itemRepository.checkExistItem(ItemDto);

            if(esixtItem.Item1 == true) {

                // update  and add new 
                esixtItem.Item2.Quantity += ItemDto.Quantity;
                await _itemRepository.SaveChangesAsync();
                 var itemAfterUpdate = _mapper.Map<ItemDto>(esixtItem.Item2);
                return new ResultView<ItemDto> { 
                    Entity = itemAfterUpdate, IsSuccess = false,
                    Message = "Already Exist item Updated Quantity but the price not updated"
                };

            }
            else
            {  // insert 
                var produce = await _productRepository.GetProductAndCatgoryByIDAsync(ItemDto.productID);

                if(produce == null)
                {
                    return new ResultView<ItemDto>
                    {
                        Entity = ItemDto,
                        IsSuccess = false,
                        Message = "Not correct Product"
                    };
                }


                string header = (produce.Title + " "+ ItemDto.ColorName + " " + produce.productGender.ToString() + " " + produce.category.Name + " " + produce.category.subCategory.ToString() + " " + ItemDto.SizeName);
                
                var Itm = _mapper.Map<Item>(ItemDto);
                Itm.descraption = header;
                var newItem = await _itemRepository.CreateAsync(Itm);

                              await _itemRepository.SaveChangesAsync();

                return new ResultView<ItemDto>
                {
                    Entity = ItemDto,
                    IsSuccess = true,
                    Message = "Created Successfully"
                };
              
            }
       
        }
        public async Task<ResultView<ItemDto>> Update(ItemDto ItemDto)
        {
            try
            {
                Item itm = _mapper.Map<Item>(ItemDto);
                itm.IsDeleted = false;
                itm.descraption = $"{ItemDto.ColorName} {ItemDto.SizeName} {ItemDto.SizeName}";
                var NewPrd = await _itemRepository.UpdateAsync(itm);
                await _itemRepository.SaveChangesAsync();

                return new ResultView<ItemDto> { Entity = ItemDto, IsSuccess = true, Message = "Updated Successfully" };
            }
            catch (Exception)
            {
                return new ResultView<ItemDto> { Entity = null, IsSuccess = false, Message = "not Found " };
            }

        }

        public async Task<ResultDataList<ItemListDTO>> GetAllPagination(int items, int pagenumber)
        {
            var allData = (await _itemRepository.getListWithColorAndSix());
            var Items = allData.Where(x => x.IsDeleted == false || x.IsDeleted == null)
                               .Skip(items * (pagenumber - 1)).Take(items).ToList();
            var itemlist = _mapper.Map<List<ItemListDTO>>(Items); 
            return new ResultDataList<ItemListDTO> { 
                Count = itemlist .Count(),
                 Entities= itemlist
            };
        }

        public async Task<ResultView<ItemDto>> GetByID(int ID)
        {
          var itm = await _itemRepository.GetByIdAsync(ID);
            if (itm == null)
            {
                return new ResultView<ItemDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Not Found"
                };
            }
            var item = _mapper.Map<ItemDto>(itm);
            return new ResultView<ItemDto>
            {
                Entity = item,
                IsSuccess = true,
                Message = "item found "
            };
        }
        public async Task<ResultDataList<itemForOrderDTO>> GetListOfItemForOrder(int OrderID,int items, int pagenumber)
        {
            


            var allData = (await _itemInOrderRepo.GetAllAsync());
            var ListOFItems = allData.Where(x => (x.IsDeleted == false || x.IsDeleted == null) && x.orderID == OrderID)
                .Include(i => i.item).ThenInclude(c => c.color).Include(z => z.item.size).Select(i => new itemForOrderDTO
                {
                    Id = i.item.Id,
                    ColorName = i.item.color.Name,
                    SizeName = i.item.size.Name,
                    Quantity = i.QuantityOfItem,
                    Price = i.item.Price
                }).ToList();

            return new ResultDataList<itemForOrderDTO>
            {
                Count = ListOFItems.Count(),
                Entities = ListOFItems
            };
        }
        public async Task<ResultDataList<ItemListDTO>> GetListOfItemForProduct(int items, int pagenumber, int ProID)
        {
            var allData = (await _itemRepository.GetAllAsync());
            var Items = allData.Include(x=>x.color).Include(x=>x.size).Where(x => (x.IsDeleted == false || x.IsDeleted == null)&& x.productID ==ProID)
                               .Skip(items * (pagenumber - 1)).Take(items).ToList();
            var itemlist = _mapper.Map<List<ItemListDTO>>(Items);
            return new ResultDataList<ItemListDTO>
            {
                Count = itemlist.Count(),
                Entities = itemlist
            };
        }

        public async Task<ResultView<ItemDto>> HDelete(int Itemid)
        {

            try
            {
                 var itm = await _itemRepository.GetByIdAsync(Itemid);
;                await _itemRepository.DeleteAsync(itm);
                await _itemRepository.SaveChangesAsync();
                var itemDto = _mapper.Map<ItemDto>(itm);
                return new ResultView<ItemDto>
                {
                    Entity = itemDto,
                    IsSuccess = true,
                    Message = "Deleted Successfully"
                };
            }
            catch (Exception ex)
            {

                return new ResultView<ItemDto> { Entity = null, IsSuccess = false, Message = ex.Message };
            }
            
        }

        public async Task<ResultView<ItemDto>> SDelete(int Itemid)
        {
            try
            {
                var itemObj = await _itemRepository.GetByIdAsync(Itemid);
                if (itemObj == null)
                {
                    return new ResultView<ItemDto>
                    {
                        Entity =null,
                        IsSuccess = false,
                        Message = "Can't  Deleted "
                    };
                }
                itemObj.IsDeleted =true;    
                 
                await _itemRepository.SaveChangesAsync();
                var itemDto = _mapper.Map<ItemDto>(itemObj);
                return new ResultView<ItemDto>
                {
                    Entity = itemDto,
                    IsSuccess = true,
                    Message = "Deleted Successfully"
                };
            }
            catch (Exception ex)
            {

                return new ResultView<ItemDto> { Entity = null, IsSuccess = false, Message = ex.Message };
            }
        }

        public async   Task<ResultDataList<ItemListDTO>> GetAll()
        {
            var allData = (await _itemRepository.getListWithColorAndSix());
            var Items = allData.ToList();
            var itemlist = _mapper.Map<List<ItemListDTO>>(Items);
            return new ResultDataList<ItemListDTO>
            {
                Count = itemlist.Count(),
                Entities = itemlist
            };
        
        }

       
    }
}
 