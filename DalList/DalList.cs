
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
namespace Dal
{
  sealed  internal class DalList:IDal
    {
        DalList()
        {
                
        }
      //  private XDocument m_XDocument=XDocument.Load(@"c:\" + "DalXml");
        //private XmlSerializer m_XmlSerializer= new XmlSerializer(typeof(EmployeeDTO));;
      //  m_XDocument = 
            // m_XmlSerializer 

        public static IDal Instance { get; } = new DalList();
        public IOrder Order => new DalOrders();
        public IOrderItem orderItem => new DalOrderItem();
        public IProduct product=>new DalProduct();
    }
}
