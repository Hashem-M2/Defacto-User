using Application.Contract;
using Context;
using Model;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ColorRepository :Repository<Color,int>,IColorRepository
    {
        public ColorRepository(ApplicationDbContext context) : base(context)
        {

        }
    }

}
