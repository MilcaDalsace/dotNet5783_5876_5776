using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;
using DalApi;

namespace BlImplementation
{
    internal class BlProduct : BLApi.IProduct
    {

        IDal CDal = new Dal.DalList();
        /// <summary>
        /// A function that returns a list of products
        /// </summary>
        public IEnumerable<BO.ProductForList> GetProductList()
        {
            IEnumerable<DO.Product> ListOfProduct = CDal.product.GetAll();
            List<BO.ProductForList> ProductListToReturn = new List<BO.ProductForList>();
            //IEnumerable<BO.ProductForList> ProductListToReturn = new List<BO.ProductForList>();
            foreach (DO.Product Product in ListOfProduct)
            {
                BO.ProductForList productForList = new BO.ProductForList()
                {
                    ID = 0,
                    ProductName = Product.Name,
                    Price = Product.Price,
                    Category = (BO.Categories)Product.Category
                };
                ProductListToReturn.Add(productForList);
            }
            return ProductListToReturn;
        }
        /// <summary>
        /// A function that returns a product catalog
        /// </summary>
        public IEnumerable<BO.ProductItem> GetCatalog()
        {
            IEnumerable<DO.Product> ListOfProduct = CDal.product.GetAll();
            List<BO.ProductItem> ProductListToReturn = new List<BO.ProductItem>();
            foreach (DO.Product Product in ListOfProduct)
            {
                //ID
                BO.ProductItem productForList = new BO.ProductItem()
                {
                    ID = 0,
                    ProductName = Product.Name,
                    Price = Product.Price,
                    Category = (BO.Categories)Product.Category,
                    Available = Product.InStock > 0 ? true : false,
                    //אין קונה לבנתיים לכן 0
                    AmountInCart = 0
                };
                ProductListToReturn.Add(productForList);
            }
            return ProductListToReturn;
        }
        /// <summary>
        /// A function that receives a product code and returns its details
        /// </summary>
        public BO.Product GetProductDetails(int idProduct)
        {
            if (idProduct > 0)
            {
                try
                {
                    DO.Product productRead = CDal.product.Read(idProduct);
                    BO.Product productToReturn = new BO.Product()
                    {
                        ID = productRead.ID,
                        InStock = productRead.InStock,
                        Name = productRead.Name,
                        Price = productRead.Price,
                        Category = (BO.Categories)productRead.Category,
                    };
                    return productToReturn;
                }
                catch (ObjectNotFoundException ex) { throw new BO.NoSuchObjectExcption(ex); };
            }
            else
                throw new BO.OneFieldsInCorrect();
        }
        /// <summary>
        /// A function that receives a product and adds it to the list of products
        /// </summary>
        public void AddProduct(BO.Product ProductToAdd)
        {
            if (ProductToAdd.ID < 0 || ProductToAdd.Name == "" || ProductToAdd.Price < 0 || ProductToAdd.InStock < 0)
                throw new BO.OneFieldsInCorrect();
            DO.Product productToAdd = new DO.Product()
            {
                ID = ProductToAdd.ID,
                Name = ProductToAdd.Name,
                InStock = ProductToAdd.InStock,
                Price = ProductToAdd.Price,
            };
            try { CDal.product.Create(productToAdd); }
            catch (TheArrayIsFull ex)
            { throw new BO.TheArrayIsFullException(ex); };
        }
        /// <summary>
        /// A function that receives a product and deletes it from the list of products
        /// </summary>
        public void DeleteProduct(int idProduct)
        {
            IEnumerable<DO.OrderItem> allOrderItems = CDal.orderItem.GetAll();
            foreach (DO.OrderItem orderItem in allOrderItems)
            {
                if (orderItem.ProductId == idProduct)
                {
                    throw new BO.ProductInOrder();
                }
            }
            try { CDal.product.Delete(idProduct); }
            catch (ObjectNotFoundException ex) { throw new BO.NoSuchObjectExcption(ex); };
        }
        /// <summary>
        /// A function that receives a product and updates it in the product list
        /// </summary>
        
        public void UpdateProduct(BO.Product productToUpdate)
        {
            if (productToUpdate.ID < 0 || productToUpdate.Name == "" || productToUpdate.Price < 0 || productToUpdate.InStock < 0)
                throw new BO.OneFieldsInCorrect();
            DO.Product productToAdd = new DO.Product()
            {
                InStock = productToUpdate.InStock,
                ID = productToUpdate.ID,
                Name = productToUpdate.Name,
                Price = productToUpdate.Price,
                Category = (DO.Categories)productToUpdate.Category
            };
            try
            {
                CDal.product.Update(productToAdd);
            }
            catch (ObjectNotFoundException ex)
            {
                throw new BO.NoSuchObjectExcption(ex);
            }
        }
    }
}