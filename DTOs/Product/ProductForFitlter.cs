using DTOs.Item;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Product
{
    public class ProductForFitlter
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsApproved { get; set; }
        public Gender? productGender { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public string? CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string? VendorName { get; set; }
        public string VendorId { get; set; }

        public List<ItemDto>? items { get; set; }

        public string? ar_Title { get; set; }
        public string? ar_Description { get; set; }
    }
}
