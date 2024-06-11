using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Item : BaseEntity
    {

        public bool IsForDefacto { get; set; } = false; 
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("size")]
        public int? SizeID { get; set; }
        public Size? size{ get; set; }   
        [ForeignKey("color")]
        public int ColorID { get; set; }
        public Color? color{ get; set; }  
        
        [ForeignKey("product")]
        public int productID { get; set; }
        public Product product{ get; set; }

        public string descraption { get; set; }
    }
}
