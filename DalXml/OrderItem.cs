using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal class OrderItem:IOrderItem
    {
        public int Create(DO.OrderItem item)
        {
            XDocument doc = XDocument.Load(@"..\xml\OrderItem.xml");
            XElement root = new XElement("OrderItem");
            root.Add(new XElement("Price", item.Price));
            root.Add(new XElement("Amount", item.Amount));
            root.Add(new XElement("ID", item.ID));
            root.Add(new XElement("ProductId", item.ProductId));
            root.Add(new XElement("OrderId", item.OrderId));
            doc.Element("OrderItems")?.Add(root);
            doc.Save(@"..\xml\OrderItem.xml");
            return item.ID;
        }
        //public  GetAll();
        public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
        {
            XDocument doc = XDocument.Load(@"..\xml\OrderItem.xml");
            var xmlOrderItem = doc.Descendants("OrderItem");
            List<DO.OrderItem> someOrderItem = new List<DO.OrderItem>();
            foreach (var orderItem in xmlOrderItem)
            {
                DO.OrderItem myOrderItem = new DO.OrderItem();
                int.TryParse(orderItem.Element("ID")?.Value, out int Id);
                myOrderItem.ID = Id;
                int.TryParse(orderItem.Element("OrderId")?.Value,out int orderId);
                myOrderItem.OrderId = orderId;
                int.TryParse(orderItem.Element("Price")?.Value, out int productPrice);
                myOrderItem.Price = productPrice;
                int.TryParse(orderItem.Element("Amount")?.Value, out int amount);
                myOrderItem.Amount = amount;
                int.TryParse(orderItem.Element("ProductId")?.Value, out int productId);
                myOrderItem.ProductId = productId;
                someOrderItem.Add(myOrderItem);
            }
            return func == null? someOrderItem: someOrderItem.Where(func);
        }
        public DO.OrderItem Read(int id)
        {
            XDocument doc = XDocument.Load(@"..\xml\OrderItem.xml");
            var xmlOrders = doc.Descendants("OrderItem");
            XElement? xOrderItem = xmlOrders.ToList().Find(item => Convert.ToInt32(item.Element("ID")?.Value) == id);
            if (Convert.ToInt32(xOrderItem?.Element("ID")?.Value) == 0)
                throw new ObjectNotFoundException();
            DO.OrderItem orderItem = new DO.OrderItem()
            {
                ID = Convert.ToInt32(xOrderItem?.Element("ID")?.Value),
                Amount =Convert.ToInt32(xOrderItem?.Element("Amount")?.Value),
                OrderId = Convert.ToInt32(xOrderItem?.Element("OrderId")?.Value),
                Price = Convert.ToInt32(xOrderItem?.Element("Price")?.Value),
                ProductId = Convert.ToInt32(xOrderItem?.Element("ProductId")?.Value)
            };
            return orderItem;
        }

       
        public DO.OrderItem ReadByFunc(Predicate<DO.OrderItem> func)
        {
            return new DO.OrderItem();
        }
        public void Delete(int id)
        {
            XDocument doc = XDocument.Load(@"..\xml\OrderItem.xml");
            var xmlOrders = doc.Descendants("OrderItem");
            xmlOrders.ToList().Find(item1 => Convert.ToInt32(item1.Element("ID")?.Value) == id)?.Remove();
        }
        public void Update(DO.OrderItem item)
        {
            XDocument doc = XDocument.Load(@"..\xml\OrderItem.xml");
            var xmlOrders = doc.Descendants("OrderItem");
            xmlOrders.ToList().Find(item1 => Convert.ToInt32(item1.Element("ID")?.Value) == item.ID)?.SetValue(item);
        }
    }
}
