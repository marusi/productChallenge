using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Domain.Models
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }

        public int ProductCategoryId { get; set; }

        public string ProductName {  get; set; }

      //  public DateTime LastUpdate { get; set; }

        public ProductCategory ProductCategory { get; set; }
    }
}
