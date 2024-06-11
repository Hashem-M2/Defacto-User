using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_s.Product
{
    public class GetAllProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string CategoryName { get; set; }
        public string? ar_Title { get; set; }
        public string? ar_Description { get; set; }
    }
}
