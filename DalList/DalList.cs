﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
namespace Dal
{
  sealed  public class DalList:IDal
    {
        public IOrder Order => new DalOrders();
        public IOrderItem orderItem => new DalOrderItem();
        public IProduct product=>new DalProduct();
    }
}