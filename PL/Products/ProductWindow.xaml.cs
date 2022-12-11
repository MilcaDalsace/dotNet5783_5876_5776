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
        public ProductWindow(IBl bl,int? id)
        {
            InitializeComponent();
            tempBl = bl;
            proCategoryCB.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            tempProduct = new BO.Product();
            tempId = id;
        }


        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            tempProduct.Name = Convert.ToString(proNameTxtB.GetValue);
            float.TryParse(Convert.ToString(proPriceTxtB.GetValue), out float result);
            tempProduct.Price = (float)(DO.Categories)result;
            tempProduct.Category = (BO.Categories)Convert.ToInt32(proCategoryCB.SelectedIndex);
            tempProduct.InStock = Convert.ToInt32(proAmountTxtB.GetValue);
            //לזרוק שגיאה אם לא קיבל ערך
            if (tempId==null)
            {
                tempBl.Product.AddProduct(tempProduct);
            }
            else
            {
                tempBl.Product.UpdateProduct(tempProduct);
            }
        }

       
    }
}
