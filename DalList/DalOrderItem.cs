using DO;
using static Dal.DataSource;
using DalApi;
using System.Linq;

namespace Dal
{
    internal class DalOrderItem :IOrderItem
    {
        public int Create(DO.OrderItem item)
        {
            //add 
            if (DataSource.SIZEOFARRAYPRUDOCT == DataSource._orderItems.Count)
                throw new TheArrayIsFull() ;
            else
            {
               for (int i = 0; i < DataSource._orderItems.Count; i++)
                {
                    if (DataSource._orderItems[i].ProductId== item.ProductId&& DataSource._orderItems[i].OrderId == item.OrderId)
                    {
                        OrderItem orederItemTemp = DataSource._orderItems[i];
                        orederItemTemp.Amount += item.Amount;
                        DataSource._orderItems[i] = orederItemTemp;
                        return DataSource._orderItems[i].ID;
                    } 
               }
                int tempOrderItemId = DataSource.Config.OrderItemId;
                item.ID = tempOrderItemId;
                DataSource._orderItems.Add(item);
                return tempOrderItemId;
            }
        }
        public OrderItem ReadByFunc(Predicate<OrderItem> func)
        {
            return DataSource._orderItems.Find(func);
        }
        public IEnumerable<OrderItem> GetAll(Func<OrderItem, bool>? func = null)
        {
            return (func == null)? DataSource._orderItems:DataSource._orderItems.Where<OrderItem>(func);
        }
        public  DO.OrderItem Read(int id)
        {
            for (int i = 0; i < DataSource._orderItems.Count; i++)
                if (DataSource._orderItems[i].ID == id)
                    return DataSource._orderItems[i];
            throw new ObjectNotFoundException();
        }
        public void Update(DO.OrderItem item)
        {
            // OrderItem curOrderItem = DataSource._orderItems.Find(orderItem => orderItem.ID == item.ID) orderItem == item ;
            for (int i = 0; i < DataSource._orderItems.Count; i++)
                if (DataSource._orderItems[i].ID == item.ID)
                    DataSource._orderItems[i] = item;
        }
        public void Delete(int Id)
        {
            for (int i = 0; i < DataSource._orderItems.Count; i++)
            {
                if (DataSource._orderItems[i].ID == Id)
                {
                    DataSource._orderItems.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
