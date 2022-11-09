
using DO;
using Dal;
namespace Dal
{
    public class DalProduct
    {
        public int Create(DO.Product newPrudoct)
        {
            if (DataSource.Config._productNum == DataSource._products.Length) 
            {
                throw new Exception("the array is full");
            }
            else
            {
                int tempPrudoctNum = DataSource.Config._productNum++;
                int newPrudoctId = DataSource.Config.ProductId;
                newPrudoct.ID = newPrudoctId;
                DataSource._products[tempPrudoctNum] = newPrudoct;
                return newPrudoctId;
            }
        }
        public DO.Product[] ReadAll()
        {
            DO.Product[] tempProducts = new DO.Product[DataSource._products.Length];
            for (int i = 0; i < tempProducts.Length; i++)
            {
                if (DataSource._products[i].ID == 0)
                    i= tempProducts.Length;
                else
                tempProducts[i] = DataSource._products[i];
            }
            return tempProducts;
        }
        public DO.Product Read(int productToReadId)
        {
            for (int i = 0; i < DataSource._products.Length; i++)
                if (DataSource._products[i].ID == productToReadId)
                    return DataSource._products[i];
            throw new Exception("product not found");
        }
        public void Update(DO.Product ProductToUpdate)
        {
            for (int i = 0; i < DataSource._products.Length; i++)
                if (DataSource._products[i].ID == ProductToUpdate.ID)
                {
                    DataSource._products[i] = ProductToUpdate;
                    return;
                }
            throw new Exception("Id of product not found.");
        }
        public void Delete(int id)
        {
            for (int i = 0; i < DataSource._products.Length; i++)
            {
                if (DataSource._products[i].ID == id)
                {
                    DataSource._products[i] = DataSource._products[DataSource.Config._productNum--];
                    return;
                }
            }
            throw new Exception("Product not found.");
        }

    }
}
