using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // 1 to 1 with user 
    public class Address :BaseEntity
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string Country { get; set; }
      /*  [ForeignKey("User")]
        public string UserID { get; set; }
        public AppUser User { get; set; }*/
    }
}
