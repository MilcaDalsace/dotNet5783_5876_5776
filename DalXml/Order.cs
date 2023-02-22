using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;
using static System.Reflection.Metadata.BlobBuilder;

namespace Dal
{
    public class Order:IOrder
    {
        //static int id = 2;
        public int Create(DO.Order item)
        {
            XDocument doc = XDocument.Load(@"..\xml\Order.xml");
            XElement root = new XElement("Order");
            if (item.ID != 0)
                root.Add(new XElement("ID", item.ID));
            else
            {
                //XDocument doc = XDocument.Load(@"..\xml\Order.xml");
                //var xmlOrders = doc.Descendants("Order");
                //xmlOrders.ToList().Find(item => Convert.ToInt32(item.Element("ID")?.Value) == id)?.Remove();
                //doc.Element("Orders")?.ReplaceAll(xmlOrders);
                //doc.Save(@"..\xml\Order.xml");


                XDocument docConfig = XDocument.Load(@"..\xml\Config.xml");

               // XDocument docConfig = XDocument.Load(@"..\xml\Config.xml");
               // var xmlOrders = docConfig.Descendants("oid");
                int id = Convert.ToInt32(docConfig.Element("oid")?.Value);
                docConfig.Element("oid")?.Remove();
                item.ID = id++;
                docConfig.Element("id")?.Add(id);
                docConfig.Save(@"..\xml\Config.xml");
                root.Add(new XElement("ID", item.ID));
            }  
            root.Add(new XElement("CustomerName", item.CustomerName));
            root.Add(new XElement("CustomerEmail", item.CustomerEmail));
            root.Add(new XElement("CustomerAdress", item.CustomerAdress));
            root.Add(new XElement("OrderDate", item.OrderDate));
            root.Add(new XElement("ShipDate", item.ShipDate));
            root.Add(new XElement("DeliveryDate", item.DeliveryDate));
            doc.Element("Orders")?.Add(root);
            doc.Save(@"..\xml\Order.xml");
            return item.ID;
        }
        //public  GetAll();
        public IEnumerable<DO.Order> GetAll(Func<DO.Order, bool>? func = null)
        {
            XDocument doc = XDocument.Load(@"..\xml\Order.xml");
            var xmlOrder = doc.Descendants("Order");
            List<DO.Order> someOrder = new List<DO.Order>();
            xmlOrder.ToList().ForEach(order =>
            {
                DO.Order myOrder = new DO.Order();
                int.TryParse(order.Element("ID")?.Value, out int Id);
                myOrder.ID = Id;
                myOrder.CustomerName = order.Element("CustomerName")?.Value;
                myOrder.CustomerAdress = order.Element("CustomerAdress")?.Value;
                myOrder.CustomerEmail = order.Element("CustomerEmail")?.Value;
                DateTime.TryParse(order.Element("OrderDate")?.Value, out DateTime orderDate);
                myOrder.OrderDate = orderDate;
                DateTime.TryParse(order.Element("ShipDate")?.Value, out DateTime shipDate);
                myOrder.ShipDate = shipDate;
                DateTime.TryParse(order.Element("DeliveryDate")?.Value, out DateTime deliveryDate);
                myOrder.DeliveryDate = deliveryDate;
                someOrder.Add(myOrder);
            });
            return func == null?someOrder:someOrder.Where(func);
        }
        public DO.Order Read(int id)
        {
            XDocument doc = XDocument.Load(@"..\xml\Order.xml");
            var xmlOrders = doc.Descendants("Order");
            XElement? xOrder = xmlOrders.ToList().Find(item => Convert.ToInt32(item.Element("ID")?.Value) == id);
            if (Convert.ToInt32(xOrder?.Element("ID")?.Value) == 0)
                throw new ObjectNotFoundException();
            DO.Order order = new DO.Order()
            {
                ID = Convert.ToInt32(xOrder?.Element("ID")?.Value),
                CustomerName = xOrder?.Element("CustomerName")?.Value,
                CustomerEmail = xOrder?.Element("CustomerEmail")?.Value,
                CustomerAdress = xOrder?.Element("CustomerAdress")?.Value,
                OrderDate = Convert.ToDateTime(xOrder?.Element("OrderDate")?.Value),
                ShipDate = Convert.ToDateTime(xOrder?.Element("ShipDate")?.Value),
                DeliveryDate = Convert.ToDateTime(xOrder?.Element("DeliveryDate")?.Value),
            };
           // order.ID = Convert.ToInt32(xOrder?.Element("ID")?.Value);
            return order;

        }
        public DO.Order ReadByFunc(Predicate<DO.Order> func)
        {
            IEnumerable<DO.Order> orders = GetAll();
            return orders.ToList().Find(func);
        }
        public void Delete(int id)
        {
            XDocument doc = XDocument.Load(@"..\xml\Order.xml");
            var xmlOrders = doc.Descendants("Order");
            xmlOrders.ToList().Find(item => Convert.ToInt32(item.Element("ID")?.Value) == id)?.Remove();
            doc.Element("Orders")?.ReplaceAll(xmlOrders);
            doc.Save(@"..\xml\Order.xml");
            return;
        }
        public void Update(DO.Order item)
        {
          Delete(item.ID);
          Create(item);
        }
    }
}
