using System.ComponentModel;

namespace DesktopApp.ViewModel
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private string _todaySales = "₹ 0";
        private string _pendingInvoices = "0 Invoices";
        private string _lowStockItems = "0 Items";

        public string TodaySales
        {
            get => _todaySales;
            set { _todaySales = value; OnPropertyChanged(nameof(TodaySales)); }
        }

        public string PendingInvoices
        {
            get => _pendingInvoices;
            set { _pendingInvoices = value; OnPropertyChanged(nameof(PendingInvoices)); }
        }

        public string LowStockItems
        {
            get => _lowStockItems;
            set { _lowStockItems = value; OnPropertyChanged(nameof(LowStockItems)); }
        }

        public DashboardViewModel()
        {
            // Placeholder for future initialization
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            // You can manually assign data here or inject from services
            TodaySales = "₹ 0";
            PendingInvoices = "0 Invoices";
            LowStockItems = "0 Items";
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
