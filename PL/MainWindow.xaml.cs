using BLApi;
using BO;
using PL.Order;
using System;
using System.Collections.Generic;
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
        }

        IBl bl =  BLApi.Factory.Get();
        private void BtnProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow("user").Show();
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow(bl,"user").Show();
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

        private void newOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow("user").Show();
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

        private void OrderTrackingBtn_Click(object sender, RoutedEventArgs e)
        {
            OrderTrackingSP.IsEnabled = true;
            OrderTrackingSP.Visibility = Visibility.Visible;
        }

        private void CheckOrderIdBtn_Click(object sender, RoutedEventArgs e)
        {
            //משהו מוזר במימוש!
            string a = OrderTrackingSP.ToString();
            int.TryParse(a, out int OrderId);
            OrderTracking orderTracking = new OrderTracking();
            //OrderTrackingLbl.GetValue;
        }
    }
}
