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
        public OrderListWindow(IBl bl)
        {
            InitializeComponent();
            tempBl = bl;
            //AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            //AttributeSelector.Items.Add("All products");
            //ProductsListview.ItemsSource = bl.Product.GetProductList();
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
