namespace ChamaSubscription.Infrastructure.DTO.Query

{
    public class ProductSkuQueryDTO
    {
        public int? ProductCategoryId { get; set; }

      // option value reminder* variable
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}