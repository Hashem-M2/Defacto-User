using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Model.Color;
using Size = Model.Size;

namespace DTOs.Orders
{
    public class GetCart
    {
        public int Id { get; init; }
        public string Image { get; init; }
        public string Title { get; init; }
        public decimal price { get; init; }
        public ICollection<string> ColorsName { get; init; }
    }
}
