﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DalApi
{
    public interface IOrderItem:ICrud<OrderItem>
    {
        //public OrderItem ReadByOrderitemId(int orderId, int productId);
     //   public IEnumerable<OrderItem> ReadByOrderId(int orderId);
    }
}
