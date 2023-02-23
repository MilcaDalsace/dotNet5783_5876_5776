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
        public Order UpdateOrderSent(int idOrder);//עדכון שההזמנה נשלחה
        public Order UpdateOrderSupply(int idOrder);//עדכון שההזמנה הגיעה
        public void UpdateOrder(int idOrder, int idProduct, int CurAmount, int action);
        public BO.OrderTracking GetOrderTracking(int idOrder);
        public int? OrderSelection();
    }
}
