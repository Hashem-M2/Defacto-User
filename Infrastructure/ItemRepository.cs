using Application.Contract;
using Context;
using DTOs.Item;
using DTOs.Orders;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ItemRepository : Repository<Item, int>, IItemRepository
    {
        private readonly ApplicationDbContext _context;
        public ItemRepository(ApplicationDbContext Context) : base(Context)
        {
            _context = Context;
        }
        // check it item exist before 
        public async Task<(bool,Item?)> checkExistItem(ItemDto itemDto)
        {
            var searchresult = await _context.items.FirstOrDefaultAsync(x => x.productID == itemDto.productID && x.ColorID == itemDto.ColorID);
            if (searchresult == null) { 
              return (false,null);// add new item
            }

            if (searchresult != null && itemDto.SizeID == null)
            {
                return (false,null); // add new item
            }
            else if (searchresult != null && itemDto.SizeID != null)
            {
                if(searchresult.SizeID == itemDto.SizeID)
                {
                    return (true,searchresult); // update 
                }
                else
                {
                    return (false, null);// add new item
                }
            }

            return (false, null);

        }


        public async Task<IQueryable<Item>> getListWithColorAndSix()
        {
           return  _context.items.Include(x => x.color).Include(s => s.size);
        }
        public async Task<List<OrderItemDTO>> FilterBySelected(List<OrderItemDTO> OrderItemDTO)
        {
            var SelectedItems = OrderItemDTO.Where(i => i.IsSelected == true).ToList();
            return SelectedItems;
         }
        public async Task<List<Item>> GetAllItemsBy(List<OrderItemDTO> OrderItemDTO)
        {
            var SelectedItems = await FilterBySelected(OrderItemDTO);
            var res = await _context.items.Where( item => SelectedItems.Select(i => i.itemID).Contains(item.Id)).ToListAsync();
            return res;
        }

    }
}
