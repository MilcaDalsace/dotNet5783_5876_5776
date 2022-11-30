using DO;
using static Dal.DataSource;
using DalApi;
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
               //if
                int tempOrderItemId = DataSource.Config.OrderItemId;
                item.ID = tempOrderItemId;
                DataSource._orderItems.Add(item);
                return tempOrderItemId;
            }
        }
        public IEnumerable<OrderItem> GetAll()
        {
            List<OrderItem> tempOrderItemArray = new List<OrderItem> { };
            
            for (int i = 0; i < DataSource._orderItems.Count; i++)
                tempOrderItemArray.Add(DataSource._orderItems[i]);
            return tempOrderItemArray;
        }
        public DO.OrderItem ReadByOrderitemId(int orderId,int productId)
        {
            for (int i = 0; i < DataSource._orderItems.Count; i++)
                if (DataSource._orderItems[i].ProductId == productId&& DataSource._orderItems[i].OrderId == orderId)
                    return DataSource._orderItems[i];
            throw new ObjectNotFoundException();
        }
        //2 פונקציות שהוספתי!!
       public  DO.OrderItem Read(int id)
        {
            for (int i = 0; i < DataSource._orderItems.Count; i++)
                if (DataSource._orderItems[i].ID == id)
                    return DataSource._orderItems[i];
            throw new ObjectNotFoundException();
        }
        public IEnumerable<OrderItem> ReadByOrderId(int orderId)
        {
            int placeInNewArray = 0;
            List<OrderItem> arrayOfOrderItem = new List<OrderItem> { };
            //DO.OrderItem[] arrayOfOrderItem = new DO.OrderItem[DataSource._orderItems.Count];
            for (int i = 0; i < DataSource._orderItems.Count; i++)
                if (DataSource._orderItems[i].OrderId == orderId)
                    arrayOfOrderItem.Add(DataSource._orderItems[i]) ;
            //if (arrayOfOrderItem[0].ID == 0)
            //    return null;
            return arrayOfOrderItem;
        }
        public void Update(DO.OrderItem item)
        {
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
