
using DalApi;
using DO;
using static Dal.DataSource;
using System.Linq;

namespace Dal
{
    public class DalProduct:IProduct
    {
        public int Create(DO.Product item)
        {
            if (DataSource.SIZEOFARRAYPRUDOCT == DataSource._products.Count) 
            {
                throw new  TheArrayIsFull();
            }
            else
            {
                int newPrudoctId = DataSource.Config.ProductId;
                item.ID = newPrudoctId;
                DataSource._products.Add(item);
                return newPrudoctId;
            }
        }
        public IEnumerable<Product> GetAll(Func<Product, bool>? func = null)
        {
            return (func == null) ? DataSource._products : DataSource._products.Where<Product>(func);
        }
        public DO.Product Read(int id)
        {
            Product product = DataSource._products.Find(item => item.ID == id);
            if (product.ID == 0)
                throw new ObjectNotFoundException();
            return product;
        }
        public Product ReadByFunc(Predicate<Product> func)
        {
            return DataSource._products.Find(func);
        }
        public void Update(DO.Product item)
        {
            try
            {
            Product product = Read(item.ID);
            int index = DataSource._products.IndexOf(product);
            DataSource._products[index] = item;
            }
            catch (ObjectNotFoundException ex)
            {
                throw ex;
            }
        }
        public void Delete(int id)
        {
            try {
            Product product = Read(id);
            _products.Remove(product); }
            catch (ObjectNotFoundException ex)
            {
                throw ex;
            }
        }

    }
}
