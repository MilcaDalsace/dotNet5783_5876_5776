using DO;
using static Dal.DataSource;
using DalApi;
namespace Dal;

internal class DalOrders:IOrder
{
 public  int  Create(DO.Order newOrder)
    {
        if (DataSource.SIZEOFARRAYORDER == DataSource._orders.Count)
            throw new TheArrayIsFull();
        else
        {
            int newOrderId = DataSource.Config.OrderId;
            newOrder.ID = newOrderId;
            DataSource._orders.Add(newOrder);
            return newOrderId;
        }
    }
    public IEnumerable<Order> GetAll()
    {
        List<Order> tempOrderArray = new List<Order>();
       // DO.Order[] tempOrderArray = new DO.Order[DataSource._orders.Count];
        for (int i = 0; i < DataSource._orders.Count; i++)
        {
            tempOrderArray.Add( DataSource._orders[i]);
        }
        return tempOrderArray;
    }
  public Order Read(int id)
    {
        for (int i = 0; i < DataSource._orders.Count; i++)
            if (DataSource._orders[i].ID == id)
                return DataSource._orders[i];
        throw new ObjectNotFoundException();
    }
     public void Update(DO.Order OrderToUpdate)
    {
        for (int i = 0; i < DataSource._orders.Count; i++) { 
            if (DataSource._orders[i].ID == OrderToUpdate.ID) { 
                DataSource._orders[i] = OrderToUpdate;
                return;}
            }
        throw new ObjectNotFoundException();
    }
   public void Delete(int Id)
    {
        for (int i = 0; i < DataSource._orders.Count; i++)
        {
            if (DataSource._orders[i].ID == Id)
            {
                DataSource._orders.RemoveAt(i);
                return;
            }
        }
    }
}
