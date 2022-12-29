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
    public Order ReadByFunc(Predicate<Order> func)
    {
        return DataSource._orders.Find(func);
    }
    public Order Read(int id)
    {
        Order order=DataSource._orders.Find(item=>item.ID==id);
        if(order.ID==0)
            throw new ObjectNotFoundException();
        return order;
    }
     public void Update(DO.Order OrderToUpdate)
    {
        Order order = Read(OrderToUpdate.ID);
       int index= DataSource._orders.IndexOf(order);
        DataSource._orders[index] = OrderToUpdate;
        //for (int i = 0; i < DataSource._orders.Count; i++) { 
        //    if (DataSource._orders[i].ID == OrderToUpdate.ID) { 
        //        DataSource._orders[i] = OrderToUpdate;
        //        return;}
        //    }
        //throw new ObjectNotFoundException();
    }
   public void Delete(int Id)
    {
        Order order = Read(Id);
        _orders.Remove(order);
    }
}
