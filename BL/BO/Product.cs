﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Product
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public float Price { get; set; }
        public BO.Categories Category { get; set; }
        public int InStock { get; set; }

        public override string ToString() => $@"
        Product ID: {ID}
        Name: {Name} 
        category: {Category}
    	Price: {Price}
    	Amount in stock: {InStock}
";
    }
}
