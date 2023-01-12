using DalApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    sealed internal class DalXml : IDal
    {
        DalXml()
        {
            int a = 0;
        }
        private XDocument m_XDocument = XDocument.Load(@"c:\" + "DalXml");
        //private XmlSerializer m_XmlSerializer= new XmlSerializer(typeof(EmployeeDTO));;
        //  m_XDocument = 
        // m_XmlSerializer
        public static IDal Instance { get; } = new DalXml();
        public IOrder Order { get; } = new Dal.Order();
        public IProduct product { get; } = new Dal.Product();
        public IOrderItem orderItem { get; } = new Dal.OrderItem();

    }
}
