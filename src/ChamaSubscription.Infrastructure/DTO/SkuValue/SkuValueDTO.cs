using ChamaSubscription.Infrastructure.DTO.Option;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaSubscription.Infrastructure.DTO.SkuValue
{
  public  class SkuValueDTO
    {
        public int Id { get; set; }

        public string SkuValueName { get; set; }
        // public ProductDTO Product { get; set; }
        public OptionValueDTO OptionValue { get; set; }
        public int OptionValueId { get; set; }
    }
}

