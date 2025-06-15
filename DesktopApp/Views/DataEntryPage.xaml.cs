using DesktopApp.ViewModel;
using System.Windows.Controls;

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
    }
}
