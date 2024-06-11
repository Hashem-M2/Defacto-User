using Microsoft.AspNetCore.Http;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Product
{
    public class CreateOrUpdateProductDTO
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsApproved { get; set; }
        public Gender? productGender { get; set; }
        public int categoryID { get; set; }
        public string? Code { get; set; }
        public string Description { get; set; }
        public IFormFile? ProductImage1 { get; set; }
        public IFormFile? ProductImage2 { get; set; }
        public IFormFile? ProductImage3 { get; set; }
        public IFormFile? ProductImage4 { get; set; }
        public string VendorId { get; set; }
        public string? ar_Description { get; set; }
        public string? ar_Title { get; set; }
    }
}
