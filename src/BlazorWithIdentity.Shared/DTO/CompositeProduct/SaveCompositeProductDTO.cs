using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Shared.DTO.CompositeProduct
{
  public  class SaveCompositeProductDTO
    {
        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<int> CombinedProducts { get; set; }

        public SaveCompositeProductDTO()
        {
            CombinedProducts = new List<int>();
        }


    }
}
