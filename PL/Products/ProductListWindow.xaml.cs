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
using BL;
//using BlImplementation;
using BO;
using PL;
using PL.Cart;
using BlImplementation;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window,INotifyPropertyChanged
    {
        int debily=0;
        private IBl tempBl=BLApi.Factory.Get();
        string userStatus;
        public event PropertyChangedEventHandler PropertyChanged;
        public ProductListWindow(string status)
        {
            InitializeComponent();
            ////ataContext=this;
           DataContext= tempBl.Product.GetProductList();
            if (status != "admin")
            {
                AddProductBtn.Visibility = Visibility.Hidden;
                ShowCartBtn.Visibility = Visibility.Visible;
            }
            else
            {
                AddProductBtn.Visibility = Visibility.Visible;
                ShowCartBtn.Visibility = Visibility.Hidden;
            }
               
            userStatus = status;
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Categories));
            ProductsListview.ItemsSource = tempBl.Product.GetProductList();
            debily = ProductsListview.Items.Count;
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool GetBy(DO.Product p) => p.Category == (DO.Categories)AttributeSelector.SelectedItem;
            ProductsListview.ItemsSource = tempBl.Product.GetProductList(GetBy);
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow(tempBl,0, userStatus).ShowDialog();
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
            //BO.ProductForList product = (BO.ProductForList)(sender as ListView).SelectedItem;
            new ProductWindow(tempBl, product.ID, userStatus).ShowDialog();
            ProductsListview.ItemsSource = tempBl.Product.GetProductList();

        }

        private void ShowCartBtn_Click(object sender, RoutedEventArgs e)
        {
            new CartListWindow().Show();
        }
    }
}
