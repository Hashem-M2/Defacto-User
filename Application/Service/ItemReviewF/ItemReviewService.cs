using Application.Contract;
using AutoMapper;
using DTO_s.ViewResult;
using DTOs.Review;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.ItemReviewF
{
    public class ItemReviewService : IItemReviewService
    {
        private readonly IItemReviewRepository _itemReviewRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _UserManager;
        private readonly IMapper _mapper;

        public ItemReviewService(IItemReviewRepository itemReviewRepository
            , IItemRepository itemRepository,IUserRepository userRepository
            , IMapper mapper, UserManager<AppUser> userManager)
        {
            _itemReviewRepository = itemReviewRepository;
            _itemRepository = itemRepository;
            _userRepository = userRepository;
            _UserManager= userManager;
            _mapper = mapper;
            
        }
        public async  Task<ResultView<ItemReviewDTO>> Create(ItemReviewDTO review)
        {
            var item = await _itemRepository.GetByIdAsync(review.itemID);
            var user = await _UserManager.FindByIdAsync(review.CustomerID);
           if(user == null || item== null)
            {
                return new ResultView<ItemReviewDTO>
                {
                    IsSuccess = false,
                    Entity = null,
                    Message = "not valid item or user "
                };
            }
            // if found the review update 
            var foundRev = await _itemReviewRepository.GetReviewforUserAsync(review);
            if(foundRev != null)
            {
                foundRev.ReviewMessage = review.ReviewMessage;
                
                await _itemRepository.SaveChangesAsync();

            }

            var itm = _mapper.Map<Item>(review);
            await  _itemRepository.CreateAsync(itm);
            await _itemRepository.SaveChangesAsync();

            return new ResultView<ItemReviewDTO>
            {
                Entity = review,
                IsSuccess = true,
                Message = "Add review  Successfully"
            };
       
        }

        public async Task<ResultDataList<ItemReviewDTO>> ItemReviews(int itemID)
        {
            var itemlist = await _itemReviewRepository.GetallReviewByItemIdAsync(itemID);
        
            var list = _mapper.Map<List<ItemReviewDTO>>(itemlist);

            return new ResultDataList<ItemReviewDTO> { 
            
                    Entities  = list,
                    Count = list.Count()
            };
        }

        public async Task<ResultView<ItemReviewDTO>> rateItem(ItemReviewDTO review)
        {

            var item = await _itemRepository.GetByIdAsync(review.itemID);
            var user = await _UserManager.FindByIdAsync(review.CustomerID);
            if (user == null || item == null)
            {
                return new ResultView<ItemReviewDTO>
                {
                    IsSuccess = false,
                    Entity = null,
                    Message = "not valid item or user "
                };
            } 
            // get item by customer id and   item Id
            var foundRev = await _itemReviewRepository.GetReviewforUserAsync(review);
            if (foundRev != null)
            {
                foundRev.ReviewRate = review.ReviewRate;
                await _itemRepository.SaveChangesAsync();

            }
            var itm = _mapper.Map<Item>(review);
            await _itemRepository.CreateAsync(itm);
            await _itemRepository.SaveChangesAsync();

            return new ResultView<ItemReviewDTO>
            {
                Entity = review,
                IsSuccess = true,
                Message = "Rate  Successfully"
            };



        }
    }
}
