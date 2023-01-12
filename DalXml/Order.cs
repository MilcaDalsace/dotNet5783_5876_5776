using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    public class Order:IOrder
    {
        //XElement root;
        //string path = @"..\xml\Order.xml";
        //public XmlOrder() => root = new XElement("order");
        public int Create(DO.Order item)
        {
            XDocument doc = XDocument.Load(@"..\xml\Order.xml");
            XElement root = new XElement("Order");
            root.Add(new XElement("ID", 123));
            root.Add(new XElement("CustomerName", item.CustomerName));
            root.Add(new XElement("CustomerEmail", item.CustomerEmail));
            root.Add(new XElement("CustomerAdress", item.CustomerAdress));
            root.Add(new XElement("OrderDate", item.OrderDate));
            root.Add(new XElement("ShipDate", item.ShipDate));
            root.Add(new XElement("DeliveryDate", item.DeliveryDate));
            doc.Element("Order").Add(root);
            doc.Save(@"..\xml\Order.xml");
            return item.ID;
        }
        //public  GetAll();
        public IEnumerable<DO.Order> GetAll(Func<DO.Order, bool>? func = null)
        {
            IEnumerable<DO.Order> aa=new List<DO.Order>();
            return aa;
        }
        public DO.Order Read(int id)
        { return new DO.Order(); }
        public DO.Order ReadByFunc(Predicate<DO.Order> func)
        {
            return new DO.Order();
        }
        public void Delete(int id)
        {
        }
        public void Update(DO.Order item)
        {

        }
    }
}
