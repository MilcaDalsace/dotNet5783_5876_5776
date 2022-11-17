using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    internal class ProductItem
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public DO.Categories Category { get; set; }
        public bool Available { get; set; }
        public int AmountInCart { get; set; }

        public override string ToString() => $@"
        Product ID: {ID}
        Name: {ProductName} 
        category: {Category}
    	Price: {Price}
        Available: {Available}
    	Amount in cart: {AmountInCart}
";
    }
}
