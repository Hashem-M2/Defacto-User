using Context.Migrations;
using DTO_s.ViewResult;
using DTOs.Item;
using DTOs.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.ItemReviewF
{
    public interface IItemReviewService 
    {
        // GET REWVIEW ber item by id 
        Task<ResultDataList<ItemReviewDTO>> ItemReviews(int itemID);
        // add review for item  
        Task<ResultView<ItemReviewDTO>> Create(ItemReviewDTO Item);

        // rate item 1-5 
        Task<ResultView<ItemReviewDTO>> rateItem(ItemReviewDTO Item);
       

    }
}
