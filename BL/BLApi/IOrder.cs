using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BLApi
{
    /// <summary>
    /// Interface for displaying a order
    /// </summary>
    public interface IOrder
    {
        public IEnumerable<OrderForList> GetListOrder();
        public Order GetOrderDetails(int idOrder);
        public Order UpdateOrderSent(int idOrder);
        public Order UpdateOrderSupply(int idOrder);
       //public Order UpdateOrder(int idOrder,Order newOrder);
    }
}
