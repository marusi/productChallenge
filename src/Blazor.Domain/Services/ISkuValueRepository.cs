using Blazor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Domain.Services
{
    public interface ISkuValueRepository
    {
        Task<SkuValue> GetSkuValue(int id, bool includeRelated = true);

        void Add(SkuValue skuValue);
        void Remove(SkuValue skuValue);
    }
}
