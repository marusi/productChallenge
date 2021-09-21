using ChamaSubscription.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaSubscription.Infrastructure.Repositories
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly ChamaSubscriptionDbContext context;

        public UnitOfWork(ChamaSubscriptionDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
