using Blazor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Domain.Services
{
    public interface IOptionRepository
    {
        Task<Option> GetOption(int id, bool includeRelated = true);

        void Add(Option option);
        void Remove(Option option);
    }
}
