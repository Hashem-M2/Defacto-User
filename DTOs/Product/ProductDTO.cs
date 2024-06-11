using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_s.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsApproved { get; set; }
        public Gender? productGender { get; set; }
        public string? Code { get; set; }
        public string Description { get; set; }
        public string[] ImagesArr { get; set; } = new string[4];
     
        public string? CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string? VendorName { get; set; }
        public string VendorId { get; set; }
        /*  public List<Item>? items { get; set; }*/
        public string? ar_Title { get; set; }
        public string? ar_Description { get; set; }
    }
}
