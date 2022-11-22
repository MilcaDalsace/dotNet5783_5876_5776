﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLApi;
using DalApi;

namespace BlImplementation
{
    internal class BlOrder : IOrder
    {
        IDal CDal = new Dal.DalList();
        public IEnumerable<BO.OrderForList> GetListOrder()
        {
            float sum = 0;
            IEnumerable<BO.OrderForList> listToReturn = new List<BO.OrderForList>();
            IEnumerable<DO.Order> list = CDal.Order.GetAll();
            try
            {
                foreach (DO.Order order in list)
                {
                    IEnumerable<DO.OrderItem> orderItems = CDal.orderItem.ReadByOrderId(order.ID);
                    foreach (DO.OrderItem item in orderItems)
                        sum += item.Price * item.Amount;
                   BO. OrderForList orderForList = new BO.OrderForList()
                    {
                        ID = order.ID,
                        FinalPrice = sum,
                        AmountItems = orderItems.Count(),
                        OrderSatus = orderStatus(order),
                        CustomerName = order.CustomerName
                    };
                    listToReturn.ToList<BO.OrderForList>().Add(orderForList);
                }
            }
            catch (ObjectNotFoundException ex)
            {
                throw new BO.NoSuchObjectExcption(ex);
            }
            return listToReturn;
        }
        public BO.Status orderStatus(DO.Order orderToChek)
        {
            if (DateTime.Compare(orderToChek.DeliveryDate, DateTime.Today) < 0)
                return BO.Status.arrived;
            if (DateTime.Compare(orderToChek.ShipDate, DateTime.Today) < 0)
                return BO.Status.sent;
            return BO.Status.received;
        }
        public BO.Order GetOrderDetails(int idOrder)
        {
            float sum = 0;
            DO.Order currOrder = new DO.Order();
            IEnumerable<DO.OrderItem> orderItems = new List<DO.OrderItem>();
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
                return GetOrderDetails(idOrder);
            }
            catch (ObjectNotFoundException ex)
            {
                throw new BO.NoSuchObjectExcption(ex);
            }
        }
        public BO.Order UpdateOrderSupply(int idOrder);
        public BO.Order UpdateOrder(int idOrder,BO. Order newrder);
    }
}
//    IEnumerable<DO.OrderItem> orderItems = CDal.orderItem.ReadByOrderId(idOrder);
//    IEnumerable<BO.OrderItem> orderItemBO = new List<BO.OrderItem>();
//                foreach(DO.OrderItem item in orderItems)
//                {
//                    BO.OrderItem itemToUpdate = new BO.OrderItem()
//                    {
//                        Id = item.OrderId,
//                        ProductId = item.ProductId,
//                        FinalPrice = item.Price * item.Amount,
//                        Amount = item.Amount,
//                        ProductName = item.
//                    };
//    sum+=item.Amount* item.Price;
//}
//BO.Order orderToReturn = new BO.Order()
//{
//    FinalPrice = sum,
//    OrderItemList = orderItems,
//    ShipDate = orderToUpdate.ShipDate,
//    OrderStatus = orderStatus(orderToUpdate),
//    CustomerAdress = orderToUpdate.CustomerAdress,
//    DeliveryDate = orderToUpdate.DeliveryDate,
//    CustomerEmail = orderToUpdate.CustomerEmail,
//    CustomerName = orderToUpdate.CustomerName,
//    ID = orderToUpdate.ID,
//    OrderDate = orderToUpdate.OrderDate,
//}
//}