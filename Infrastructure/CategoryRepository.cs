using Application.Contract;
using Application.Contracts;
using Context;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CategoryRepository : Repository<Category, int>,ICategoryRepository
    {

        public CategoryRepository(ApplicationDbContext context) :base(context) 
        {
            
        }
    }
}