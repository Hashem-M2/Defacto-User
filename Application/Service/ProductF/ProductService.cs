using Application.Contract;
using Application.Contracts;
using Application.Services;
using AutoMapper;
using DTO_s.Category;
using DTO_s.Product;
using DTO_s.ViewResult;
using DTOs.Product;
using DTOs.UserDTOs;
using DTOs.ProductFilter;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

using Newtonsoft.Json;
using DTOs.Orders;

namespace Ecommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IImageRepository _imageRepository;

        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper, IImageRepository imageRepository,ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _imageRepository = imageRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        private new List<string> _allowedExtensions = new List<string> { ".png", ".jpg", ".jpeg" };
        private long _maxAllowedPosterSize = 1048576;

        public async Task<ResultDataList<ProductDTO>> GetAllPagination(int items, int pagenumber) //10 , 3 -- 20 30
        {
            var AlldAta = (await _productRepository.GetAllAsync());
            var Prds = AlldAta.Include(x => x.category).Include(i=>i.images).Include(c => c.User)
                .Where(p => (p.IsDeleted == false || p.IsDeleted == null)).Skip(items * (pagenumber - 1)).Take(items).ToList();

            var productList = _mapper.Map<List<ProductDTO>>(Prds);
            
            ResultDataList<ProductDTO> resultDataList = new()
            {
                Entities = productList,
                Count = Prds.Count()
            };
          
            return resultDataList;
        }

        public async Task<ResultDataList<ProductDTO>> GetAllByCatIdwithPaging(int catId,int pagenumber, int items)
        {
            var cat = await _categoryRepository.GetByIdAsync(catId);
            if(cat == null)
            {
                ResultDataList<ProductDTO> emptylist = new()
                {
                    Entities = null,
                    Count = 0
                };
                return emptylist;
            }

            var AlldAta = (await _productRepository.GetAllAsync()).Include(x => x.category).Include(c => c.User);
            var Prds = AlldAta.Where(p=>p.categoryID == catId && (p.IsDeleted == false || p.IsDeleted == null))
                .Skip(items * (pagenumber - 1)).Take(items).ToList();


            if(Prds == null)
            {
                ResultDataList<ProductDTO> emptylist = new()
                {
                    Entities = null,
                    Count = 0
                };
                return emptylist;
            }
            var productList = _mapper.Map<List<ProductDTO>>(Prds);

            ResultDataList<ProductDTO> resultDataList = new()
            {
                Entities = productList,
                Count = Prds.Count()
            };

            return resultDataList;
        }
        public async Task<ResultDataList<ProductForFitlter>> GetAllProductBySubcategory(SubCategory subCategory,string searchTxt, string catName, int pagenumber, int items = 12)
        {
            string[] searchWords = [];
            if (!searchTxt.IsNullOrEmpty())
            {
                searchWords = searchTxt.ToLower().Split(' ');
            }
            
            var allData = (await _productRepository.GetAllAsync());
            var productsForFilter = allData.Include(x => x.category).Include(c => c.User).Include(i => i.images).Include(i => i.items).ThenInclude(c=>c.color)
                .Where(x => x.category.subCategory == subCategory || subCategory == 0
                && (x.IsDeleted == false || x.IsDeleted == null)
                && (x.category.Name == catName || catName.IsNullOrEmpty())
                && x.items.Any()
                && (searchTxt.IsNullOrEmpty() || x.items.Any(i => searchWords.Any(w => i.descraption.ToLower().Contains(w))))
                );

            var productsForPage = productsForFilter.Skip(items * (pagenumber - 1)).Take(items).ToList();

            var productList = _mapper.Map<List<ProductForFitlter>>(productsForPage);


            ResultDataList<ProductForFitlter> resultDataList = new ResultDataList<ProductForFitlter>()
            {
                Entities = productList,
                Count = productsForFilter.Count()
            };
            return resultDataList;
        }
       
        public async Task<ResultDataList<ProductForFitlter>> GetAllProductByFilterModel(AjaxFilterDTO filterDTO, string searchTxt, int pagenumber, int items = 12)
        {
            string[] searchWords = [];
            if (!searchTxt.IsNullOrEmpty())
            {
                searchWords = searchTxt.ToLower().Split(' ');
            }
            var allData = (await _productRepository.GetAllAsync())?
                .Include(i => i.items).ThenInclude(x=> x.size)
                .Include(i=>i.items).ThenInclude(c=>c.color);

            var productsForFilter =  allData.Include(x => x.category).Include(c => c.User).Include(i => i.images)
                .Where(x =>
                          (x.category.subCategory == filterDTO.subCategory || filterDTO.subCategory == 0)
                           && (x.IsDeleted == false || x.IsDeleted == null)
                          && (x.items.Any(p => p.Price >= filterDTO.mintPrice && p.Price <= filterDTO.maxPrice)
                           || (filterDTO.mintPrice == null) || (filterDTO.maxPrice == null) ||
                           ((filterDTO.mintPrice == null) && (filterDTO.maxPrice == null)))

                           && (filterDTO.genderArr.Contains(x.productGender) || filterDTO.genderArr.IsNullOrEmpty())

                           && (filterDTO.subCategoryArr.Contains(x.category.subCategory) || filterDTO.subCategoryArr.IsNullOrEmpty())


                           && (x.items.Any(i => filterDTO.sizeArr.Contains(i.size.Code.ToLower())) ||filterDTO.sizeArr.IsNullOrEmpty())
                           && (x.items.Any(i => filterDTO.colotArr.Contains(i.color.Name.ToLower())) ||filterDTO.colotArr.IsNullOrEmpty())
                           && x.items.Any()
                           && (searchTxt.IsNullOrEmpty() || x.items.Any(i => searchWords.Any(w => i.descraption.ToLower().Contains(w))))

                      );



                   var productsForPage = productsForFilter.Skip(items * (pagenumber - 1)).Take(items).ToList();



            var productList = _mapper.Map<List<ProductForFitlter>>(productsForPage);


            ResultDataList<ProductForFitlter> resultDataList = new ResultDataList<ProductForFitlter>()
            {
                Entities = productList,
                Count = productsForFilter.Count()
            };
        
            return resultDataList;
        }
       
        public async Task<ResultDataList<ProductDTO>> GetAll() //10 , 3 -- 20 30
        {
            var AlldAta = (await _productRepository.GetAllAsync());
            var Prds = AlldAta.Include(x => x.category).Include(c => c.User).Where(p=> (p.IsDeleted == false || p.IsDeleted == null)).ToList();

            var PrdDto = _mapper.Map<List<ProductDTO>>(Prds);
            ResultDataList<ProductDTO> resultDataList = new ResultDataList<ProductDTO>();
            resultDataList.Entities = PrdDto;
            resultDataList.Count = AlldAta.Count();
            return resultDataList;
        }
        public async Task<ResultView<CreateOrUpdateProductDTO>> CreateWithImage(CreateOrUpdateProductDTO product)
        {
            var Query = (await _productRepository.GetAllAsync()); // select * from product
            var OldProduct = Query.Where(p => p.Title == product.Title).FirstOrDefault();
            if (OldProduct != null)
            {
                return new ResultView<CreateOrUpdateProductDTO> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            if (!_allowedExtensions.Contains(Path.GetExtension(product.ProductImage1.FileName).ToLower()) ||
                !_allowedExtensions.Contains(Path.GetExtension(product.ProductImage2.FileName).ToLower()) ||
                !_allowedExtensions.Contains(Path.GetExtension(product.ProductImage3.FileName).ToLower()) ||
                !_allowedExtensions.Contains(Path.GetExtension(product.ProductImage4.FileName).ToLower()))
            {
                return new ResultView<CreateOrUpdateProductDTO> { Entity = null, IsSuccess = false, Message = "The Allowed Extensions is .png and .jpg" };
            }
            else
            {
                 var Prd = _mapper.Map<Product>(product);
                Prd.IsApproved = false;
                Prd.IsDeleted = false;
                var NewPrd = await _productRepository.CreateAsync(Prd);
                await _productRepository.SaveChangesAsync();


                try
                {
                    using var datastream = new MemoryStream();

                    await product.ProductImage1.CopyToAsync(datastream);
                    var Img1Byts = datastream.ToArray();
                    string img1Base64String = Convert.ToBase64String(Img1Byts);
                    await _imageRepository.CreateAsync(new Images() { imagepath = img1Base64String, IsDeleted = false, productID = NewPrd.Id });
                   

                    using var datastream2 = new MemoryStream();
                    await product.ProductImage2.CopyToAsync(datastream2);
                    var Img2Byts = datastream2.ToArray();
                    string img2Base64String = Convert.ToBase64String(Img2Byts);
                    await _imageRepository.CreateAsync(new Images() { imagepath = img2Base64String, IsDeleted = false, productID = NewPrd.Id });
                    


                    using var datastream3 = new MemoryStream();
                    await product.ProductImage3.CopyToAsync(datastream3);
                    var Img3Byts = datastream3.ToArray();
                    string img3Base64String = Convert.ToBase64String(Img3Byts);
                    await _imageRepository.CreateAsync(new Images() { imagepath = img3Base64String, IsDeleted = false, productID = NewPrd.Id });
               
                    using var datastream4 = new MemoryStream();
                    await product.ProductImage4.CopyToAsync(datastream4);
                    var Img4Byts = datastream4.ToArray();
                    string img4Base64String = Convert.ToBase64String(Img4Byts);
                    await _imageRepository.CreateAsync(new Images() { imagepath = img4Base64String, IsDeleted = false, productID = NewPrd.Id });
                    await _imageRepository.SaveChangesAsync();
                }
                catch
                {
                    return new ResultView<CreateOrUpdateProductDTO> { Entity = null, IsSuccess = false, Message = "can't add image  " };
                }


                return new ResultView<CreateOrUpdateProductDTO> { Entity = product, IsSuccess = true, Message = "Created Successfully" };
            }

        }
        public async Task<ResultView<CreateOrUpdateProductDTO>> UpdateProduct(CreateOrUpdateProductDTO product)
        {
           // var oldProduct= await _productRepository.GetProductWithImagesByIDAsync(product.Id);
            
            if (product == null)
            {
                return new ResultView<CreateOrUpdateProductDTO> { Entity = null, IsSuccess = false, Message = "Not correct Product" };
            }
            
            else
            {
                var pro_update = _mapper.Map<Product>(product);

                pro_update.IsApproved = false;
                pro_update.IsDeleted = false;

                await _productRepository.UpdateAsync(pro_update);
                var res = await _productRepository.SaveChangesAsync();

                try
                {
                    var oldProduct = await _productRepository.GetProductWithImagesByIDAsync(product.Id);
                    if ((product.ProductImage1 != null && !_allowedExtensions.Contains(Path.GetExtension(product.ProductImage1.FileName).ToLower())) ||
                      (product.ProductImage1 != null && !_allowedExtensions.Contains(Path.GetExtension(product.ProductImage1.FileName).ToLower())) ||
                      (product.ProductImage1 != null && !_allowedExtensions.Contains(Path.GetExtension(product.ProductImage1.FileName).ToLower())) ||
                      (product.ProductImage1 != null && !_allowedExtensions.Contains(Path.GetExtension(product.ProductImage1.FileName).ToLower()))
                      )
                    {
                        return new ResultView<CreateOrUpdateProductDTO> { Entity = null, IsSuccess = false, Message = "The Allowed Extensions is .png and .jpg" };
                    }
                    if (product.ProductImage1 != null)
                    {
                            using var datastream = new MemoryStream();
                            await product.ProductImage1.CopyToAsync(datastream);
                            var Img1Byts = datastream.ToArray();
                            string img1Base64String = Convert.ToBase64String(Img1Byts);
                            var img1 =oldProduct.images.FirstOrDefault().imagepath = img1Base64String;
                            if (img1 != null)
                            {
                                img1 = img1Base64String;
                            }
                    }
                    if (product.ProductImage2 != null)
                    {
                            using var datastream = new MemoryStream();
                            await product.ProductImage2.CopyToAsync(datastream);
                            var Img1Byts = datastream.ToArray();
                            string img1Base64String = Convert.ToBase64String(Img1Byts);
                            var img2 = oldProduct.images.Skip(1).FirstOrDefault().imagepath = img1Base64String;
                            if (img2 != null)
                            {
                                img2 = img1Base64String;
                            }
                    }
                    if (product.ProductImage3 != null)
                    {
                            using var datastream = new MemoryStream();
                            await product.ProductImage3.CopyToAsync(datastream);
                            var Img1Byts = datastream.ToArray();
                            string img1Base64String = Convert.ToBase64String(Img1Byts);
                             var img3 = oldProduct.images.Skip(2).FirstOrDefault().imagepath = img1Base64String;
                            if (img3 != null)
                            {
                                img3 = img1Base64String;
                            }
                    }
                     if (product.ProductImage4 != null)
                    {
                            using var datastream = new MemoryStream();
                            await product.ProductImage4.CopyToAsync(datastream);
                            var Img1Byts = datastream.ToArray();
                            string img1Base64String = Convert.ToBase64String(Img1Byts);
                            var img4 = oldProduct.images.Skip(3).FirstOrDefault().imagepath;
                            if(img4 != null)
                            {
                                img4 = img1Base64String;
                            }
                            
                    }

                    
                    await _imageRepository.SaveChangesAsync();
                    return new ResultView<CreateOrUpdateProductDTO> { Entity = product, IsSuccess = true, Message = "Successfully Updated Product" };

                }
                catch (Exception ex)
                {

                    return new ResultView<CreateOrUpdateProductDTO> { Entity = null, IsSuccess = false, Message = ex.Message };
                }
            }

        }

        public async Task<ResultView<ProductDTO>> Create(ProductDTO product)
        {
            var Query = (await _productRepository.GetAllAsync()); // select * from product
            var OldProduct = Query.Where(p => p.Title == product.Title).FirstOrDefault();
            if (OldProduct != null)
            {
                return new ResultView<ProductDTO> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                var Prd = _mapper.Map<Product>(product);
                Prd.IsApproved = false;
                Prd.IsDeleted = false;
                var NewPrd = await _productRepository.CreateAsync(Prd);
                await _productRepository.SaveChangesAsync();
                var PrdDto = _mapper.Map<ProductDTO>(NewPrd);
                return new ResultView<ProductDTO> { Entity = PrdDto, IsSuccess = true, Message = "Created Successfully" };
            }

        }
              
        public async Task<ResultView<ProductDTO>> HardDelete(ProductDTO product)
        {
            try
            {
                var PRd = _mapper.Map<Product>(product);
                var Oldprd = _productRepository.DeleteAsync(PRd);
                await _productRepository.SaveChangesAsync();
                var PrdDto = _mapper.Map<ProductDTO>(Oldprd);
                return new ResultView<ProductDTO> { Entity = product, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<ProductDTO> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }
        public async Task<ResultView<ProductDTO>> SoftDelete(int productID)
        {
            try
            {
                var Oldprd = (await _productRepository.GetAllAsync()).FirstOrDefault(p => p.Id == productID);
                if(Oldprd == null)
                {
                    return new ResultView<ProductDTO> { Entity = null, IsSuccess = false, Message = "Not correct ID"};
                }
              
                Oldprd.IsDeleted = true;
                await _productRepository.SaveChangesAsync();
                var PrdDto = _mapper.Map<ProductDTO>(Oldprd);
                return new ResultView<ProductDTO> { Entity = PrdDto, IsSuccess = true, Message = "Deleted Successfully" };
            }
            catch (Exception ex)
            {
                return new ResultView<ProductDTO> { Entity = null, IsSuccess = false, Message = ex.Message };

            }
        }


        public async Task<ResultView<ProductDTO>> GetOne(int ID)
        {
            var prd = await _productRepository.GetProductWithImagesByIDAsync(ID);
            if(prd == null)
            {
                return new ResultView<ProductDTO>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = " not found"
                };
            }
            var product = _mapper.Map<ProductDTO>(prd);

            product.ImagesArr = new string[4];
           
         
           
            if (prd.images.Count()>=1)
            {
                var img1 = (prd.images.FirstOrDefault().imagepath) ?? string.Empty;
                product.ImagesArr[0] = img1;
                
                if (prd.images.Count() >= 2)
                    {
                        var img2 = prd.images.Skip(1).FirstOrDefault().imagepath ?? string.Empty;
                        product.ImagesArr[1] = img2;
                       
                    if(prd.images.Count() >= 3)
                    {
                        var img3 = prd.images.Skip(2).FirstOrDefault().imagepath ?? string.Empty;
                        product.ImagesArr[2] = img3;
                            
                        if (prd.images.Count() >= 3)
                        {
                            var img4 = prd.images.Skip(2).FirstOrDefault().imagepath ?? string.Empty;
                            product.ImagesArr[3] = img4;
                            }
                        }
                    } 
               
            }

            return new ResultView<ProductDTO>
            {
                Entity = product,
                IsSuccess = true,
                Message = "successfull"
            };
        }

        public async Task<ResultDataList<ProductDTO>> GetProductListByIdList(List<int> Ids)
        {

            var AlldAta = (await _productRepository.GetAllAsync());
            var Prds = AlldAta.Where(x => Ids.Contains(x.Id)).Select(p => new ProductDTO()
            {
                Id = p.Id,
                Title = p.Title,
                
                CategoryName = p.category.Name
            }).ToList();
            ResultDataList<ProductDTO> resultDataList = new ResultDataList<ProductDTO>();
            resultDataList.Entities = Prds;
            resultDataList.Count = AlldAta.Count();
            return resultDataList;
        }

        public async Task<ResultDataList<ProductDTO>> GeProductsByVendorID(int items, int pagenumber, string VendorOrAdminID)
        {
            var AlldAta = (await _productRepository.GetAllAsync());
            var Prds = AlldAta.Where(u => u.VendorOrAdminID == VendorOrAdminID)
                .Include(x => x.category).Include(c => c.User)
                                              .Select(p => new ProductDTO()
                                              {
                                                  Id = p.Id,
                                                  Title = p.Title,
                                                  CategoryId = p.category.Id,
                                                  IsApproved = p.IsApproved,
                                                  Description = p.Description,
                                                  VendorId = p.VendorOrAdminID,
                                                  productGender = p.productGender,
                                                  VendorName = p.User.UserName,
                                                  CategoryName = p.category.Name,
                                                  ar_Description = p.ar_Description,
                                                  ar_Title  = p.ar_Title,
                                                  Code  = p.ar_Title,
                                                  
                                                 
                                              }).Skip(items * (pagenumber - 1)).Take(items).ToList();
            ResultDataList<ProductDTO> resultDataList = new ResultDataList<ProductDTO>();
            resultDataList.Entities = Prds;
            resultDataList.Count = AlldAta.Count();
            return resultDataList;
        }

        public async Task<ResultView<ProductDTO>> acceptVendorProduct(ProductDTO product)
        {
            throw new NotImplementedException();
        }



        public async Task<ResultDataList<ProductFavoriteDTO>> ProductsInFavorite(List<string> ProductIds)
        {
            var result = new ResultDataList<ProductFavoriteDTO>();

            if (ProductIds != null && ProductIds.Count != 0)
            {
                var validProductIds = new List<int>();

                foreach (var productIdJson in ProductIds)
                {
                    // Parse each JSON array into a list of integers
                    List<int> productIdList = JsonConvert.DeserializeObject<List<int>>(productIdJson);

                    // Add all parsed integers to the validProductIds list
                    validProductIds.AddRange(productIdList);
                }

                var products = await _productRepository.GetAllProductinFavoriteAsyncBy(validProductIds);
                result.Count = products.Count;
                result.Entities = products;
            }
            else
            {
                result.Count = 0;
                result.Entities = null;
            }

            return result;
        }

        public async Task<List<ProductFavoriteDTO>> GetFilteredProducts(string searchText, List<ProductFavoriteDTO> productFavoriteDTOs)
        {
            var result = new List<ProductFavoriteDTO>();

            if (!string.IsNullOrEmpty(searchText) && productFavoriteDTOs != null)
            {
                result=  productFavoriteDTOs
                    .Where(x => x.Title.Contains(searchText.Trim()))
                    .ToList();
            }
            else
            {
                result = new List<ProductFavoriteDTO>();
            }


            return result;
        }


        public async Task<ResultDataList<GetCart>> ProductsInCart(List<string> ProductIds)
        {
            var result = new ResultDataList<GetCart>();

            if (ProductIds != null && ProductIds.Count != 0)
            {
                var validProductIds = new List<int>();

                foreach (var productIdJson in ProductIds)
                {
                    List<int> productIdList = JsonConvert.DeserializeObject<List<int>>(productIdJson);

                    validProductIds.AddRange(productIdList);
                }

                var products = await _productRepository.GetAllProductsInCartAsyncBy(validProductIds);
                result.Count = products.Count;
                result.Entities = products;
            }
            else
            {
                result.Count = 0;
                result.Entities = null;
            }

            return result;
        }
        public async Task<List<string>> GetProductsSizesBy(int ProductId, string ColorName)
        {

            if (ProductId > 0 && ColorName is not null)
           {
               var res = await _productRepository.GetAllSizesBy(ProductId, ColorName);
                return res;

           }
          throw new NotImplementedException();
        }
        public async Task<GetQuntityAndPrice> GetProductsQuantityBy(int ProductId, string ColorName,string SizeName)
        {

            if (ProductId > 0 && ColorName is not null)
            {
                var res = await _productRepository.GetQuantityBy(ProductId, ColorName, SizeName);
                return res;

            }
            return null;
        }

    }
}