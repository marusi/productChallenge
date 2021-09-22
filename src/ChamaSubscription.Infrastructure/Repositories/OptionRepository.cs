using ChamaSubscription.Domain.Models;
using ChamaSubscription.Domain.Services;
using ChamaSubscription.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OptionRepository : IOptionRepository
{
    private readonly ChamaSubscriptionDbContext context;

    public OptionRepository(ChamaSubscriptionDbContext context)
    {
        this.context = context;
    }

    public async Task<Option> GetOption(int id, bool includeRelated = true)
    {
        if (!includeRelated)
            return await context.Options.FindAsync(id);

        return await context.Options
         .Include(p => p.Product)

          .SingleOrDefaultAsync(o => o.Id == id);
    }

    public void Add(Option option)
    {
        context.Options.Add(option);

    }

    public void Remove(Option option)
    {
        context.Remove(option);
    }
}
