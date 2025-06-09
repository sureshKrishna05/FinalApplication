using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DesktopApp.ViewModel
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public string TodaySales { get; set; } = "₹ 12,340";
        public string PendingInvoices { get; set; } = "4 Invoices";
        public string LowStockItems { get; set; } = "9 Items";

        public ObservableCollection<OrderItem> RecentOrders { get; set; }
        public ObservableCollection<StockItem> LowStockList { get; set; }

        public DashboardViewModel()
        {
            RecentOrders = new ObservableCollection<OrderItem>
            {
                new OrderItem { OrderId = "ORD001", Product = "Betap Capsule" },
                new OrderItem { OrderId = "ORD002", Product = "Paracetamol" },
                new OrderItem { OrderId = "ORD003", Product = "Domperid" },
            };

            LowStockList = new ObservableCollection<StockItem>
            {
                new StockItem { Product = "Domperid", Quantity = 5, Unit = "Strips" },
                new StockItem { Product = "Gel", Quantity = 2, Unit = "Tubes" },
            };
        }
    }

    public class OrderItem
    {
        public string OrderId { get; set; }
        public string Product { get; set; }
    }

    public class StockItem
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }
}