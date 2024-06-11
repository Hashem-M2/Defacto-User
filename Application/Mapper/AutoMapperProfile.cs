using AutoMapper;
using DTO_s.Category;
using DTO_s.Product;
using DTOs.ColorAndSize;
using DTOs.Item;
using DTOs.Orders;
using DTOs.Product;
using DTOs.Review;
using DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegisterDTO, AppUser>().ReverseMap();
            
            CreateMap<UserRegisterDTO, Address>().ReverseMap();
            
            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<ListOfCategoryDTO, Category>().ReverseMap();

            CreateMap<ItemDto, Item>().ReverseMap()
                .ForMember(dest => dest.ColorHEX, obj => obj.MapFrom(src => src.color.HEX));

            CreateMap<ItemListDTO, Item>().ReverseMap()
                  .ForMember(dest => dest.ColorName, obj => obj.MapFrom(src => src.color.Name))
                  .ForMember(dest => dest.SizeName, obj => obj.MapFrom(src => src.size.Name));

            CreateMap<Product, CreateOrUpdateProductDTO>()
                .ForMember(src => src.VendorId, obj => obj.MapFrom(dest => dest.VendorOrAdminID))
                .ReverseMap();



            CreateMap<Product, ProductDTO>()
                .ForMember(src => src.VendorId, obj => obj.MapFrom(dest => dest.VendorOrAdminID ))
                .ForMember(src => src.VendorName, obj => obj.MapFrom(dest => dest.User.UserName ))
                .ForMember(src => src.ImagesArr, obj => obj.MapFrom(dest => dest.images.Select(i=>i.imagepath).ToArray() )).ReverseMap();


            CreateMap<Product, ProductForFitlter>()
               .ForMember(src => src.VendorId, obj => obj.MapFrom(dest => dest.VendorOrAdminID))
               .ForMember(src => src.VendorName, obj => obj.MapFrom(dest => dest.User.UserName))
               .ForMember(src => src.Image, obj => obj.MapFrom(dest => dest.images.FirstOrDefault().imagepath)).ReverseMap();
            CreateMap<GetAllUserDTO, AppUser>().ReverseMap();
            CreateMap<GetAllVendorsDTO, AppUser>().ReverseMap();


            CreateMap<AppUser, GetAllVendorsDTO>().ReverseMap();

            CreateMap<ColorDTO, Color>().ReverseMap().
                ForMember(dest=>dest.NotActive, obj=>obj.MapFrom(src=>src.IsDeleted));

            CreateMap<SizeDTO, Size>().ReverseMap().
                ForMember(dest=>dest.NotActive, obj=>obj.MapFrom(src=>src.IsDeleted));


            CreateMap<ItemReviewDTO, itemReview>().ReverseMap();
            CreateMap<getallOrdersDTO, Order>().ReverseMap()   
                .ForMember(dest => dest.CustomerName, obj => obj.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.items, 
                obj => obj.MapFrom(src => src.itemsInOrdercs.Select(x=>x.item).ToList()
                ));




        }
    }
}
