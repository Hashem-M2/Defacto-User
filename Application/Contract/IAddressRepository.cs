using Application.Contracts;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contract
{
    public interface IAddressRepository : IRepository<Address ,int>
    {
        Task<Images> GetfirstImageForProduct(int proID);
    }
}
