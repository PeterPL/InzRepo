using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MainApp.Resources;

namespace MainApp
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
                NavigationService.Navigate(new Uri("/Analysis.xaml", UriKind.Relative));
        }
    }
}