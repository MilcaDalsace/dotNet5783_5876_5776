using BLApi;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        private IBl tempBl;
        string orderStatus;
        public OrderListWindow(IBl bl,string status)
        {
            InitializeComponent();
            orderStatus = status;
            tempBl = bl;
            OrderListview.ItemsSource = tempBl.Order.GetListOrder();
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void OrderListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderForList order = (BO.OrderForList)(sender as ListView).SelectedItem;
            new OrderWindow(tempBl,order.ID).Show();
        }
    }
}
