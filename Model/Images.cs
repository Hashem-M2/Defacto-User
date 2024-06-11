using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Images :BaseEntity
    {
       public string imagepath {  get; set; }
        [ForeignKey("product")]
        public int productID { get; set; }
        public Product product { get; set; }
    }
}
