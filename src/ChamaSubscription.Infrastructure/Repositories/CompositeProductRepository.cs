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
   public class CompositeProductRepository : ICompositeProductRepository
    {
        private readonly ChamaSubscriptionDbContext context;

        public CompositeProductRepository(ChamaSubscriptionDbContext context)
        {
            this.context = context;
        }

        public async Task<CompositeProduct> GetCompositeProduct(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.CompositeProducts.FindAsync(id);

            return await context.CompositeProducts
                .Include(p => p.CombinedProducts)
                 .ThenInclude(ps => ps.ProductSku)

              .SingleOrDefaultAsync(p => p.Id == id);
        }

        public void Add(CompositeProduct compositeProduct)
        {
            context.CompositeProducts.Add(compositeProduct);

        }

        public void Remove(CompositeProduct compositeProduct)
        {
            context.Remove(compositeProduct);
        }
    }
}
