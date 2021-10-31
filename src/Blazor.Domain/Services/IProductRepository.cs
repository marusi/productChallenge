using Blazor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Domain.Services
{
   public interface IProductRepository
    {
        Task<Product> GetProduct(int id, bool includeRelated = true);

        void Add(Product product);
        void Remove(Product product);
    }
}
