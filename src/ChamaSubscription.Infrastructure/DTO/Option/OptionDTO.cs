﻿using ChamaSubscription.Infrastructure.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChamaSubscription.Infrastructure.DTO.Option
{
    public class OptionDTO
     {

    public int Id { get; set; }

    public string OptionName { get; set; }
    public ProductDTO Product { get; set; }

    public DateTime LastUpdate { get; set; }
}
}