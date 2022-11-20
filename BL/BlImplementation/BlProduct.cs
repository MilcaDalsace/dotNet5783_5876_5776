using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using BO;
using DalApi;

namespace BlImplementation
{
    internal class BlProduct : IProduct
    {
       
        IDal CDal = new Dal.DalList();
        public IEnumerable<ProductForList> GetProductList() {
            IEnumerable<DO.Product> ListOfProduct = CDal.product.GetAll();
            IEnumerable<ProductForList> ProductListToReturn = new List<ProductForList>();
            foreach(DO.Product Product in ListOfProduct)
            {
                //ID
                ProductForList productForList=new ProductForList();
                productForList.ProductName = Product.Name;
                productForList.Price=Product.Price;
                productForList.Category= (BO.Categories)Product.Category;
                ProductListToReturn.ToList().Add(productForList);
            }
            return ProductListToReturn;
        }
        public IEnumerable<ProductItem> GetCatalog()
        {
            IEnumerable<DO.Product> ListOfProduct = CDal.product.GetAll();
            IEnumerable<ProductItem> ProductListToReturn = new List<ProductItem>();
            foreach (DO.Product Product in ListOfProduct)
            {
                //ID
                ProductItem productForList = new ProductItem();
                productForList.ProductName = Product.Name;
                productForList.Price = Product.Price;
                productForList.Category = (BO.Categories)Product.Category;
                productForList.Available = Product.InStock > 0?true:false;
                //אין קונה לבנתיים לכן 0
                productForList.AmountInCart = 0;
                ProductListToReturn.ToList().Add(productForList);
            }
            return ProductListToReturn;
        }
        public Product GetProductDetails(int idProduct);
        public void AddProduct(Product ProductToAdd);
        public void DeleteProduct(int idProduct);
        public void UpdateProduct(Product productToUpdate);
    }
}
