using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ColorAndSize
{
    public class ColorDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string HEX { get; set; }
        public string RGB { get; set; }
        public bool? NotActive { get; set; }
    }
}
