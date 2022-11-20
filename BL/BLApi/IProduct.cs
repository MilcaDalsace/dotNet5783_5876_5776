using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public interface IProduct
    {
        public IEnumerable<ProductForList> GetProductList();
        public IEnumerable<ProductItem> GetCatalog();
        public Product GetProductDetails(int idProduct);
        public void AddProduct(Product ProductToAdd);
        public void DeleteProduct(int idProduct);
        public void UpdateProduct(Product productToUpdate);
    }
}
