using BO;
using System;
using System.Collections;
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
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {
        public OrderTrackingWindow(BO.OrderTracking tempOrderTracking)
        {
            InitializeComponent();
            OrderIdTB.Text=tempOrderTracking.ID.ToString();
            OrderStatusTB.Text=tempOrderTracking.OrderStatus.ToString();
            List<(DateTime, string)?> tempDateAndStatus = tempOrderTracking.DateAndStatus;
            if (tempOrderTracking.OrderStatus.ToString()== "arrived")
            {
                OrderDateTB.Text = tempDateAndStatus[0].Value.Item1.ToString();
                OrderDateStatusTB.Text = tempDateAndStatus[0].Value.Item2.ToString();
                DeliveryDateTB.Text = tempDateAndStatus[1].Value.Item1.ToString();
                DeliveryDateStatusTB.Text = tempDateAndStatus[1].Value.Item2.ToString();
                ShipDateTB.Text = tempDateAndStatus[2].Value.Item1.ToString();
                ShipDateStatusTB.Text = tempDateAndStatus[2].Value.Item2.ToString();

                OrderDateTB.Visibility = Visibility.Visible;
                OrderDateStatusTB.Visibility = Visibility.Visible;
                OrderDateLbl.Visibility=Visibility.Visible; 
                DeliveryDateTB.Visibility = Visibility.Visible;
                DeliveryDateStatusTB.Visibility = Visibility.Visible;
                DeliveryDateLbl.Visibility = Visibility.Visible;
                ShipDateTB.Visibility = Visibility.Visible;
                ShipDateStatusTB.Visibility = Visibility.Visible;
                ShipDateLbl.Visibility= Visibility.Visible;
            }
            if (tempOrderTracking.OrderStatus.ToString() == "sent")
            {
                OrderDateTB.Text = tempDateAndStatus[0].Value.Item1.ToString();
                OrderDateStatusTB.Text = tempDateAndStatus[0].Value.Item2.ToString();
                DeliveryDateTB.Text = tempDateAndStatus[1].Value.Item1.ToString();
                DeliveryDateStatusTB.Text = tempDateAndStatus[1].Value.Item2.ToString();
                OrderDateTB.Visibility = Visibility.Visible;
                OrderDateLbl.Visibility = Visibility.Visible;
                DeliveryDateTB.Visibility = Visibility.Visible;
                DeliveryDateLbl.Visibility = Visibility.Visible;
            }
            if (tempOrderTracking.OrderStatus.ToString() == "received")
            {
                OrderDateTB.Text = tempDateAndStatus[0].Value.Item1.ToString();
                OrderDateStatusTB.Text= tempDateAndStatus[0].Value.Item2.ToString();
                OrderDateTB.Visibility = Visibility.Visible;
                OrderDateLbl.Visibility = Visibility.Visible;
            }

        }
    }
}
