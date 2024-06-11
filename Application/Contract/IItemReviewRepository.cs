using Application.Contracts;
using DTOs.Review;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract
{
    public interface IItemReviewRepository : IRepository<itemReview, int>
    {
        Task<IQueryable<itemReview>> GetallReviewByItemIdAsync(int itemId);
        Task<itemReview> GetReviewforUserAsync(ItemReviewDTO review);
    }
}
