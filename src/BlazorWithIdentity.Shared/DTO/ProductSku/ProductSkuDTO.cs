
using BlazorWithIdentity.Shared.DTO.SkuValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Shared.DTO.ProductSku
{
   public class ProductSkuDTO
    {

        public int Id { get; set; }

        public decimal Price { get; set; }
        public SkuValueDTO SkuValue { get; set; }

        public DateTime LastUpdate { get; set; }



     
    }
}
