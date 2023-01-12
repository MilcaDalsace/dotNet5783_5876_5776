using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DalApi;
using DO;

namespace Dal
{
    internal class OrderItem:IOrderItem
    {
        public int Create(DO.OrderItem item)
        {
            return 1;
        }
        //public  GetAll();
        public IEnumerable<DO.OrderItem> GetAll(Func<DO.OrderItem, bool>? func = null)
        {
            IEnumerable<DO.OrderItem> aa = new List<DO.OrderItem>();
            return aa;
        }
        public DO.OrderItem Read(int id)
        { return new DO.OrderItem(); }

       
        public DO.OrderItem ReadByFunc(Predicate<DO.OrderItem> func)
        {
            return new DO.OrderItem();
        }
        public void Delete(int id)
        {
        }
        public void Update(DO.OrderItem item)
        {

        }
    }
}
