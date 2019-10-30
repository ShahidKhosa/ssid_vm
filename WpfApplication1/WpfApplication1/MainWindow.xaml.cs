using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using log4net;
using FluentScheduler;

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /*
     * Options Available
     * log.info
     * log.error
     * log.fatal
     * log.debug
    */
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();

            try
            {
                // Start the scheduler
                JobManager.Initialize(new ScheduledJobRegistry());
                _NavigationFrame.Navigate(new HomePage());
            }
            catch(Exception ex)
            {
                JobManager.Stop();
                Helper.log.Error(ex.Message);
            }            
        }
    }


    public class ScheduledJobRegistry : Registry
    {
        public ScheduledJobRegistry()
        {
            Schedule<KioskCronJob>()
                    .NonReentrant() // Only one instance of the job can run at a time
                    .ToRunOnceAt(DateTime.Now.AddMinutes(5))    // Delay startup for a while
                    .AndEvery(10).Minutes();     // Interval

            Schedule<StudentsCronJob>()
                    .NonReentrant() // Only one instance of the job can run at a time
                    .ToRunOnceAt(DateTime.Now.AddMinutes(30))    // Delay startup for a while
                    .AndEvery(30).Minutes();     // Interval

            Schedule<SendLogFile>()
                    .NonReentrant() // Only one instance of the job can run at a time
                    .ToRunOnceAt(DateTime.Now.AddMinutes(1))    // Delay startup for a while
                    .AndEvery(1).Hours();     // Interval

            // TODO... Add more schedules here
        }
    }


    public class KioskCronJob : IJob
    {
        public void Execute()
        {
            // Execute your scheduled task here
            APIManager.GetScheduleKioskSettings();            
        }
    }


    public class StudentsCronJob : IJob
    {
        public void Execute()
        {
           APIManager.GetAllStudents("get_all_students");
        }
    }


    public class SendLogFile : IJob
    {        
        public void Execute()
        {            
            APIManager.SendLogData();
        }
    }
}
