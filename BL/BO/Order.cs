using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO
{
    public class Order
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAdress { get; set; }
        public Status OrderStatus { get; set; }
        public List<OrderItem> OrderItemList { get; set; }
        public float FinalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        //        public override string ToString() => $@"
        //        order ID: {ID}, 
        //        customerName: {CustomerName}
        //    	customerEmail: {CustomerEmail}
        //    	custmerAdress: {CustomerAdress}
        //        orderStatus: {OrderStatus}
        //    	orderItemList: {OrderItemList}
        //    	finalPrice: {FinalPrice}
        //        orderDate: {OrderDate}
        //        shipDate: {ShipDate}
        //        deliveryDate: {DeliveryDate}
        //";
        //    }

        public override string ToString()
        {
            string tostring = "order ID: " + ID + "\n customerName: " + CustomerName + "\n customerEmail: " + CustomerEmail + "\n custmerAdress: " + CustomerAdress
                    + "\n orderStatus: " + OrderStatus + "\n finalPrice: " + FinalPrice
                    + "\n orderDate: " + OrderDate
                    + "\n shipDate: " + ShipDate + "\n deliveryDate: " + DeliveryDate
                    ;
            for (int i = 0; i < OrderItemList.Count; i++)
                tostring += "\n item" + (i + 1) + ": " + OrderItemList[i] + "\n";
            return tostring;
        }
    }
}
