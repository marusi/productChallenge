﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaSubscription.Infrastructure.DTO.Product
{
  public  class SaveProductSkuDTO
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public int SkuValueId { get; set; }
    }
}
