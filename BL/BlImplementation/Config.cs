using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
  
    internal static class Config
    {
        static internal int _productForList = 1;
        static internal int _productItem = 1;
        static internal int _orderItemId = 1;
        static public int ProductForList
        {
            get { return _productForList++; }
        }
        static public int ProductItem
        {
            get { return _productItem++; }
        }
        static public int OrderItemId
        {
            get { return _orderItemId++; }
        }
    }

}
