using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    /// <summary>
    /// Interface for displaying a product
    /// </summary>
    public interface IProduct
    {
        public IEnumerable<ProductForList> GetProductList(Func<DO.Product, bool>? func = null);
        public IEnumerable<ProductItem> GetCatalog();
        public Product GetProductDetails(int idProduct);
        public void AddProduct(Product ProductToAdd);
        public void DeleteProduct(int idProduct);
        public void UpdateProduct(Product productToUpdate);
    }
}
