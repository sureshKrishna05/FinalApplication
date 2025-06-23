using DesktopApp.ViewModel;
using System.Windows.Controls;

namespace DesktopApp.Views
{
    public partial class DataEntryPage : UserControl
    {
        public DataEntryPage()
        {
            InitializeComponent();
            // ViewModel handles everything now
            this.DataContext = new DataEntryViewModel();
        }
    }
}
