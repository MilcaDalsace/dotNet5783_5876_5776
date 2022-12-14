
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal
{
  sealed  internal class DalList:IDal
    {
        DalList()
        {
                
        }
        public static IDal Instance { get; } = new DalList();
        public IOrder Order => new DalOrders();
        public IOrderItem orderItem => new DalOrderItem();
        public IProduct product=>new DalProduct();
    }
}
