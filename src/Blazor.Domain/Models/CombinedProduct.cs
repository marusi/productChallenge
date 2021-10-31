using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Domain.Models
{
    [Table("CombinedProducts")]
    public class CombinedProduct
    {
        public int ProductSkuId { get; set; }

        public ProductSku ProductSku { get; set; }

        public int CompositeProductId { get; set; }

        public CompositeProduct CompositeProduct { get; set; }
    }
}
