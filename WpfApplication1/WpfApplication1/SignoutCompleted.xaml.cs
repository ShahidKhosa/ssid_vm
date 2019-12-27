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
using System.Windows.Threading;

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for SignoutCompleted.xaml
    /// </summary>
    public partial class SignoutCompleted : Page
    {
        DispatcherTimer timer;

        public SignoutCompleted()
        {
            InitializeComponent();

            Helper.UpdateLogoVisibility(footerBar);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
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
            try
            {
                timer.Stop();
                Visitor.ResetData();
                Student.ResetData();

                this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
