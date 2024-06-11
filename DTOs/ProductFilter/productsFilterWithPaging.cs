using DTO_s.ViewResult;
using DTOs.Product;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ProductFilter
{
    public class productsFilterWithPaging
    {
        
       public  ResultDataList<ProductForFitlter> resultDataList {  get; set; }  
        public int CurrentPage { get; set; }
        public int itemsForPage = 12;
        public int NoOfPage()
        {
            return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(resultDataList?.Count) / Convert.ToDouble(itemsForPage)));
        }
        public SubCategory subCategory { get; set; }
        public string searchTxt {  get; set; }
    }
}
