using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for SearchStudent.xaml
    /// </summary>
    public partial class SearchStudent : Page
    {        

        public SearchStudent()
        {
            InitializeComponent();
        }


        private void btn_Home_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
            }
        }
        

        public static T GetChildOfType<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }


        private void PreviewTextInput_EnhanceComboSearch(object sender, TextCompositionEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;

            cmb.IsDropDownOpen = true;

            if (!string.IsNullOrEmpty(cmb.Text))
            {
                string fullText = cmb.Text.Insert(GetChildOfType<TextBox>(cmb).CaretIndex, e.Text);
                cmb.ItemsSource = StudentPersonalInfo.Students.Where(s => s.Name.IndexOf(fullText, StringComparison.InvariantCultureIgnoreCase) != -1).ToList();                
            }
            else if (!string.IsNullOrEmpty(e.Text))
            {
                cmb.ItemsSource = StudentPersonalInfo.Students.Where(s => s.Name.IndexOf(e.Text, StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
            }
            else
            {
                cmb.ItemsSource = StudentPersonalInfo.Students;
            }

            cmb.SelectedValuePath = "ID";
            cmb.DisplayMemberPath = "Name";
        }


        private void Pasting_EnhanceComboSearch(object sender, DataObjectPastingEventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;

            cmb.IsDropDownOpen = true;

            string pastedText = (string)e.DataObject.GetData(typeof(string));
            string fullText = cmb.Text.Insert(GetChildOfType<TextBox>(cmb).CaretIndex, pastedText);

            if (!string.IsNullOrEmpty(fullText))
            {
                cmb.ItemsSource = StudentPersonalInfo.Students.Where(s => s.Name.IndexOf(fullText, StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
            }
            else
            {
                cmb.ItemsSource = StudentPersonalInfo.Students;
            }

            cmb.SelectedValuePath = "ID";
            cmb.DisplayMemberPath = "Name";
        }


        private void PreviewKeyUp_EnhanceComboSearch(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                ComboBox cmb = (ComboBox)sender;

                cmb.IsDropDownOpen = true;

                if (!string.IsNullOrEmpty(cmb.Text))
                {
                    cmb.ItemsSource = StudentPersonalInfo.Students.Where(s => s.Name.IndexOf(cmb.Text, StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
                }
                else
                {
                    cmb.ItemsSource = StudentPersonalInfo.Students;
                }

                cmb.SelectedValuePath = "ID";
                cmb.DisplayMemberPath = "Name";
            }
        }


        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //StudentPersonalInfo result = Students.Exists(s => s.ID == ((StudentPersonalInfo)cmbStudent.SelectedItem).ID);
                StudentPersonalInfo student = StudentPersonalInfo.Students.Find(s => s.ID == ((StudentPersonalInfo)cmbStudent.SelectedItem).ID);

                if (student != null)
                {
                    MatchStudentID matchStudentID = new MatchStudentID()
                    {
                        Student = student,
                        searchStudent = this
                    };

                    matchStudentID.Show();                                       
                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           
        }


        internal void SetStudentData(StudentPersonalInfo student)
        {
            Student.ID = student.ID;
            Student.JobID = (APIManager.KioskSettings.ContainsKey("job_id") ? Int32.Parse(APIManager.KioskSettings["job_id"].ToString()) : 0);
            Student.StudentID = student.StudentID;
            Student.FirstName = student.FirstName;
            Student.LastName = student.LastName;
            Student.Grade = student.Grade;
            Student.LiveImage = student.Image;

            if (Student.ID > 0 && Student.LiveImage != string.Empty)
            {
                string imagePath = string.Format("/assets/membership/{0}/{1}", Student.JobID, Student.LiveImage);
                Student.ImagePath = Guid.NewGuid() + ".jpg";

                APIManager.DownloadFile(imagePath, Student.ImagePath, 0);

                //if (!File.Exists(Student.ImagePath))
                //{
                //    APIManager.DownloadFile(imagePath, Student.ImagePath, 0);
                //}
            }

            this.NavigationService.Navigate(new Uri("StudentSigninReason.xaml", UriKind.Relative));
        }


    }
}
