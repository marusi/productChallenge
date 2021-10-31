using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWithIdentity.Shared.DTO.Option
{
    public class SaveOptionDTO
    {
        public int Id { get; set; }

        public string OptionName { get; set; }

        public int ProductId { get; set; }
    }
}