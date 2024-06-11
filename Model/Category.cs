using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Category :BaseEntity
    {
        public string Name { get; set; }
        public string? ar_Name { get; set; }
        public SubCategory subCategory { get; set; } = 0;
        public string? Description { get; set; }
        public string? ar_Description { get; set; }
        public string? Image { get; set; }

        public ICollection<Product> Products { get; set; }


        public Category()
        {
            Products = new List<Product>();
        }
    }
}
