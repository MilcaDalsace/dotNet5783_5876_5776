using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public int ID { get; set; }
        public Status OrderStatus { get; set; }
        public List<(DateTime, string)?> DateAndStatus { get; set; }



        public override string ToString()
        {
            String toString = "Product ID: " + ID + "\nStatus: " + OrderStatus + "\nTime and status:\n";
            for (int i = 0; i < DateAndStatus.Count; i++)
            {
                toString += DateAndStatus[i] + "\n";
            }

            return toString;
        }
        //        Product ID: {ID}
        //        OrderStatus: {OrderStatus} 
        //        DateAndStatus: {DateAndStatus.}
        //";
        //    }
    }
}
