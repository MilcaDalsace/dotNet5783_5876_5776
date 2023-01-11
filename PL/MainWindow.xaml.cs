﻿using BLApi;
using BO;
using DalApi;
using PL.Order;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        IBl bl =  BLApi.Factory.Get();
        IBl tempBl;

        private void BtnOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            //new OrderListWindow(bl, "orderTracking").ShowDialog();
            OrderTrackingSP.Visibility = Visibility.Visible;
            OrderTrackingSP.IsEnabled = true;
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminOrdersBtn.Visibility = Visibility.Visible;
            AdminProdBtn.Visibility = Visibility.Visible;
            AdminOrdersBtn.IsEnabled = true;
            AdminProdBtn.IsEnabled = true;
            SubGrid.IsEnabled = false;
            SubGrid.Visibility = Visibility.Hidden;

        }
        private void AdminOrdersBtn_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow(bl, "admin").ShowDialog();
            AdminOrdersBtn.Visibility = Visibility.Hidden;
            AdminProdBtn.Visibility = Visibility.Hidden;
            AdminOrdersBtn.IsEnabled = false;
            AdminProdBtn.IsEnabled = false;
            SubGrid.IsEnabled = true;
            SubGrid.Visibility = Visibility.Visible;

        }

        private void AdminProdBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow("admin").ShowDialog();
            AdminOrdersBtn.Visibility = Visibility.Hidden;
            AdminProdBtn.Visibility = Visibility.Hidden;
            AdminOrdersBtn.IsEnabled = false;
            AdminProdBtn.IsEnabled = false;
            SubGrid.IsEnabled = true;
            SubGrid.Visibility = Visibility.Visible;
        }

        private void CheckOrderIdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tempOrderId = Convert.ToString(OrderTrackingTB.Text);
                int.TryParse(tempOrderId, out int OrderId);
                tempBl = new BlImplementation.Bl();
                BO.OrderTracking tempOrderTracking = new OrderTracking();
                tempOrderTracking = tempBl.Order.GetOrderTracking(OrderId);
                OrderTrackingTB.Text = null;
                OrderTrackingSP.IsEnabled = false;
                OrderTrackingSP.Visibility = Visibility.Hidden;
                new OrderTrackingWindow(tempOrderTracking).ShowDialog();

            }
            catch (BO.NoSuchObjectExcption ex)
            {
                OrderTrackingSP.IsEnabled = false;
                OrderTrackingSP.Visibility = Visibility.Hidden;
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnNewOrder_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow("user").ShowDialog();
        }
    }
}
