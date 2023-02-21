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
using PL;
using PL.Cart;
using BlImplementation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window,INotifyPropertyChanged
    {
        private IBl tempBl=BLApi.Factory.Get();
       // string userStatus;
        public IEnumerable<BO.ProductForList> products { get; set; }
        public string statusProp { get; set; }
        public Array categoriesForP { get; set; }
        //public IEnumerable<BO.ProductForList> productList { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ProductListWindow(string status)
        {
            InitializeComponent();
            products= tempBl.Product.GetProductList();
            statusProp = status;
            categoriesForP = Enum.GetValues(typeof(BO.Categories));
            if (statusProp == "user")
            {
            ProductsListview.ItemsSource = products;
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductsListview.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
            view.GroupDescriptions.Add(groupDescription);
            }
            DataContext = new {products=products,status=statusProp,categories= categoriesForP };


            //var arrXmlProducts = new XElement("Product",
            //                                             new XAttribute("ID", 1),
            //                                             new XAttribute("Name", "orit"),
            //                                             new XAttribute("Price", 2),
            //                                             new XAttribute("InStock", 2),
            //                                             new XAttribute("Catagory",2));
                    

            //XElement? xmlProduct = XDocument.Load(@"../xml/Product.xml").Root;
            //StreamWriter write = new StreamWriter("../xml/Product.xml");
            //XmlSerializer ser = new XmlSerializer(typeof(List<DO.Product>));
            //ser.Serialize(write, products);
            //write.Close();
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool GetBy(DO.Product p) => p.Category == (DO.Categories)AttributeSelector.SelectedItem;
            ProductsListview.ItemsSource = tempBl.Product.GetProductList(GetBy);
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(tempBl,0, statusProp).ShowDialog();
            OnPropertyChanged();
            //change 27/12
            //bool GetBy(DO.Product p) => p.Category == (DO.Categories)AttributeSelector.SelectedItem;
            //ProductsListview.ItemsSource = tempBl.Product.GetProductList();
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList product = (BO.ProductForList)((ListView)sender).SelectedItem;
            new ProductWindow(tempBl, product.ID, statusProp).ShowDialog();
            ProductsListview.ItemsSource = tempBl.Product.GetProductList();

        }

        private void ShowCartBtn_Click(object sender, RoutedEventArgs e)
        {
            new CartListWindow().Show();
        }

      
    }
    public class NotBooleanToVisibilityConverter1 : IValueConverter
    {
        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        { 
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
    public class NotBooleanToVisibilityShowCartBtnConverter : IValueConverter
    {
        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            if (value.ToString() != "admin")
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
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
