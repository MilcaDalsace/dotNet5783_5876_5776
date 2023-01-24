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
            XDocument doc = XDocument.Load(@"..\xml\Product.xml");
            XElement root = new XElement("Product");
            root.Add(new XElement("ID", item.ID));
            root.Add(new XElement("Name", item.Name));
            root.Add(new XElement("Price", item.Price));
            root.Add(new XElement("Category", item.Category));
            root.Add(new XElement("InStock", item.InStock));
            doc.Element("Products")?.Add(root);
            doc.Save(@"..\xml\Product.xml");
            return item.ID;
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
            return func == null?someProducts: someProducts.Where(func);
        }
        public DO.Product Read(int id)
        {
            XDocument doc = XDocument.Load(@"..\xml\Product.xml");
            var xmlOrders = doc.Descendants("Product");
            XElement? xOrder = xmlOrders.ToList().Find(item => Convert.ToInt32(item.Element("ID")?.Value) == id);
            if (Convert.ToInt32(xOrder?.Element("ID")?.Value) == 0)
                throw new ObjectNotFoundException();
            DO.Product product = new DO.Product()
            {
                ID = Convert.ToInt32(xOrder?.Element("ID")?.Value),
               
                InStock = Convert.ToInt32(xOrder?.Element("InStock")?.Value),
                Price = Convert.ToInt32(xOrder?.Element("Price")?.Value),
                Name = xOrder?.Element("Name")?.Value
            };
            //Category =(DO.Categories)(Convert.ToInt32 (xOrder?.Element("Category")?.Value)),
            return product;
        }


        public DO.Product ReadByFunc(Predicate<DO.Product> func)
        {
            return new DO.Product();
        }
        public void Delete(int id)
        {
            XDocument doc = XDocument.Load(@"..\xml\Product.xml");
            var xmlOrders = doc.Descendants("Product");
            xmlOrders.ToList().Find(item => Convert.ToInt32(item.Element("ID")?.Value) == id)?.Remove();
        }
        public void Update(DO.Product item)
        {
            XDocument doc = XDocument.Load(@"..\xml\Product.xml");
            var xmlOrders = doc.Descendants("Product");
            xmlOrders.ToList().Find(item1 => Convert.ToInt32(item1.Element("ID")?.Value) == item.ID)?.Element("ID")?.SetValue(111);
        }
    }
}
