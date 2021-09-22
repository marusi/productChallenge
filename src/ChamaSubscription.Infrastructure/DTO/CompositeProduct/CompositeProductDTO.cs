using ChamaSubscription.Infrastructure.DTO.ProductSku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaSubscription.Infrastructure.DTO.CompositeProduct
{
   public class CompositeProductDTO
    {

        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<ProductSkuDTO> CombinedProducts { get; set; }

        public CompositeProductDTO()
        {
            CombinedProducts = new List<ProductSkuDTO>();
        }

    }
}
