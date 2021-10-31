using Blazor.Domain.Models;
using Blazor.Domain.Services;
using Blazor.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class OptionRepository : IOptionRepository
{
    private readonly ApplicationDbContext context;

    public OptionRepository(ApplicationDbContext context)
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
