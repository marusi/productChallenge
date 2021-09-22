using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaSubscription.Infrastructure.DTO.SkuValue
{
   public class SaveSkuValueDTO
    {
        public int Id { get; set; }

        public string SkuValueName { get; set; }

        //  public int ProductId { get; set; }
        public int OptionValueId { get; set; }
    }
}
