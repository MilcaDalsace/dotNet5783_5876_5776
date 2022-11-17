using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public float FinalPrice { get; set; }
        public override string ToString() => $@"
        FinalPrice: {FinalPrice}, 
        Id: {Id}
        ProductId:{ProductId}
        ProductName : {ProductName}
    	Price: {Price}
    	Amount : {Amount}
";
    }
}
