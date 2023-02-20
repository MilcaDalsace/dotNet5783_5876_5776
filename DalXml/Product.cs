using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal
{
    internal class Product:IProduct
    {
        public int Create(DO.Product item)
        {
            IEnumerable<DO.Product> allProducts = GetAll().ToList();
            DO.Product? tempProduct = allProducts?.ToList().Find(item1 => item1.ID == item.ID);
            if (tempProduct?.ID == 0)
            {
                allProducts?.ToList().Add(item);
                StreamWriter stWr = new StreamWriter("../Product.xml");
                XmlSerializer seri = new XmlSerializer(typeof(List<Product>));
                seri.Serialize(stWr, allProducts);
                stWr.Close();
            }
            else
            {
                throw new ObjectAlreadyExist();
            }
            return item.ID;
        }
        //public  GetAll();
        public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
        { 
            XmlSerializer seri = new XmlSerializer(typeof(List<DO.Product>));
            StreamReader read = new StreamReader("../Product.xml");
            IEnumerable<DO.Product>? allProducts;
            allProducts = (IEnumerable<DO.Product>?)seri.Deserialize(read);
            read.Close();
            //return w
            return (IEnumerable<DO.Product>)allProducts;
        }
        public DO.Product Read(int id)
        {
           IEnumerable<DO.Product> allProducts=GetAll();
           DO.Product product=allProducts.ToList().Find(item1 => item1.ID == id);
           if(product.ID!=0)
                return product;
            throw new Exception("not exist");
        }


        public DO.Product ReadByFunc(Predicate<DO.Product> func)
        {
            IEnumerable<DO.Product> lst = GetAll();
            return lst.ToList().Find(func);
        }
        public void Delete(int id)
        {
            List<DO.Product> productList = GetAll().ToList();
            productList.Remove(Read(id));
            StreamWriter writer = new StreamWriter("../Products.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<Product>));
            ser.Serialize(writer, productList);
            writer.Close();
        }
        public void Update(DO.Product item)
        {
            Delete(item.ID);
            Create(item);
        }
    }
}
