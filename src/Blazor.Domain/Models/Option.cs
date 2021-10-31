using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Domain.Models
{
    [Table("Options")]
    public class Option
    {
        public int Id { get; set; }

       

        public int ProductId { get; set; }

        public string OptionName {  get; set; }
        public DateTime LastUpdate { get; set; }

        public Product Product { get; set; }
    }
}
