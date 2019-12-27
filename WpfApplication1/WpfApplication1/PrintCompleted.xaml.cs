﻿using System;
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
using System.Windows.Threading;

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for PrintCompleted.xaml
    /// </summary>
    public partial class PrintCompleted : Page
    {
        DispatcherTimer timer;

        public PrintCompleted()
        {
            InitializeComponent();

            Helper.UpdateLogoVisibility(footerBar);

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
        }

        void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            Visitor.ResetData();
            Student.ResetData();

            this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));            
        }
    }
}
