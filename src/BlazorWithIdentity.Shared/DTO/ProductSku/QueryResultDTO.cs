using System.Collections.Generic;

namespace BlazorWithIdentity.Shared.DTO.ProductSku
{
    public class QueryResultDTO<T>
    {

        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }

        
    }
}