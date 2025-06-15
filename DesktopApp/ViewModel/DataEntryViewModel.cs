using System.ComponentModel;
using System.Windows.Input;

namespace DesktopApp.ViewModel
{
    public class DataEntryViewModel : INotifyPropertyChanged
    {
        private string _partyName = string.Empty;
        private string _productName = string.Empty;
        private int _quantity;
        private decimal _price;

        public string PartyName
        {
            get => _partyName;
            set { _partyName = value; OnPropertyChanged(nameof(PartyName)); }
        }

        public string ProductName
        {
            get => _productName;
            set { _productName = value; OnPropertyChanged(nameof(ProductName)); }
        }

        public int Quantity
        {
            get => _quantity;
            set { _quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }

        public decimal Price
        {
            get => _price;
            set { _price = value; OnPropertyChanged(nameof(Price)); }
        }

        public ICommand SaveCommand { get; }

        public DataEntryViewModel()
        {
            SaveCommand = new RelayCommand(SaveEntry, CanSave);
        }

        private void SaveEntry(object? obj)
        {
            // Replace this with actual DB save logic
            System.Diagnostics.Debug.WriteLine($"Saved: {PartyName}, {ProductName}, Qty: {Quantity}, ₹{Price}");
        }

        private bool CanSave(object? obj)
        {
            return !string.IsNullOrWhiteSpace(PartyName)
                && !string.IsNullOrWhiteSpace(ProductName)
                && Quantity > 0
                && Price > 0;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
