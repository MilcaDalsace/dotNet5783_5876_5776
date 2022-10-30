using DO;
using static Dal.DataSource;
namespace Dal;

public class DalOrders
{
    public int Create(DO.Order newOrder)
    {
        if (DataSource.Config._orderNum == DataSource._orders.Length)
            throw new Exception("the array is full");
        else
        {
            int newOrderNum = DataSource.Config._orderNum++;
            int newOrderId = DataSource.Config.OrderId;
            newOrder.ID = newOrderId;
            DataSource._orders[newOrderNum] = newOrder;
            return newOrderId;
        }
    }
    public DO.Order[] ReadAll()
    {
        DO.Order[] tempOrderArray = new DO.Order[DataSource._orders.Length];
        for (int i = 0; i < tempOrderArray.Length; i++)
        {
            tempOrderArray[i] = DataSource._orders[i];
        }
        return tempOrderArray;
    }
    public DO.Order Read(int idOrderToread)
    {
        for (int i = 0; i < DataSource._orders.Length; i++)
            if (DataSource._orders[i].ID == idOrderToread)
                return DataSource._orders[i];
        throw new Exception("the order not found");
    }
    public void Update(DO.Order OrderToUpdate)
    {
        for (int i = 0; i < DataSource._orders.Length; i++)
            if (DataSource._orders[i].ID == OrderToUpdate.ID)
                DataSource._orders[i] = OrderToUpdate;
        throw new Exception("order to update not found");
    }
    public void Delete(int Id)
    {
        for (int i = 0; i < DataSource._orders.Length; i++)
        {
            if (DataSource._orders[i].ID == Id)
            {
                DataSource._orders[i] = DataSource._orders[DataSource.Config._orderNum--];
                return;
            }
        }
    }
}
