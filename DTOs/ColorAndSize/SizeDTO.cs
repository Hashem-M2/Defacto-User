using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ColorAndSize
{
    public class SizeDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Code { get; set; }
        public bool? NotActive { get; set; }
    }
}
