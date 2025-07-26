using DesktopApp.Data;
using DesktopApp.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace DesktopApp.ViewModel
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private string _todaySales = "₹ 0.00";
        public string TodaySales
        {
            get => _todaySales;
            set { _todaySales = value; OnPropertyChanged(nameof(TodaySales)); }
        }

        private string _pendingInvoices = "0";
        public string PendingInvoices
        {
            get => _pendingInvoices;
            set { _pendingInvoices = value; OnPropertyChanged(nameof(PendingInvoices)); }
        }

        private string _lowStockItems = "0";
        public string LowStockItems
        {
            get => _lowStockItems;
            set { _lowStockItems = value; OnPropertyChanged(nameof(LowStockItems)); }
        }

        // Collection to hold the most recently added products
        public ObservableCollection<ProductInfo> RecentProducts { get; set; }

        public DashboardViewModel()
        {
            RecentProducts = new ObservableCollection<ProductInfo>();
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // --- IMPLEMENTATION ---

                    // 1. Calculate Low Stock Items
                    // Counts products where stock is less than a threshold (e.g., 10)
                    int lowStockCount = db.Products.Count(p => p.StockQuantity < 10);
                    LowStockItems = $"{lowStockCount}";

                    // 2. Load Recently Added Products
                    // Gets the 5 most recent products added to the database
                    var recent = db.Products.OrderByDescending(p => p.Id).Take(5).ToList();
                    RecentProducts.Clear();
                    foreach (var product in recent)
                    {
                        RecentProducts.Add(product);
                    }

                    // Placeholders for features that require an Invoices table
                    TodaySales = "₹ 0.00"; // This will be implemented later
                    PendingInvoices = "0"; // This will be implemented later
                }
            }
            catch (System.Exception ex)
            {
                // Handle potential database errors gracefully
                LowStockItems = "Error";
                System.Diagnostics.Debug.WriteLine($"Error loading dashboard data: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}