using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using DalApi;

namespace BlImplementation
{
    internal class BlProduct : IProduct
    {

        IDal CDal = new Dal.DalList();
        public IEnumerable<BO.ProductForList> GetProductList() {
            IEnumerable<DO.Product> ListOfProduct = CDal.product.GetAll();
            IEnumerable<BO.ProductForList> ProductListToReturn = new List<BO.ProductForList>();
            foreach (DO.Product Product in ListOfProduct)
            {
                //ID
                BO.ProductForList productForList = new BO.ProductForList();
                productForList.ProductName = Product.Name;
                productForList.Price = Product.Price;
                productForList.Category = (BO.Categories)Product.Category;
                ProductListToReturn.ToList().Add(productForList);
            }
            return ProductListToReturn;
        }
        public IEnumerable<BO.ProductItem> GetCatalog()
        {
            IEnumerable<DO.Product> ListOfProduct = CDal.product.GetAll();
            IEnumerable<BO.ProductItem> ProductListToReturn = new List<BO.ProductItem>();
            foreach (DO.Product Product in ListOfProduct)
            {
                //ID
                BO.ProductItem productForList = new BO.ProductItem();
                productForList.ProductName = Product.Name;
                productForList.Price = Product.Price;
                productForList.Category = (BO.Categories)Product.Category;
                productForList.Available = Product.InStock > 0 ? true : false;
                //אין קונה לבנתיים לכן 0
                productForList.AmountInCart = 0;
                ProductListToReturn.ToList().Add(productForList);
            }
            return ProductListToReturn;
        }
        public BO.Product GetProductDetails(int idProduct)
        {
            BO.Product productToReturn=new BO.Product() ;
            if(idProduct > 0)
            {
                try { 
                DO.Product productRead=  CDal.product.Read(idProduct);
                productToReturn.ID = productRead.ID;
                productToReturn.InStock= productRead.InStock;
                productToReturn.Name= productRead.Name;
                productToReturn.Price= productRead.Price;
                productToReturn.Category = (BO.Categories)productRead.Category; }
             catch { }
                
            }
        }
        public void AddProduct(Product ProductToAdd);
        public void DeleteProduct(int idProduct);
        public void UpdateProduct(Product productToUpdate);
    }
}
