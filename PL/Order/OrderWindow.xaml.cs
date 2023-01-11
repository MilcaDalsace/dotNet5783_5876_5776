using BLApi;
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

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private IBl tempBl;
        int tempId;
        private BO.Order? order;
        public string statusProp { get; set; }
        public OrderWindow(IBl bl, int id, string status)
        {
            InitializeComponent();
            tempBl = bl;
            tempId = id;
            statusProp = status;
            order = tempBl.Order.GetOrderDetails(id);
            DataContext = new { order = order, status = statusProp };
            //תנאי שגוי לשנות
            if (order != null)
            {
                //?
                //ordCustomerNameTxtB.Text = order.CustomerName?.ToString();
                //ordCustomerEmailTxtB.Text=order.CustomerEmail?.ToString();
                //ordCustomerAdderssLbl.Text = order.CustomerAdress?.ToString();
                //ordStatusTxtB.Text = order.OrderStatus.ToString();
                //ordFinalPriceLblTxtB.Text = order.FinalPrice.ToString();
                //ordDeliveryDateTxtB.Text = order.DeliveryDate.ToString();
                //ordShipDateTxtB.Text = order.ShipDate.ToString();
                // orderItemListLV.ItemsSource=order.OrderItemList;
                //ordCustomerNameTxtB.IsEnabled=false;
                //ordCustomerEmailTxtB.IsEnabled = false;
                //ordCustomerAdderssLbl.IsEnabled = false;
                //ordStatusTxtB.IsEnabled = false;
                //ordFinalPriceLblTxtB.IsEnabled = false;
                //ordDeliveryDateTxtB.IsEnabled = false;
                //ordShipDateTxtB.IsEnabled = false;
                //if (status != "admin")
                //{
                //    ordUpdateDeliveryDateBtn.Visibility = Visibility.Hidden;
                //    ordUpdateShipDateBtn.Visibility= Visibility.Hidden;
                //}
                //else
                //{
                //    ordUpdateDeliveryDateBtn.Visibility = Visibility.Visible;
                //    ordUpdateShipDateBtn.Visibility = Visibility.Visible;
                //}
                //if (status == "orderTracking")
                //{

                //}
                //saveChangesBtn.Visibility=Visibility.Visible;
            }
        }

        private void ordUpdateDeliveryDateBtn_Click(object sender, RoutedEventArgs e)
        {
            //האם SENT וSUPPLY נמצאים בפונקציות הנכונות?
            //עושה בעיות בפונקציה הפנימית שנשלחה
            try
            { 
               BO.Order order= tempBl.Order.UpdateOrderSupply(tempId);
               ordDeliveryDateTxtB.Text = order.DeliveryDate.ToString();
            }
            catch (BO.OrderAlreadyUpdate)
            {
                MessageBox.Show("OrderAlreadyUpdate");
            }
            catch (BO.OrderDidnotsentAlready)
            {
                MessageBox.Show("OrderDidnotsentAlready");
            };

        }

        private void ordUpdateShipDateBtn_Click(object sender, RoutedEventArgs e)
        {
            try { 
                BO.Order order = tempBl.Order.UpdateOrderSent(tempId);
                ordShipDateTxtB.Text = order.ShipDate.ToString();
              }
            catch (BO.OrderAlreadyUpdate)
            {
                MessageBox.Show("OrderAlreadyUpdate");
            }
            catch(BO.OrderDidnotsentAlready)
            {
                MessageBox.Show("OrderDidnotsentAlready");
            };
        }

       
    }
    public class NotBooleanToDeliVeryvisibilityConverter : IValueConverter
    {
        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {
            //bool boolValue = (bool)value;
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
}
