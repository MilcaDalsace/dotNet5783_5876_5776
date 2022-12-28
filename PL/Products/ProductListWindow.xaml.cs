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
//using BlImplementation;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        int debily=0;
        private IBl tempBl=BLApi.Factory.Get();
        string status1;
        public ProductListWindow(string status)
        {
            InitializeComponent();
            status1 = status;
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            ProductsListview.ItemsSource = tempBl.Product.GetProductList();
            debily = ProductsListview.Items.Count;
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
            new ProductWindow(tempBl,0,status1).ShowDialog();
            //change 27/12
            //bool GetBy(DO.Product p) => p.Category == (DO.Categories)AttributeSelector.SelectedItem;
            ProductsListview.ItemsSource = tempBl.Product.GetProductList();
        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList product = (BO.ProductForList)(sender as ListView).SelectedItem;
            new ProductWindow(tempBl, product.ID,status1).Show(); 
        }
    }
}
