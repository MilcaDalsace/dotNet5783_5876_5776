using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public Status OrderSatus { get; set; }
        public int AmountItems { get; set; }
        public float FinalPrice { get; set; }

        public override string ToString() => $@"
        Product ID: {ID}
        Name: {CustomerName} 
        OrderSatus: {OrderSatus}
    	AmountItems: {AmountItems}
        FinalPrice: {FinalPrice}
";
    }
}
