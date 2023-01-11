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
    public class XmlOrder
    {
        XElement root;
        string path = @"..\xml\order.xml";
        public XmlOrder() => root = new XElement("order");
    }
}
