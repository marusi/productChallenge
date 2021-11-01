using Blazor.Domain.Extensions;

namespace Blazor.Domain.Models
{
    public class ProductSkuQuery : IQueryObject
    {
        public int? CategoryId { get; set; }
        public int? SkuValueId { get; set; }
        public int? OptionValueId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}