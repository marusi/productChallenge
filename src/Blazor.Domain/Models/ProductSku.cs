using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Domain.Models
{
    [Table("ProductSkus")]
    public class ProductSku
    {
        public int Id { get; set; }

        public int SkuValueId { get; set; }

       
        public decimal Price { get; set; }
        public DateTime LastUpdate { get; set; }

        public SkuValue SkuValue { get; set; }
    }
}
