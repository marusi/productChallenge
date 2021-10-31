using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Shared.DTO.Product
{
   public class ProductDTO
    {

        public int Id { get; set; }

        public string ProductName { get; set; }
        public KeyValuePairDTO ProductCategory { get; set; }

       // public DateTime LastUpdate { get; set; }
    }
}
