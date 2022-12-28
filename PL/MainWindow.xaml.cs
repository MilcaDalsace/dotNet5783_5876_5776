using BLApi;
using PL.Admin;
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
            new ProductListWindow().Show();
        }

        private void BtnOrder_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow(bl,"user").Show();
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            new AdminWindow(bl).Show();
        }
    }
}
