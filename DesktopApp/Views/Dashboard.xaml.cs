using DesktopApp.ViewModel;
using System.Windows.Controls;

namespace DesktopApp.Views
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent(); 
            DataContext = new DashboardViewModel(); 
        }
    }
}