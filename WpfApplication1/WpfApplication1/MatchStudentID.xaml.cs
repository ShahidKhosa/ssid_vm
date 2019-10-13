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

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for MatchStudentID.xaml
    /// </summary>
    public partial class MatchStudentID : Window
    {        
        internal StudentPersonalInfo Student { get; set; }

        internal SearchStudent searchStudent { get; set; }

        public MatchStudentID()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {            
            this.Close();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (txtStudentID.Text != String.Empty && txtStudentID.Text.Equals(Student.StudentID))
            {
                if (searchStudent != null)
                {
                    searchStudent.SetStudentData(Student);

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Student ID not matched, please enter correct Student ID");
                ResetData();
            }
        }


        private void ResetData()
        {
            txtStudentID.Text = "";
            txtStudentID.Focus();
        }


        private void txtStudentID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSubmit_Click(sender, e);
            }
        }
    }
}
