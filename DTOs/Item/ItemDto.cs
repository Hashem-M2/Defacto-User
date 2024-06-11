using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Item
{
    public class ItemDto
    {
        [Required]
        public int Id { get; set; }
        public bool IsForDefacto { get; set; } = false;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int? SizeID { get; set; }
        public string SizeName { get; set; }
        public int ColorID { get; set; }
        public string ColorName { get; set; }
        public string? ColorHEX { get; set; }
        public int productID { get; set; }

    
    }
}
