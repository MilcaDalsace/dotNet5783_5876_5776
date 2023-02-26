using BLApi;
using BO;
using DalApi;
using DO;
using PL.Order;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // DataSourceXml ds = new DataSourceXml();
            //ds.Show();
            //ds.Close();


            //List<int> config = new List<int>() {};
            //int i = 1;
            //config.Add(i);
            //StreamWriter write1 = new StreamWriter("../xml/Config.xml");
            //XmlSerializer ser1 = new XmlSerializer(typeof(List<int>));
            //ser1.Serialize(write1, config);
            //write1.Close();

            //List<DO.Product> products = new List<DO.Product>();
            //products.Add(new DO.Product()
            //{
            //    Category = (DO.Categories)1,
            //    ID = 1,
            //    InStock = 1,
            //    Name = "w",
            //    Price = 1
            //});

            //StreamWriter write = new StreamWriter("../xml/Product.xml");
            //XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>));
            //ser.Serialize(write, products);
            //write.Close();
        }

        IBl bl =  BLApi.Factory.Get();
        IBl tempBl;

        private void BtnOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            //new OrderListWindow(bl, "orderTracking").ShowDialog();
            OrderTrackingSP.Visibility = Visibility.Visible;
            OrderTrackingSP.IsEnabled = true;
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminOrdersBtn.Visibility = Visibility.Visible;
            AdminProdBtn.Visibility = Visibility.Visible;
            AdminOrdersBtn.IsEnabled = true;
            AdminProdBtn.IsEnabled = true;
            SubGrid.IsEnabled = false;
            SubGrid.Visibility = Visibility.Hidden;

        }
        private void AdminOrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow(bl, "admin").ShowDialog();
            AdminOrdersBtn.Visibility = Visibility.Hidden;
            AdminProdBtn.Visibility = Visibility.Hidden;
            AdminOrdersBtn.IsEnabled = false;
            AdminProdBtn.IsEnabled = false;
            SubGrid.IsEnabled = true;
            SubGrid.Visibility = Visibility.Visible;

        }

        private void AdminProdBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow("admin").ShowDialog();
            AdminOrdersBtn.Visibility = Visibility.Hidden;
            AdminProdBtn.Visibility = Visibility.Hidden;
            AdminOrdersBtn.IsEnabled = false;
            AdminProdBtn.IsEnabled = false;
            SubGrid.IsEnabled = true;
            SubGrid.Visibility = Visibility.Visible;
        }

        private void CheckOrderIdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tempOrderId = Convert.ToString(OrderTrackingTB.Text);
                int.TryParse(tempOrderId, out int OrderId);
                tempBl = new BlImplementation.Bl();
                BO.OrderTracking tempOrderTracking = new OrderTracking();
                tempOrderTracking = tempBl.Order.GetOrderTracking(OrderId);
                OrderTrackingTB.Text = null;
                OrderTrackingSP.IsEnabled = false;
                OrderTrackingSP.Visibility = Visibility.Hidden;
                new OrderTrackingWindow(tempOrderTracking).ShowDialog();

            }
            catch (BO.NoSuchObjectExcption ex)
            {
                OrderTrackingSP.IsEnabled = false;
                OrderTrackingSP.Visibility = Visibility.Hidden;
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow("user").ShowDialog();
        }

        private void btnSimulator_Click(object sender, RoutedEventArgs e)
        {
            new SimulatorWindow().ShowDialog();
        }
    }
}
