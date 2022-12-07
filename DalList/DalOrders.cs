using DO;
using static Dal.DataSource;
using DalApi;
using System.Linq;

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
    public IEnumerable<Order> GetAll(Func<Order, bool>? func = null)
    {
        return (func == null) ? DataSource._orders : DataSource._orders.Where<Order>(func);
    }
    public Order ReadByFunc(Func<Order, bool> func)
    {
        return DataSource._orders.Where<Order>(func).FirstOrDefault();
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
