using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Domain.Models
{
    [Table("SkuValues")]
    public class SkuValue
    {
        public int Id { get; set; }

        public int OptionValueId { get; set; }

        public string SkuValueName { get; set; }

       // public DateTime LastUpdate { get; set; }

        public OptionValue OptionValue { get; set; }
    }
}
