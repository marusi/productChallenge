using System.Collections.Generic;

namespace Blazor.Domain.Models
{
    public class QueryResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}