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
using BL;
using BLApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for Product.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl tempBl;
        private BO.Product tempProduct;
        int? tempId;
        int debily = 0;
     BO.Cart userCartToUpdate = new BO.Cart();
        public ProductWindow(IBl bl,int id,string status)
        {
            InitializeComponent();
            
            tempBl = bl;
            proCategoryCB.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            tempProduct = new BO.Product();
            tempId = id;
            if (id != 0)
            {
                tempProduct= tempBl.Product.GetProductDetails(id); ;
                proNameTxtB.Text= tempProduct.Name;
                proAmountTxtB.Text = tempProduct.InStock.ToString();
                proPriceTxtB.Text = tempProduct.Price.ToString();
                proCategoryCB.Text = tempProduct.Category.ToString();
            }
            if (status != "admin")
            {
                proNameTxtB.IsEnabled = false;
                proAmountTxtB.IsEnabled = false;
                proPriceTxtB.IsEnabled = false;
                proCategoryCB.IsEnabled = false;
                submitBtn.Visibility = Visibility.Hidden;
            }
            else
                AddProToCartBtn.Visibility= Visibility.Hidden;  
            }
        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            try { 
            if (tempId==0)
            {
                tempBl.Product.AddProduct(tempProduct);
                debily++;
                Close();
            }
            else
            { 
                tempBl.Product.UpdateProduct(tempProduct);
                Close();
            }}
            catch(BO.TheArrayIsFullException)
            {
                MessageBox.Show("cant add product-the array is full");
            }
            catch (BO.OneFieldsInCorrect)
            {
                MessageBox.Show("one fields incorrect");
            }
        }

        private void proNameTxtB_TextChanged(object sender, TextChangedEventArgs e)
        {
            tempProduct.Name=proNameTxtB.Text;
        }

        private void proPriceTxtB_TextChanged(object sender, TextChangedEventArgs e)
        {
            //tofloat how to do
            tempProduct.Price = Convert.ToInt32(proPriceTxtB.Text);
        }

        private void proAmountTxtB_TextChanged(object sender, TextChangedEventArgs e)
        {
            tempProduct.InStock =Convert.ToInt32(proAmountTxtB.Text);
        }

        private void proCategoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tempProduct.Category =(BO.Categories)( proCategoryCB.SelectedItem);
        }

        private void AddProToCartBtn_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderItem curPro = new BO.OrderItem
            {
                Amount = 1,
                FinalPrice = tempProduct.Price,
                Price = tempProduct.Price,
                ProductId = tempProduct.ID,
                ProductName = tempProduct.Name
            };
            try 
            { 
                tempBl.Cart.Add(ICart.curCartP, tempProduct.ID);
                MessageBox.Show("פריט נוסף בהצלחה");
                this.Close();
            }
            catch (BO.OutOfStockExcption ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            catch (BO.NoSuchObjectExcption ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            // ProductListWindow.curCartP.ItemOrderList = userCartToUpdate.ItemOrderList.Add(tempProduct);
        }
    }
}
