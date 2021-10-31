using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Shared.DTO.Product
{
  public  class SaveProductDTO
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int ProductCategoryId { get; set; }
    }
}
