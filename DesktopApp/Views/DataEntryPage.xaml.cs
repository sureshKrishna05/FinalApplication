using DesktopApp.Data;
using DesktopApp.Model;
using DesktopApp.ViewModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for DataEntryPage.xaml
    /// </summary>
    public partial class DataEntryPage : UserControl
    {
        public DataEntryPage()
        {
            InitializeComponent();
            DataContext = new DataEntryViewModel();
           
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ClearAllTextBoxes(this);
        }

        private void ClearAllTextBoxes(DependencyObject parent)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is TextBox tb)
                {
                    tb.Text = string.Empty;
                }
                else
                {
                    ClearAllTextBoxes(child); // recursive
                }
            }
        }

        private void SaveToDatabase()
        {
            using var db = new AppDbContext();
            var company = new CompanyInfo
            {
                CompanyName = Name_company.Text,
                ContactNumber = contact.Text,
                GSTIN = gstin.Text,
                PAN = pan.Text,
                CompanyType = type.Text

            };

            db.CompanyInfos.Add(company);
            db.SaveChanges();

            MessageBox.Show("Saved to database");
        }

    }
}
