using Blazor.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Blazor.Domain.Services
{
    public interface IProductSkuRepository
    {
        Task<ProductSku> GetProductSku(int id, bool includeRelated = true);

        void Add(ProductSku productSku);
        void Remove(ProductSku productSku);

        Task<QueryResult<ProductSku>> GetProductSkus(ProductSkuQuery filter);
    }
}
