using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_s.Category
{
    public class ListOfCategoryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? ar_Name { get; set; }
        public string? ar_Description { get; set; }
        public string? Description { get; set; }
        public SubCategory subCategory { get; set; }
        public string? Image { get; set; }


    }
}
