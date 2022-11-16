using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IDoOrderItem:ICrud<OrderItem>
    {
        public OrderItem ReadByOrderitemId(int orderItemId);
        public IEnumerable<OrderItem> ReadByOrderId(int orderId);
    }
}
