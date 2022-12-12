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
using BL;
using BlImplementation;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        //blapi.ibl bl = new BlImplementation.Bl;
        private IBl tempBl;
        public ProductListWindow(IBl bl)
        {
            InitializeComponent();
            tempBl = bl;
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            //AttributeSelector.Items.Add("All product");
            ProductsListview.ItemsSource = bl.Product.GetProductList();
            //ProductsListview.ItemsSource.ItemCount = bl.Product.GetProductList().Count;
        }
        //static public bool GetBy(ProductForList p,BO.Categories curCat) =>p.Category==curCat;

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ProductsListview.ItemsSource = tempBl.Product.GetProductList((item=>item.Category == (DO.Categories)AttributeSelector.SelectedItem));
            bool GetBy(DO.Product p) => p.Category == (DO.Categories)AttributeSelector.SelectedItem;
            ProductsListview.ItemsSource = tempBl.Product.GetProductList(GetBy);
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(tempBl,0).Show();
        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList product = (BO.ProductForList)(sender as ListView).SelectedItem;
            new ProductWindow(tempBl, product.ID).Show(); 
        }
    }
}
