using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    internal class Product:IProduct
    {
        public int Create(DO.Product item)
        {
            return 1;
        }
        //public  GetAll();
        public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
        {
            IEnumerable<DO.Product> aa = new List<DO.Product>();
            return aa;
        }
        public DO.Product Read(int id)
        { return new DO.Product(); }


        public DO.Product ReadByFunc(Predicate<DO.Product> func)
        {
            return new DO.Product();
        }
        public void Delete(int id)
        {
        }
        public void Update(DO.Product item)
        {

        }
    }
}
