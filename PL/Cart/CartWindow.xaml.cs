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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private IBl tempBl = BLApi.Factory.Get();
        BO.OrderItem curProduct;
        public CartWindow(BO.OrderItem product)
        {
            InitializeComponent();
            curProduct=product;
            nameTxt.Text=product.ProductName;
            priceTxt.Text = product.Price.ToString();
            finalPriceTxt.Text=product.FinalPrice.ToString();
            amountTxt.Text=product.Amount.ToString();
        }

        private void changeAmountBtn_Click(object sender, RoutedEventArgs e)
        {
            try {
            tempBl.Cart.UpdateAmount(ProductListWindow.curCartP, curProduct.ProductId, Convert.ToInt32(amountTxt.Text));
            Close(); 
            }
            catch (BO.OutOfStockExcption ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try {
            tempBl.Cart.UpdateAmount(ProductListWindow.curCartP, curProduct.ProductId, 0);
            Close();
            }
            catch (BO.OutOfStockExcption ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
