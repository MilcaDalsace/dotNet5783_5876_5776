using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
            XDocument doc = XDocument.Load(@"..\xml\Product.xml");
            var xmlProducts = doc.Descendants("Product");
            List<DO.Product> someProducts = new List<DO.Product>();

            foreach (var order in xmlProducts)
            {
                DO.Product myProduct = new DO.Product();
                int.TryParse(order.Element("ID")?.Value, out int productId);
                myProduct.ID = productId;
                myProduct.Name = order.Element("Name")?.Value;
                int.TryParse(order.Element("Price")?.Value, out int productPrice);
                myProduct.Price = productPrice;
                DO.Categories.TryParse(order.Element("Category")?.Value, out DO.Categories productCategories);
                myProduct.Category = productCategories;
                int.TryParse(order.Element("InStock")?.Value, out int productInStock);
                myProduct.InStock = productInStock;
                someProducts.Add(myProduct);
            }
            return someProducts;
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
