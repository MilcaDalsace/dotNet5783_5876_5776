using BLApi;
using BlImplementation;
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
using System.Windows.Shapes;

namespace PL.Admin
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private IBl tempBl;
        public AdminWindow(IBl bl)
        {
            InitializeComponent();
            tempBl = bl;    
        }

        private void AdminOrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow(tempBl,"admin").Show();
        }

        private void AdminProdBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().Show();
        }
    }
}
