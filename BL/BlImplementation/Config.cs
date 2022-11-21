using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
  
    internal static class Config
    {
        static internal int _productId = 1;
        static internal int _orderId = 1;
        static internal int _orderItemId = 1;
        static public int ProductId
        {
            get { return _productId++; }
        }
        static public int OrderId
        {
            get { return _orderId++; }
        }
        static public int OrderItemId
        {
            get { return _orderItemId++; }
        }
    }

}
