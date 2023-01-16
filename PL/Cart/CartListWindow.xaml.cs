using BLApi;
using BO;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartListWindow.xaml
    /// </summary>
    public partial class CartListWindow : Window
    {
        private IBl tempBl = BLApi.Factory.Get();
        public List<BO.OrderItem>? cart { get; set; }
        public CartListWindow()
        {
            InitializeComponent();
            cart=ICart.curCartP.ItemOrderList;
            DataContext =cart;
            //CartLstview.ItemsSource = 
        }

        private void submitOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            ICart.curCartP.Name = "orit";
            ICart.curCartP.CustomerAdress = "orittt@gmail.com";
            ICart.curCartP.CustomerEmail = "orittt@gmail.com";
            tempBl.Cart.SaveCart(ICart.curCartP);
        }

        private void CartLstview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.OrderItem product = (BO.OrderItem)((ListView)sender).SelectedItem;
            new CartWindow(product).ShowDialog();
            CartLstview.ItemsSource = ICart.curCartP.ItemOrderList;

        }
    }
}
