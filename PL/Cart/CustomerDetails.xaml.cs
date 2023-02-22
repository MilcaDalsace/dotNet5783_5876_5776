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
    /// Interaction logic for CustomerDetails.xaml
    /// </summary>
    public partial class CustomerDetails : Window
    {
        public BO.Cart cart { get; set; }
        public CustomerDetails()
        {
            InitializeComponent();
            cart = ICart.curCartP;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //new CartListWindow().Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ICart.curCartP.CustomerEmail=(sender).ToString();
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            ICart.curCartP.CustomerAdress = (sender).ToString();
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {
            ICart.curCartP.Name= (sender).ToString();
        }
    }
}
