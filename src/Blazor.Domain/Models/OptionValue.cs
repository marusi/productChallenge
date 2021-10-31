using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Domain.Models
{
    [Table("OptionValues")]
    public class OptionValue
    {
        public int Id { get; set; }



      //  public int? ProductId { get; set; }
        public int OptionId { get; set; }

        public string OptionValueName { get; set; }
      //  public DateTime LastUpdate { get; set; }

      //  public Product Product { get; set; } 
        public Option Option { get; set; } 
    }
}
