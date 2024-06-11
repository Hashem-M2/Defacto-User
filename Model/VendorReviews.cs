using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class VendorReviews : BaseEntity
    {
        [ForeignKey("User")]
        public string CustomerID { get; set; }
        public AppUser User { get; set; }
        [Required]
        public string ReviewMessage {  get; set; }

        [MaxLength(5), MinLength(1)]
        public int? ReviewRate {  get; set; }
        [ForeignKey("Vendor")]
        public string VendorID { get; set; }
        public AppUser Vendor { get; set; }
    }
}
