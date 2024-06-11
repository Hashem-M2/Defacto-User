using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class itemReview : BaseEntity
    {
        [ForeignKey("User")]
        public string CustomerID { get; set; }
        public AppUser User { get; set; }
        [Required]
        public string ReviewMessage { get; set; }
        [Range(1, 5, ErrorMessage = "Review Rate Must be between 1 to 5")]
        public int? ReviewRate { get; set; }
        [ForeignKey("item")]
        public int itemID { get; set; }
        public Item item { get; set; }
    }
}
