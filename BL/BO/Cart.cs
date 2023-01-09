using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cart
    {
        public string? Name { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAdress { get; set; }
        public List<OrderItem>? ItemOrderList { get; set; }
        public float FinalPrice { get; set; }
          
            public override string ToString() 
        {
            string tostring = "name: " + Name + "\n mail: " + CustomerEmail + "\n adress: " + CustomerAdress;
            for (int i = 0; i < (ItemOrderList==null? 0:ItemOrderList .Count ); i++)
                tostring += "\n item" + (i + 1) + ":" + (ItemOrderList == null ? 0 :ItemOrderList[i])+"\n";
            return tostring;    
            }
    }
} 
