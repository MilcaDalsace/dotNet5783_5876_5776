using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLApi;
using BO;
using DalApi;

namespace BlImplementation
{
    internal class BlOrder:IOrder
    {
        IDal CDal = new Dal.DalList();
        public IEnumerable<OrderForList> GetListOrder()
        {
            float sum = 0;
            IEnumerable<OrderForList> listToReturn = new List<OrderForList>();
            IEnumerable<DO.Order> list= CDal.Order.GetAll();
            try
            {
                foreach (DO.Order order in list)
                {
                    IEnumerable<DO.OrderItem> orderItems = CDal.orderItem.ReadByOrderId(order.ID);
                    foreach (DO.OrderItem item in orderItems)
                        sum += item.Price * item.Amount;
                    OrderForList orderForList = new OrderForList()
                    {
                        ID = order.ID,
                        FinalPrice = sum,
                        AmountItems = orderItems.Count(),
                        OrderSatus = orderStatus(order),
                        CustomerName = order.CustomerName
                    };
                    listToReturn.ToList<OrderForList>().Add(orderForList);
                }
            }
            catch (ObjectNotFoundException ex) {
                throw new BO.NoSuchObjectExcption(ex);
            }
            return listToReturn;
        }
        public BO.Status orderStatus(DO.Order orderToChek)
        {
            if (DateTime.Compare(orderToChek.ShipDate, DateTime.Today) < 0)
                return BO.Status.arrived;
            if (DateTime.Compare(orderToChek.DeliveryDate, DateTime.Today) < 0)
                    return BO.Status.sent;
            return BO.Status.received;
        }
        public Order GetOrderDetails(int idOrder);
        public Order UpdateOrderSent(int idOrder);
        public Order UpdateOrderSupply(int idOrder);
        public Order UpdateOrder(int idOrder, Order newrder);
    }
}
