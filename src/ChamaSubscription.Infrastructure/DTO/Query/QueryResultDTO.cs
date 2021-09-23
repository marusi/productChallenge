using System.Collections.Generic;

namespace ChamaSubscription.Infrastructure.DTO.Query
{
    public class QueryResultDTO<T>

    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}