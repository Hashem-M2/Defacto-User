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
    public class ImageRepository : Repository<Images, int>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext context) : base(context)
        {

        }

  
    }
    
}
