using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Shared.DTO.OptionValue
{
    public class SaveOptionValueDTO
    {
        public int Id { get; set; }

        public string OptionValueName { get; set; }

      //  public int ProductId { get; set; }
        public int OptionId { get; set; }
    }
}