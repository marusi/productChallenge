using Blazor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Domain.Services
{
    public interface ICompositeProductRepository
    {
        Task<CompositeProduct> GetCompositeProduct(int id, bool includeRelated = true);

        void Add(CompositeProduct compositeProduct);
        void Remove(CompositeProduct compositeProduct);
    }
}