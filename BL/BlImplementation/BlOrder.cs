using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLApi;
using BO;
using DalApi;

namespace BlImplementation;

internal class BlOrder : BLApi.IOrder
{
    IDal CDal = DalApi.Factory.Get();
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
            list.ToList().ForEach(order =>
            {
                IEnumerable<DO.OrderItem> orderItems = CDal.orderItem.GetAll(item => item.OrderId == order.ID);
                orderItems.ToList().ForEach(item => sum += item.Price * item.Amount);
                listToReturn.Add(new BO.OrderForList()
                {
                    ID = order.ID,
                    FinalPrice = sum,
                    AmountItems = orderItems.Count(),
                    OrderSatus = orderStatus(order),
                    CustomerName = order.CustomerName
                });
                sum = 0;
            }
            );
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
        IEnumerable<DO.OrderItem> orderItemsIenum = CDal.orderItem.GetAll();
        List<BO.OrderItem> orderItems = new List<BO.OrderItem>();
        orderItemsIenum.ToList().ForEach(orderItem =>
        {
            if (orderItem.OrderId == idOrder)
                orderItems.Add(new BO.OrderItem()
                {
                    Amount = orderItem.Amount,
                    Id = orderItem.ID,
                    Price = orderItem.Price,
                    FinalPrice = orderItem.Amount * orderItem.Price,
                    ProductId = orderItem.ProductId,
                    ProductName = CDal.product.Read(orderItem.ProductId).Name
                });
        });
        if (idOrder > 0)
        {
            try
            {
                currOrder = CDal.Order.Read(idOrder);
                orderItems.ToList().ForEach(item=>sum += item.Price * item.Amount);   
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
                OrderItemList = orderItems
            };
            return orderToReturn;
        }
        throw new BO.OneFieldsInCorrect();
    }
    /// <summary>
    ///A function that receives an order code and updates an order shipment 
    /// </summary>
    public Order UpdateOrderSent(int idOrder)
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
    public Order UpdateOrderSupply(int idOrder)
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
                if (orderStatus(order) == BO.Status.received)
                    throw new BO.OrderDidnotsentAlready();
                else
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
    public void UpdateOrder(int idOrder, int idProduct, int CurAmount, int action)
    {//price
        try
        {
            switch (action)
            {
                case 1:
                    DO.OrderItem baseItem = CDal.orderItem.ReadByFunc(item => item.OrderId == idOrder && item.ProductId == idProduct);
                    CDal.orderItem.Delete(baseItem.ID);
                    break;
                case 2:
                    DO.OrderItem newOrder = new DO.OrderItem()
                    {
                        Amount = CurAmount,
                        OrderId = idOrder,
                        ProductId = idProduct,
                        //Price=?
                    };
                    //add try
                    CDal.orderItem.Create(newOrder);
                    break;
                case 3:
                    float price = CDal.product.Read(idProduct).Price;
                    DO.OrderItem baseItemForUpdate = CDal.orderItem.ReadByFunc(item => item.OrderId == idOrder && item.ProductId == idProduct);
                    baseItemForUpdate.Amount = CurAmount;
                    baseItemForUpdate.Price = price * CurAmount;
                    CDal.orderItem.Update(baseItemForUpdate);
                    break;
            }
        }
        catch (ObjectNotFoundException ex)
        {
            throw new BO.NoSuchObjectExcption(ex);

        }
        //DO.Order order = CDal.Order.Read(idOrder);
        //int baseAmount=baseItemForUpdate.Amount;
        //try
        //{
        //    float newFinalPrice=0;
        //    List<DO.OrderItem> listOfProducts = new List<DO.OrderItem>();
        //    DO.Order myOrder = CDal.Order.Read(idOrder);
        //    try {
        //    DO.OrderItem baseItem = CDal.orderItem.ReadByOrderitemId(idOrder,idProduct);
        //    }
        //    catch
        //    {

        //    }
        //    if(baseItem.Amount< CurAmount)
        //    //foreach (DO.OrderItem item in allItems)
        //    //{
        //    //    if (item.OrderId == idOrder) {
        //    //        newFinalPrice += item.Amount * item.Price;
        //    //        listOfProducts.Add(item);
        //    //    }
        //    //}
        //    int diff; 
        //foreach (BO.OrderItem itemUpdate in newrder)
        //    for (int i=0;i< listOfProducts.Count;i++)
        //    {
        //        if (listOfProducts[i].ProductId == itemUpdate.ProductId) 
        //        {
        //            if (listOfProducts[i].Amount < itemUpdate.Amount)
        //            {
        //                diff = itemUpdate.Amount - listOfProducts[i].Amount;
        //                DO.OrderItem temp = listOfProducts[i];
        //                temp.Amount = itemUpdate.Amount;
        //                listOfProducts[i] = temp;
        //                //temp.FinalPrice = itemUpdate.Amount * itemUpdate.Price;
        //                newFinalPrice +=diff* itemUpdate.Price;
        //            }

        //        }
        //    }
    }

    public BO.OrderTracking GetOrderTracking(int idOrder)
    {
        BO.OrderTracking orderTracking = new BO.OrderTracking();
        try
        {
            BO.Order order = new BO.Order();
            order = GetOrderDetails(idOrder);
            DO.Order orderDO = new DO.Order
            { 
                CustomerAdress = order.CustomerAdress,
                CustomerEmail = order.CustomerEmail,
                CustomerName = order.CustomerName,
                DeliveryDate = order.DeliveryDate,
                ID = idOrder,
                OrderDate = order.OrderDate,
                ShipDate= order.ShipDate
            };
            orderTracking.ID = order.ID;
            orderTracking.OrderStatus = order.OrderStatus;
            orderTracking.DateAndStatus = new List<(DateTime, string)?> { };
            orderTracking.DateAndStatus.Add((order.OrderDate, "Received"));
            BO.Status a= orderStatus(orderDO);
            if (orderStatus(orderDO) == BO.Status.arrived)
            {
            orderTracking.DateAndStatus.Add((order.ShipDate, "Sent"));
            orderTracking.DateAndStatus.Add((order.DeliveryDate, "Arrived"));
            }
            else
                if(orderStatus(orderDO) == BO.Status.sent)
                orderTracking.DateAndStatus.Add((order.ShipDate, "Sent"));
        }
        catch (ObjectNotFoundException ex)
        {
            throw new BO.NoSuchObjectExcption(ex);
        }
        return orderTracking;
    }
}

