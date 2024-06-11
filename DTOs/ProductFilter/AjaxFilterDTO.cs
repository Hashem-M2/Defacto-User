using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ProductFilter
{
    public class AjaxFilterDTO
    {
        public SubCategory subCategory { get; set; }
        public SubCategory[]? subCategoryArr { get; set; }
        public Gender[]? genderArr { get; set; }
        public string[]? sizeArr { get; set; }
        public string[]? colotArr { get; set; }
        public int? maxPrice { get; set; }
        public int? mintPrice { get; set; }
        public string? searchTxt { get; set; }

    }  
}
