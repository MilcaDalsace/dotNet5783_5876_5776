using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PL
{
    /// <summary>
    /// Interaction logic for DataSourceXml.xaml
    /// </summary>
    public partial class DataSourceXml : Window
    {

        public DataSourceXml()
        {
            List<DO.Product> products = new();
            List<DO.Order> orders = new();
            List<DO.OrderItem> orderItems = new();
            Tuple<DO.Categories, string, int>[] arrProducts = {
         new Tuple<DO.Categories,string, int >((DO.Categories)3,"Pavlova", 100),
         new Tuple<DO.Categories,string, int>((DO.Categories)2,"croissants", 70),
         new Tuple<DO.Categories,string, int>( (DO.Categories)1,"Macaroons", 150),
         new Tuple<DO.Categories,string, int>( (DO.Categories)1,"Eclair coffee", 200),
         new Tuple<DO.Categories,string, int>((DO.Categories)2,"Lemon Pie", 220),
         new Tuple<DO.Categories,string, int>( (DO.Categories)1,"Nougat pie", 250),
         new Tuple<DO.Categories,string, int>( (DO.Categories)2,"Ferrero Rocher", 300),
         new Tuple<DO.Categories,string, int>( (DO.Categories)3,"Malfey", 270),
         new Tuple<DO.Categories,string, int>((DO.Categories)1,"birthday cake", 500),
         new Tuple<DO.Categories,string, int>((DO.Categories)2,"puffs", 400)
        };
            //add 20 Customers
            Tuple<string, string, string>[] arrOrders = {
         new  Tuple<string, string, string>("David", "David@gmail.com", "David st."),
         new  Tuple<string, string, string>("Chaya", "Chaya@gmail.com", "David st."),
         new  Tuple<string, string, string>("Ayala", "Ayala@gmail.com", "David st."),
         new  Tuple<string, string, string>("Efrat", "Efrat@gmail.com", "David st."),
         new  Tuple<string, string, string>("Tamar", "Tamar@gmail.com", "David st."),
         new  Tuple<string, string, string>("Miri", "Miri@gmail.com", "David st."),
         new  Tuple<string, string, string>("Adas", "Adas@gmail.com", "David st."),
         new  Tuple<string, string, string>("Noa", "Noa@gmail.com", "David st."),
         new  Tuple<string, string, string>("Feigy", "Feigy@gmail.com", "David st."),
         new  Tuple<string, string, string>("Rachel", "Rachel@gmail.com", "David st."),
         new  Tuple<string, string, string>("Malcky", "Malcky@gmail.com", "David st."),
         new  Tuple<string, string, string>("Shira", "Shira@gmail.com", "David st."),
         new  Tuple<string, string, string>("Itamar", "Itamar@gmail.com", "David st."),
         new  Tuple<string, string, string>("Jonatan", "Jonatan@gmail.com", "DaDavid st.vid"),
         new  Tuple<string, string, string>("Ariel", "Ariel@gmail.com", "David st."),
         new  Tuple<string, string, string>("Michal", "Michal@gmail.com", "David st."),
         new  Tuple<string, string, string>("Binyamin", "Binyamin@gmail.com", "David st."),
         new  Tuple<string, string, string>("Ester", "Ester@gmail.com", "David st."),
         new  Tuple<string, string, string>("Chava", "Chava@gmail.com", "David st."),
         new  Tuple<string, string, string>("Sara", "Sara@gmail.com", "David st.")
        };
          Random _random = new();
            //Random r = new Random();
            for (int i = 0; i < 10; i++)                                  //add products by the randoaly numbers
            {
                int tempRand = _random.Next(0, 10);
                DO.Product p = new()
                {
                    ID = (int)_random.NextInt64(100000, 999999),
                    Name = arrProducts[tempRand].Item2,
                    Price =tempRand,
                    InStock = arrProducts[tempRand].Item3,
                    Category = arrProducts[tempRand].Item1
                };
                products.Add(p);
            }

            for (int i = 0; i < 30; i++)
            {
                DO.Order o = new()
                {
                    ID = 0,
                    CustomerName = arrOrders[i % 20].Item1,
                    CustomerEmail = arrOrders[i % 20].Item2,
                    CustomerAdress = arrOrders[i % 20].Item3,
                    OrderDate = DateTime.Now
                };
                TimeSpan t = new((int)_random.NextInt64(1, 3), 0, 0, 0);
                o.ShipDate = (i % 20) % 5 != 0 ? o.OrderDate.Add(t) : DateTime.MinValue;
                t = new TimeSpan((int)_random.NextInt64(3, 7), 0, 0, 0);
                o.DeliveryDate = ((i % 20) % 3 != 0 || (i % 20) % 4 != 0) & (i % 20) % 5 != 0 ? o.ShipDate.Add(t) : DateTime.MinValue;
                orders.Add(o);
            }

            for (int i = 0; i < 20; i++)
            {
                int tempRand = (int)_random.NextInt64(1, 10);
                DO.OrderItem oI = new()
                {
                    ID = 0,
                    ProductId = products?[tempRand].ID ?? throw new Exception(),
                    OrderId = orders?[i].ID ?? throw new Exception(),
                    Amount = tempRand,
                    Price = products[tempRand].Price
                };
                orderItems.Add(oI);
            }



            //var arrXmlProducts = from p in products
            //                     select new XElement("Product",
            //                                             new XAttribute("ID", p.ID),
            //                                             new XAttribute("Name", p.Name ?? throw new BO.NullException()),
            //                                             new XAttribute("Price", p.Price),
            //                                             new XAttribute("InStock", p.InStock),
            //                                             new XAttribute("Catagory", p.Category)
            //        );

            XElement? xmlProduct = XDocument.Load(@"../xml/Product.xml").Root;
            StreamWriter write = new StreamWriter("../xml/Product.xml");
            XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>));
            ser.Serialize(write, products);
            write.Close();
            InitializeComponent();
        }
    }
}
