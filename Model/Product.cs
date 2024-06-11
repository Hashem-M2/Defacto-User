using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

  
    public class Product :BaseEntity
    {
        public string Title { get; set; }
        public string? ar_Title { get; set; }
        public string? Code{ get; set; }
        public bool IsApproved { get; set; }
        public Gender productGender { get; set; } = 0;
        public string? Description{ get; set; }
        public string? ar_Description{ get; set; }
        [ForeignKey("User")]
        public string VendorOrAdminID { get; set; }
        public AppUser User { get; set; }
        
        [ForeignKey("category")]
        public int categoryID { get; set; }
        public Category? category { get; set; }

        public ICollection<Item>? items { get; set; }    
        public ICollection<Images>? images { get; set; }    

        public Product()
        {
            items = new List<Item>();   
            images = new List<Images>();
        }
    }
}
