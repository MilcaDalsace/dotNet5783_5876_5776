using System;
using System.Collections.Generic;
using System.Globalization;
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
        private BO.Product tempProduct=new BO.Product();
        int? tempId;
        BO.Cart userCartToUpdate = new BO.Cart();
        public string StatusProp { get; set; }
        public Array categoryProp { get; set; }
        public ProductWindow(IBl bl,int id,string status)
        {
            InitializeComponent();
            StatusProp = status;
            tempBl = bl;
            categoryProp = Enum.GetValues(typeof(BO.Categories));
            tempProduct = new BO.Product();
            tempId = id;
            if (id != 0)
                tempProduct = tempBl.Product.GetProductDetails(id);
            DataContext = new { product= tempProduct, StatusProp = StatusProp,category= categoryProp };
        }
        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            try { 
            if (tempId==0)
            {
                tempBl.Product.AddProduct(tempProduct);
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

        //private void proNameTxtB_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    tempProduct.Name=proNameTxtB.Text;
        //}

        //private void proPriceTxtB_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    //tofloat how to do
        //    tempProduct.Price = Convert.ToInt32(proPriceTxtB.Text);
        //}

        //private void proAmountTxtB_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    tempProduct.InStock =Convert.ToInt32(proAmountTxtB.Text);
        //}

        //private void proCategoryCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    tempProduct.Category = (BO.Categories)(proCategoryCB.SelectedItem);
        //}

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
    public class NotBooleanToIsEnabledConverter : IValueConverter
        {
            public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
            {
                //bool boolValue = (bool)value;
                if (value.ToString()!= "admin")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            public object ConvertBack(
            object value,
            Type targetType,
            object parameter,

            CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    public class NotBooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            // bool boolValue = (bool)value;
            if (value.ToString() != "admin")
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        public object ConvertBack(
        object value,
        Type targetType,
        object parameter,

        CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //NotBooleanToAddProToCartBtnConverter
    public class NotBooleanToAddProToCartBtnConverter : IValueConverter
    {
        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            // bool boolValue = (bool)value;
            if (value.ToString() == "admin")
            {
                return Visibility.Hidden;
            }
            else
            {
                return Visibility.Visible;
            }
        }
        public object ConvertBack(
        object value,
        Type targetType,
        object parameter,

        CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
