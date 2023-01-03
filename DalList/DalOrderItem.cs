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
            if (SIZEOFARRAYPRUDOCT == _orderItems.Count)
                throw new TheArrayIsFull() ;
            else
            {
                //לא צריך את זה אבל שמרתי ליתר בטחון:
               //for (int i = 0; i < DataSource._orderItems.Count; i++)
               // {
               //     if (DataSource._orderItems[i].ProductId== item.ProductId&& DataSource._orderItems[i].OrderId == item.OrderId)
               //     {
               //         OrderItem orederItemTemp = DataSource._orderItems[i];
               //         orederItemTemp.Amount += item.Amount;
               //         DataSource._orderItems[i] = orederItemTemp;
               //         return DataSource._orderItems[i].ID;
               //     } 
               //}
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
            OrderItem orderItem =(from  item in DataSource._orderItems
                                  where item.ID == id
                                  select item).FirstOrDefault();
            if (orderItem.ID == 0)
                throw new ObjectNotFoundException();
            return orderItem;
        }
        public void Update(DO.OrderItem item)
        {
            try
            {
            OrderItem orderItem = Read(item.ID);
            int index = DataSource._orderItems.IndexOf(orderItem);
            DataSource._orderItems[index] = item;
            }
            catch (ObjectNotFoundException ex)
            {
                throw ex;
            }
            
        }
        public void Delete(int Id)
        {
            try
            {
            OrderItem orderItem = Read(Id);
            _orderItems.Remove(orderItem);
            }
            catch (ObjectNotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
