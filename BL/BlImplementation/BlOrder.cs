using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLApi;
using DalApi;

namespace BlImplementation
{
    internal class BlOrder : BLApi. IOrder
    {
        IDal CDal = new Dal.DalList();
        /// <summary>
        /// A function that returns a list of orders
        /// I don't get anything
        /// </summary>
        public IEnumerable<BO.OrderForList> GetListOrder()
        {
            float sum = 0;
            List<BO.OrderForList> listToReturn = new List<BO.OrderForList>();
            IEnumerable<DO.Order> list = CDal.Order.GetAll();
            try
            {
                foreach (DO.Order order in list)
                {
                    IEnumerable<DO.OrderItem> orderItems = CDal.orderItem.ReadByOrderId(order.ID);
                    foreach (DO.OrderItem item in orderItems)
                        sum += item.Price * item.Amount;
                   
                    BO.OrderForList orderForList = new BO.OrderForList()
                    {
                        ID = order.ID,
                        FinalPrice = sum,
                        AmountItems = orderItems.Count(),
                        OrderSatus = orderStatus(order),
                        CustomerName = order.CustomerName
                    };
                    sum = 0;
                    listToReturn.Add(orderForList);
                }
            }
            catch (ObjectNotFoundException ex)
            {
                throw new BO.NoSuchObjectExcption(ex);
            }
            return listToReturn;
        }
        /// <summary>
        /// A function that accepts an order and checks its status
        /// </summary>
        public BO.Status orderStatus(DO.Order orderToChek)
        {
            if (DateTime.Compare(orderToChek.DeliveryDate, DateTime.Today) > 0)
                return BO.Status.arrived;
            if (DateTime.Compare(orderToChek.ShipDate, DateTime.Today) > 0)
                return BO.Status.sent;
            return BO.Status.received;
        }
        /// <summary>
        /// A function that receives an order code and returns its details
        /// </summary>
        public BO.Order GetOrderDetails(int idOrder)
        {
            float sum = 0;
            DO.Order currOrder = new DO.Order();
            List<DO.OrderItem> orderItems = new List<DO.OrderItem>();
            if (idOrder > 0)
            {
                try
                {
                    currOrder = CDal.Order.Read(idOrder);
                    foreach (DO.OrderItem item in orderItems)
                        sum += item.Price;
                }
                catch (ObjectNotFoundException ex)
                {
                    throw new BO.NoSuchObjectExcption(ex);
                }
                BO.Order orderToReturn = new BO.Order()
                {
                    ID = idOrder,
                    OrderStatus = orderStatus(currOrder),
                    FinalPrice = sum,
                    CustomerName = currOrder.CustomerName,
                    CustomerAdress = currOrder.CustomerAdress,
                    CustomerEmail = currOrder.CustomerEmail,
                    DeliveryDate = currOrder.DeliveryDate,
                    OrderDate = currOrder.OrderDate,
                    ShipDate = currOrder.ShipDate,
                };
                return orderToReturn;
            }
            throw new BO.OneFieldsInCorrect();
        }
        /// <summary>
        ///A function that receives an order code and updates an order shipment 
        /// </summary>
        public BO.Order UpdateOrderSent(int idOrder)
        {
            //גם בישות הלוגית לעדכן?? לא יודעת מה זה...
            DO.Order orderToUpdate = new DO.Order();
            try
            {
                orderToUpdate = CDal.Order.Read(idOrder);
                if (orderStatus(orderToUpdate) == BO.Status.received)
                {
                    orderToUpdate.ShipDate = DateTime.Now;
                    CDal.Order.Update(orderToUpdate);
                }
                else
                {
                    throw new BO.OrderAlreadyUpdate();
                }
                return GetOrderDetails(idOrder);
            }
            catch (ObjectNotFoundException ex)
            {
                throw new BO.NoSuchObjectExcption(ex);
            }
        }
        /// <summary>
        /// A function that receives an order code and updates an order supply
        /// </summary>
        public BO.Order UpdateOrderSupply(int idOrder)
        {
            try
            {
                DO.Order order = CDal.Order.Read(idOrder);
                if (orderStatus(order) == BO.Status.sent)
                {
                    order.DeliveryDate = DateTime.Now;
                }
                else
                {
                    throw new BO.OrderAlreadyUpdate();
                }
                CDal.Order.Update(order);
                return GetOrderDetails(idOrder);
            }
            catch (ObjectNotFoundException ex)
            {
                throw new BO.NoSuchObjectExcption(ex);
            }
        }
      //  public BO.Order UpdateOrder(int idOrder, BO.Order newrder);
    }
}
