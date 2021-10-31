using Blazor.Domain.Models;
using Blazor.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Infrastructure.Repositories
{
   public class CompositeProductRepository : ICompositeProductRepository
    {
        private readonly ApplicationDbContext context;

        public CompositeProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<CompositeProduct> GetCompositeProduct(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.CompositeProducts.FindAsync(id);

            return await context.CompositeProducts
                .Include(p => p.CombinedProducts)
                 .ThenInclude(ps => ps.CompositeProduct)
                 .Include(ps => ps.CombinedProducts)
                 .ThenInclude(ps => ps.ProductSku)
                 .ThenInclude(ps => ps.SkuValue)
                 .ThenInclude(ps => ps.OptionValue)
                 .ThenInclude(ps => ps.Option)
                 .ThenInclude(ps => ps.Product)
                 .ThenInclude(ps => ps.ProductCategory)

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
