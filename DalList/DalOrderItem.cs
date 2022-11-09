using DO;
using static Dal.DataSource;
namespace Dal
{
    public class DalOrderItem
    {
        public int Create(DO.OrderItem newOrderItem)
        {
            if (DataSource.Config._orderItemNum == DataSource._orderItems.Length)
                throw new Exception("the array is full");
            else
            {
               for (int i = 0; i < DataSource._orderItems.Length; i++)
                {
                    if (DataSource._orderItems[i].ProductId== newOrderItem.ProductId&& DataSource._orderItems[i].OrderId == newOrderItem.OrderId)
                    {
                    DataSource._orderItems[i].Amount += newOrderItem.Amount;
                    return DataSource._orderItems[i].ID;
                    } 
               }
                int tempOrderItemNum = DataSource.Config._orderItemNum++;
                int tempOrderItemId = DataSource.Config.OrderItemId;
                newOrderItem.ID = tempOrderItemId;
                DataSource._orderItems[tempOrderItemNum] = newOrderItem;
                return tempOrderItemId;
            }
        }
        public DO.OrderItem[] ReadAll()
        {
            DO.OrderItem[] tempOrderItemArray = new DO.OrderItem[DataSource._orderItems.Length];
            for (int i = 0; i < tempOrderItemArray.Length; i++)
                tempOrderItemArray[i] = DataSource._orderItems[i];
            return tempOrderItemArray;
        }
        public DO.OrderItem Read(int orderId,int productId)
        {
            for (int i = 0; i < DataSource._orderItems.Length; i++)
                if (DataSource._orderItems[i].ProductId == productId&& DataSource._orderItems[i].OrderId == orderId)
                    return DataSource._orderItems[i];
            throw new Exception("The item is not on order");
        }
        //2 פונקציות שהוספתי!!
        public DO.OrderItem ReadByOrderitemId(int orderItemId)
        {
            for (int i = 0; i < DataSource._orderItems.Length; i++)
                if (DataSource._orderItems[i].ID == orderItemId)
                    return DataSource._orderItems[i];
            throw new Exception("The item is not on order");
        }
        public DO.OrderItem[] ReadByOrderId(int orderId)
        {
            int placeInNewArray = 0;
            DO.OrderItem[] arrayOfOrderItem = new DO.OrderItem[DataSource._orderItems.Length];
            for (int i = 0; i < DataSource._orderItems.Length; i++)
                if (DataSource._orderItems[i].OrderId == orderId)
                    arrayOfOrderItem[placeInNewArray++] = DataSource._orderItems[i];
            return arrayOfOrderItem;
        }
        public void Update(DO.OrderItem orderItemsToUpdate)
        {
            for (int i = 0; i < DataSource._orderItems.Length; i++)
                if (DataSource._orderItems[i].ID == orderItemsToUpdate.ID)
                    DataSource._orderItems[i] = orderItemsToUpdate;
        }
        public void Delete(int Id)
        {
            for (int i = 0; i < DataSource._orderItems.Length; i++)
            {
                if (DataSource._orderItems[i].ID == Id)
                {
                    DataSource._orderItems[i] = DataSource._orderItems[DataSource.Config._orderNum--];
                    return;
                }
            }
        }
    }
}
