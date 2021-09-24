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
    public class SkuValueRepository : ISkuValueRepository
    {
        private readonly ChamaSubscriptionDbContext context;

        public SkuValueRepository(ChamaSubscriptionDbContext context)
        {
            this.context = context;
        }

        public async Task<SkuValue> GetSkuValue(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.SkuValues.FindAsync(id);

            return await context.SkuValues
            
             .Include(s => s.OptionValue)
                 .ThenInclude(a => a.Option)
               // .Include(a => a.SkuValueName)

              .SingleOrDefaultAsync(o => o.Id == id);
        }

        public void Add(SkuValue skuValue)
        {
            context.SkuValues.Add(skuValue);

        }

        public void Remove(SkuValue skuValue)
        {
            context.Remove(skuValue);
        }
    }

}
