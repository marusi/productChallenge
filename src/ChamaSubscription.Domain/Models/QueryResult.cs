﻿using System;
using System.Collections.Generic;


namespace ChamaSubscription.Domain.Models
{
   public class QueryResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }

    }
}
