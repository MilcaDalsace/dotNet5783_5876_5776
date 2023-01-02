using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed internal class DalXml : IDal
    {
        public IOrder order { get; } = new Dal.Order();
        public IProduct product { get; } = new Dal.Product();
        public IOrderItem orderItem { get; } = new Dal.OrderItem();

    }
}
