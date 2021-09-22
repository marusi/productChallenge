﻿using ChamaSubscription.Domain.Models;
using ChamaSubscription.Domain.Services;
using ChamaSubscription.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OptionValueRepository : IOptionValueRepository
{
    private readonly ChamaSubscriptionDbContext context;

    public OptionValueRepository(ChamaSubscriptionDbContext context)
    {
        this.context = context;
    }

    public async Task<OptionValue> GetOptionValue(int id, bool includeRelated = true)
    {
        if (!includeRelated)
            return await context.OptionValues.FindAsync(id);

        return await context.OptionValues
      //   .Include(o => o.Product)
         .Include(o => o.Option.Product)

          .SingleOrDefaultAsync(o => o.Id == id);
    }

    public void Add(OptionValue optionValue)
    {
        context.OptionValues.Add(optionValue);

    }

    public void Remove(OptionValue optionValue)
    {
        context.Remove(optionValue);
    }
}
