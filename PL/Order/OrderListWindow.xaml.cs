﻿using BLApi;
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
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        private IBl tempBl;
        string orderStatus;
        public OrderListWindow(IBl bl,string status)
        {
            InitializeComponent();
            orderStatus = status;
            tempBl = bl;
            List<BO.OrderForList> tempOrderForList = (List<BO.OrderForList>)bl.Order.GetListOrder();
            List<BO.OrderTracking> tempOrderTracking = new List<BO.OrderTracking>();
            foreach (BO.OrderForList orderForList in tempOrderForList)
            {
                tempOrderTracking.Add(bl.Order.GetOrderTracking(orderForList.ID));
            }
            if (orderStatus == "orderTracking")
                OrderListview.ItemsSource = tempOrderTracking;
            else
                OrderListview.ItemsSource =tempOrderForList;
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void OrderListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        { 
            BO.OrderTracking order;
            BO.OrderForList order1;
            if (orderStatus== "orderTracking")
            {
                order = (BO.OrderTracking)((ListView)sender).SelectedItem;
                //order = (BO.OrderTracking)(sender as ListView).SelectedItem;
                new OrderWindow(tempBl,order.ID,orderStatus).Show();
            }
            else
            {
                 order1 = (BO.OrderForList)((ListView)sender).SelectedItem;
                 //order1 = (BO.OrderForList)(sender as ListView).SelectedItem;
                new OrderWindow(tempBl, order1.ID, orderStatus).Show();
            }
               
           
        }
    }
}
