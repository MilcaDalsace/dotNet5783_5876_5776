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
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private IBl tempBl;
        int tempId;
        private BO.Order? order;
        public OrderWindow(IBl bl,int id)
        {
            InitializeComponent();
            tempBl = bl;
            tempId = id;
            order = tempBl.Order.GetOrderDetails(id);
            //תנאי שגוי לשנות
            if (order!=null)
            {
                ordCustomerNameTxtB.Text = order.CustomerName.ToString();
                ordCustomerEmailTxtB.Text=order.CustomerEmail.ToString();
                ordCustomerAdderssLbl.Text = order.CustomerAdress.ToString();
                ordStatusTxtB.Text = order.OrderStatus.ToString();
                ordFinalPriceLblTxtB.Text = order.FinalPrice.ToString();
                ordDeliveryDateTxtB.Text = order.DeliveryDate.ToString();
                ordShipDateTxtB.Text = order.ShipDate.ToString();

                ordCustomerNameTxtB.IsEnabled=false;
                ordCustomerEmailTxtB.IsEnabled = false;
                ordCustomerAdderssLbl.IsEnabled = false;
                ordStatusTxtB.IsEnabled = false;
                ordFinalPriceLblTxtB.IsEnabled = false;
                ordDeliveryDateTxtB.IsEnabled = false;
                ordShipDateTxtB.IsEnabled = false;
            }
        }

    }
}
