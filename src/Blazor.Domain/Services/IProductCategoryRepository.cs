
using Blazor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Domain.Services
{
   public interface IProductCategoryRepository
    {
        Task<ProductCategory> GetProductCategory(int id, bool includeRelated = true);
        void Add(ProductCategory productCategory);
        void Remove(ProductCategory productCategory);
    }
}
