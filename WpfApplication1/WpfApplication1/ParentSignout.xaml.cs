using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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


namespace SchoolSafeID
{
    /// <summary>
    /// Interaction logic for Scan_License.xaml
    /// </summary>
    public partial class ParentSignout : Page
    {
        ObservableCollection<StudentPersonalInfo> selectedStudentsList = null;

        public ParentSignout()
        {
            InitializeComponent();
            selectedStudentsList = new ObservableCollection<StudentPersonalInfo>();
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {            
            // Navigate back one page from this page, if there is an entry
            // in back navigation history
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
            else
            {
                this.NavigationService.Navigate(new Uri("HomePage.xaml", UriKind.Relative));
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(selectedStudentsList.Count > 0 && cmbSignoutReasons.SelectedValue.ToString() != string.Empty)
            {
                btnConfirm.Content = "Please wait...";
                btnBack.IsEnabled = false;
                btnConfirm.IsEnabled = false;

                APIManager.ParentStudentSignout(selectedStudentsList);

                this.NavigationService.Navigate(new Uri("SignoutCompleted.xaml", UriKind.Relative));
            }
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<SignoutReasons> signoutReasons = new List<SignoutReasons>();

            for(int i = 1; i < 10; i++)
            {
                string key = "signout_option" + i;

                if (APIManager.KioskSettings.ContainsKey(key) && APIManager.KioskSettings[key].ToString() != string.Empty)
                {
                    signoutReasons.Add(new SignoutReasons() { ID = i, Reason = APIManager.KioskSettings[key].ToString() });
                }
            }
            
            cmbSignoutReasons.ItemsSource = signoutReasons;
            cmbSignoutReasons.SelectedValuePath = "ID";
            cmbSignoutReasons.DisplayMemberPath = "Reason";

            txtIndividual.Text = Visitor.FirstName + " " + Visitor.LastName;
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

        private void cmbStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StudentPersonalInfo student = (StudentPersonalInfo)cmbStudent.SelectedItem;

            if (student != null && !selectedStudentsList.Contains(student))
            {                
                selectedStudentsList.Add(student);
            }

            if(selectedStudentsList.Count > 0)
            {
                selecteStudents.ItemsSource = selectedStudentsList;
                selecteStudents.DataContext = selectedStudentsList;
                cmbStudent.SelectedIndex = -1;

                lblSelectedStudents.Visibility = Visibility.Visible;
                selecteStudents.Visibility = Visibility.Visible;
            }

        }


        private void cmdDeleteStudent_Clicked(object sender, RoutedEventArgs e)
        {
            Button cmd = (Button)sender;

            if (cmd.DataContext is StudentPersonalInfo student)
            {
                if (student != null && selectedStudentsList.Contains(student))
                {
                    selectedStudentsList.Remove(student);

                    if (selectedStudentsList.Count < 1)
                    {
                        lblSelectedStudents.Visibility = Visibility.Collapsed;
                        selecteStudents.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }


    }

    public class SignoutReasons
    {
        public int ID { get; set; }
        public string Reason { get; set; }
    }
}
