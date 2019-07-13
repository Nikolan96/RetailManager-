﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManagerCore.Library.Models
{
    public class ProductModel
    {

        public string Id { get; set; }

        public string ProductName { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal Tax { get; set; }

        public decimal RetailPrice { get; set; }

        public int QuantityInStock { get; set; }

        public decimal Margin { get; set; }

    }
}
