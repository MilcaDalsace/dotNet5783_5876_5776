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
      //  static int id=5;
        public int Create(DO.Product item)
        {
            if (item.ID == 0)
            {
                XDocument docConfig = XDocument.Load(@"..\Config.xml");
                var xmlOrders = docConfig.Descendants("id");
                int oid = Convert.ToInt32(xmlOrders.ToList()[0].Element("oid")?.Value);
                int pid = Convert.ToInt32(xmlOrders.ToList()[0].Element("pid")?.Value);
                xmlOrders.ToList().Find(item => Convert.ToInt32(item.Element("pid")?.Value) == pid)?.Remove();
                item.ID = ++pid;
                XElement rootC = new XElement("id");
                rootC.Add(new XElement("oid", oid));
                rootC.Add(new XElement("pid", pid));
                xmlOrders.ToList().Add(rootC);
                docConfig.Element("ids")?.Add(rootC);
                docConfig.Save(@"..\Config.xml");
            }
            List<DO.Product> allProducts = GetAll().ToList();
            allProducts.Add(item);
                StreamWriter stWr = new StreamWriter("../xml/Product.xml");
                XmlSerializer seri = new XmlSerializer(typeof(List<DO.Product>));
                seri.Serialize(stWr, allProducts);
                stWr.Close();
          
            return item.ID;
        }
        public IEnumerable<DO.Product> GetAll(Func<DO.Product, bool>? func = null)
        {
            StreamReader read = new StreamReader("../xml/Product.xml");
            XmlSerializer seri = new XmlSerializer(typeof(List<DO.Product>));
            var allProducts =  (List<DO.Product>)seri.Deserialize(read);
            read.Close();
            //return w
            if(func != null)
                return (IEnumerable<DO.Product>)allProducts.Where(func);
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
            DO.Product product=Read(id);
            productList.Remove(product);
            StreamWriter writer = new StreamWriter("../xml/Product.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>));
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
