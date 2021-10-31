using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Domain.Models
{
   public class CompositeProduct
    {

        public int Id { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<CombinedProduct> CombinedProducts { get; set; }

    

        public CompositeProduct()
        {
            CombinedProducts = new Collection<CombinedProduct>();
            
        }
    }
}
