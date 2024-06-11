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
    public interface ICategoryService
    {
        Task<ResultView<CategoryDTO>> CreateCategory(CategoryDTO CategoryDTO);
        Task<ResultView<CategoryDTO>> UpdateCategory(CategoryDTO CategoryDTO);
        Task<ResultDataList<CategoryDTO>> GetCategoriesByGender(SubCategory gender);
        Task<ResultView<CategoryDTO>> GetCategoryByID(int Id);
        Task<ResultDataList<ListOfCategoryDTO>> GetAllPagination(int items, int pagenumber);
        Task<ICollection<CategoryDTO>> GetAllCategories();
        Task<ResultView<CategoryDTO>> HardDelete(int catID);
      
        Task<ResultView<CategoryDTO>> SoftDelete(int catID);
    }
}
