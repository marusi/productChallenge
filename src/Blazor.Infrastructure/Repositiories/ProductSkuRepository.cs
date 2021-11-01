using Blazor.Domain.Models;
using Blazor.Domain.Services;
using Blazor.Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;


namespace Blazor.Infrastructure.Repositories
{
   public class ProductSkuRepository : IProductSkuRepository
    {
        private readonly ApplicationDbContext context;

        public ProductSkuRepository(ApplicationDbContext context)
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

        public async Task<QueryResult<ProductSku>> GetProductSkus(ProductSkuQuery queryObj)
        {
            var result = new QueryResult<ProductSku>();

            var query = context.ProductSkus.
               Include(p => p.SkuValue)
                 .ThenInclude(a => a.OptionValue)
                 .ThenInclude(b => b.Option)
                 .ThenInclude(c => c.Product).ThenInclude(d => d.ProductCategory)
                   .AsQueryable();

            if (queryObj.CategoryId.HasValue)
                query = query.Where(v => v.SkuValue.OptionValue.Option.Product.ProductCategoryId == queryObj.CategoryId.Value);

            if (queryObj.OptionValueId.HasValue)
                query = query.Where(v => v.SkuValueId == queryObj.SkuValueId.Value);

            var columnsMap = new Dictionary<string, Expression<Func<ProductSku, object>>>()
            {
                ["category"] = v => v.SkuValue.OptionValue.Option.Product.ProductCategory.Name,
                ["skuValue"] = v => v.SkuValueId
            };

            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItems = await query.CountAsync();

            query = query.ApplyPaging(queryObj);

            result.Items = await query.ToListAsync();

            return result;
        }

    }

}