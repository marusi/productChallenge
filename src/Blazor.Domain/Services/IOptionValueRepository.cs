using Blazor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Domain.Services
{
    public interface IOptionValueRepository
    {
        Task<OptionValue> GetOptionValue(int id, bool includeRelated = true);

        void Add(OptionValue optionValue);
        void Remove(OptionValue optionValue);
    }
}
