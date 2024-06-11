using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Item
{
    public class ItemListDTO
    {

        public int Id { get; set; }
        public bool IsForDefacto { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string SizeName { get; set; }
        public string ColorName { get; set; }
        public int productID { get; set; }
    }
}
