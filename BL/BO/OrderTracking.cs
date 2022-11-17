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
        public List<(DateTime,Status)> DateAndStatus { get; set; }

        public override string ToString() => $@"
        Product ID: {ID}
        OrderStatus: {OrderStatus} 
        DateAndStatus: {DateAndStatus}
";
    }
}
