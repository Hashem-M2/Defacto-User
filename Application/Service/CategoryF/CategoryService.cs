using Application.Contract;
using Application.Contracts;
using AutoMapper;
using DTO_s.Category;
using DTO_s.Product;
using DTO_s.ViewResult;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _CategoryRepository;
        private readonly IMapper _mapper;
        private new List<string> _allowedExtensions = new List<string> { ".png", ".jpg", ".jpeg" };
        private long _maxAllowedPosterSize = 1048576;

        public CategoryService(ICategoryRepository CategoryRepository, IMapper mapper)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = mapper;
        }
        public async Task<ResultView<CategoryDTO>> CreateCategory(CategoryDTO CategoryDTO)
        {

            var Query = (await _CategoryRepository.GetAllAsync()); 
            var OldCategory =  Query.Where(c => c.Name == CategoryDTO.Name && c.subCategory == CategoryDTO.subCategory).FirstOrDefault();
            if (OldCategory != null)
            {
                return new ResultView<CategoryDTO> { Entity = null, IsSuccess = false, Message = "Already Exist" };
            }
            else
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(CategoryDTO.categoryImage.FileName).ToLower()) && CategoryDTO.categoryImage != null)
                {
                    return new ResultView<CategoryDTO> { Entity = null, IsSuccess = false, Message = "The Allowed Extensions is .png and .jpg" };

                }
                using var datastream = new MemoryStream();

                await CategoryDTO.categoryImage.CopyToAsync(datastream);
                var CategoryByts = datastream.ToArray();
                string Category64String = Convert.ToBase64String(CategoryByts);

                var cat = _mapper.Map<Category>(CategoryDTO);
                cat.Image = Category64String;
                var NewPrd = await _CategoryRepository.CreateAsync(cat);
                NewPrd.IsDeleted = false;
                await _CategoryRepository.SaveChangesAsync();
         
                return new ResultView<CategoryDTO> { Entity = CategoryDTO, IsSuccess = true, Message = "Created Successfully" };
            }
        }
        public async Task<ResultView<CategoryDTO>> UpdateCategory(CategoryDTO CategoryDTO )
        {

            try
            {
                if (CategoryDTO.categoryImage != null && !_allowedExtensions.Contains(Path.GetExtension(CategoryDTO.categoryImage.FileName).ToLower()) )
                {
                    return new ResultView<CategoryDTO> { Entity = null, IsSuccess = false, Message = "The Allowed Extensions is .png and .jpg" };

                }

              

                Category cat = _mapper.Map<Category>(CategoryDTO);
                cat.IsDeleted = false;
                if (CategoryDTO.categoryImage != null)
                {
                    using var datastream = new MemoryStream();

                    await CategoryDTO.categoryImage.CopyToAsync(datastream);
                    var CategoryByts = datastream.ToArray();
                    string Category64String = Convert.ToBase64String(CategoryByts);
                    cat.Image = Category64String;
                }
                var NewPrd = await _CategoryRepository.UpdateAsync(cat);
                await _CategoryRepository.SaveChangesAsync();

                return new ResultView<CategoryDTO> { Entity = CategoryDTO, IsSuccess = true, Message = "Updated Successfully" };
            }
            catch (Exception)
            {
                return new ResultView<CategoryDTO> { Entity = null, IsSuccess = false, Message = "not Found " };
            }
               
          
          
        }
        public async Task<ICollection<CategoryDTO>> GetAllCategories()
        {
          var ListofProducts =   await _CategoryRepository.GetAllAsync();
            
            return _mapper.Map<List<CategoryDTO>>(ListofProducts.Where(c=>(c.IsDeleted == false || c.IsDeleted == null)).ToList());
        }
        public async Task<ResultDataList<ListOfCategoryDTO>> GetAllPagination(int items, int pagenumber)
        {
            var AlldAta = (await _CategoryRepository.GetAllAsync());
            var categories = AlldAta.Where(x => x.IsDeleted == false || x.IsDeleted == null);
                
                
                
              var pagingModel = categories.Skip(items * (pagenumber - 1)).Take(items).ToList();

            
            var model = _mapper.Map<List<ListOfCategoryDTO>>(pagingModel);
            ResultDataList<ListOfCategoryDTO> resultDataList = new ResultDataList<ListOfCategoryDTO>();
            resultDataList.Entities = model;
            resultDataList.Count = categories.Count();
            
            return resultDataList;
        }
        public async Task<ResultDataList<CategoryDTO>> GetCategoriesByGender(SubCategory gender)
        {
            var categories = await _CategoryRepository.GetAllAsync();
            var filteredCategories = categories
                .Where(c => c.subCategory==gender && (c.IsDeleted==false||c.IsDeleted==null))
                //.Select(c => _mapper.Map<CategoryDTO>(c))
                .ToList();
            var list = _mapper.Map<List<CategoryDTO>>(filteredCategories);
            return new ResultDataList<CategoryDTO>
            {
                Entities = list,
                Count = list.Count
            };
        }
        public async Task <ResultView<CategoryDTO>> GetCategoryByID(int  Id)
        {
            
                Category cat =  await _CategoryRepository.GetByIdAsync(Id);

            if(cat == null)
            {
                return new ResultView<CategoryDTO>()
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "not found"
                };
            }
                var Catdto = _mapper.Map <CategoryDTO>(cat);

            
            ResultView<CategoryDTO> ResultView = new ResultView<CategoryDTO>()
            {
                Entity = Catdto,
                IsSuccess = true,
                Message = "Get Category Successfully " 
            };
                
                return ResultView;
        }

        public async Task<ResultView<CategoryDTO>> HardDelete(int catID)
        {
            try
            {
              var cat =   await _CategoryRepository.GetByIdAsync(catID);  
                await _CategoryRepository.DeleteAsync(cat);
                await _CategoryRepository.SaveChangesAsync();
                var catDto = _mapper.Map<CategoryDTO>(cat);
                ResultView<CategoryDTO> resultView = new ResultView<CategoryDTO>()
                {
                    Entity = catDto,
                    IsSuccess = true,
                    Message = "Deleted Successfully"
                };
                return resultView;
            }
            catch (Exception ex)
            {

                return new ResultView<CategoryDTO> { Entity = null, IsSuccess = false, Message = ex.Message };
            }
        }
  
        public async Task<ResultView<CategoryDTO>> SoftDelete(int catID)
        {
            try
            {

                var catObj=   await _CategoryRepository.GetByIdAsync(catID);

                if (catObj == null)
                {
                   return new ResultView<CategoryDTO>()
                    {
                        Entity = null,
                        IsSuccess = false,
                        Message = "Not found"
                    };
                }
                catObj.IsDeleted = true;
                await _CategoryRepository.SaveChangesAsync();
                var cat = _mapper.Map<CategoryDTO>(catObj);
                ResultView<CategoryDTO> resultView = new ResultView<CategoryDTO>()
                {
                    Entity = cat,
                    IsSuccess = true,
                    Message = "Deleted Successfully"
                };
                return resultView;
            }
            catch (Exception ex)
            {

                return new ResultView<CategoryDTO> { Entity = null, IsSuccess = false, Message = ex.Message };
            }
        }

       
    }
}
