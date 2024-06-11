using Application.Contract;
using Context;
using DTOs.Review;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ItemReviewRepository :Repository<itemReview,int> , IItemReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ItemReviewRepository(ApplicationDbContext context) : base(context)
        {

            this._context = context; 

        }

        public async Task<IQueryable<itemReview>> GetallReviewByItemIdAsync(int itemId)
        {
            return _context.itemReviews.Where(x => x.itemID == itemId);
        }

        // get item by customer id and   item Id

        public async Task<itemReview> GetReviewforUserAsync(ItemReviewDTO review)
        {
            
          var rev =_context.itemReviews.FirstOrDefault(i=>i.CustomerID == review.CustomerID && i.itemID == review.itemID);
        
            if(rev == null)
            {
                return null;
            }

            return rev;
        }
    }
}
