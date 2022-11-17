using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cart
    {
        public string Name { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAdress { get; set; }
        public List<OrderItem> ItemOrderList { get; set; }
        public float FinalPrice { get; set; }
        public override string ToString() => $@"
        name: {Name} 
        customerEmail: {CustomerEmail}
    	customerAdress: {CustomerAdress}
    	itemOrderList: {ItemOrderList}
        finalPrice: {FinalPrice}
";
    }
}
