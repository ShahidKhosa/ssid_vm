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
using System.Windows.Shapes;

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public HomePage homePage = null;

        public Settings()
        {
            InitializeComponent();
            school_url.Text = AppSettings.JobNo;            
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            string old_school_url = AppSettings.JobNo;

            AppSettings.JobNo = school_url.Text.ToString();
            AppSettings.PrinterName = printersList.SelectedValue.ToString();
            AppSettings.SaveAppSettings();

            if (!old_school_url.Equals(school_url.Text) && homePage != null)
            {
                APIManager.KioskSettings = null;
                homePage.InitPage();
            }

            this.Close();
        }

        private void Restart()
        {
            // from System.Windows.Forms.dll
            System.Windows.Forms.Application.Restart();
            Application.Current.Shutdown();
        }
    }
}
