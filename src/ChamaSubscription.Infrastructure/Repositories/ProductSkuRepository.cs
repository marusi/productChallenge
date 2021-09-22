using ChamaSubscription.Domain.Models;
using ChamaSubscription.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaSubscription.Infrastructure.Repositories
{
   public class ProductSkuRepository : IProductSkuRepository
    {
        private readonly ChamaSubscriptionDbContext context;

        public ProductSkuRepository(ChamaSubscriptionDbContext context)
        {
            this.context = context;
        }
        public async Task<ProductSku> GetProductSku(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.ProductSkus.FindAsync(id);

            return await context.ProductSkus

               .Include(s => s.SkuValue).ThenInclude(ps => ps.OptionValue).ThenInclude(ps => ps.Option)

              .SingleOrDefaultAsync(o => o.Id == id);
        }

        public void Add(ProductSku productSku)
        {
            context.ProductSkus.Add(productSku);

        }

        public void Remove(ProductSku productSku)
        {
            context.Remove(productSku);
        }
    }

}