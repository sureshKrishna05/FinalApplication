using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DesktopApp.ViewModel
{
    public class BillingViewModel : INotifyPropertyChanged
    {
        private string _partyName = string.Empty;
        private string _invoiceNo = string.Empty;
        private DateTime? _invoiceDate;

        public string PartyName
        {
            get => _partyName;
            set
            {
                if (_partyName != value)
                {
                    _partyName = value;
                    OnPropertyChanged(nameof(PartyName));
                }
            }
        }

        public string InvoiceNo
        {
            get => _invoiceNo;
            set
            {
                if (_invoiceNo != value)
                {
                    _invoiceNo = value;
                    OnPropertyChanged(nameof(InvoiceNo));
                }
            }
        }

        public DateTime? InvoiceDate
        {
            get => _invoiceDate;
            set
            {
                if (_invoiceDate != value)
                {
                    _invoiceDate = value;
                    OnPropertyChanged(nameof(InvoiceDate));
                }
            }
        }

        public ICommand SearchCommand { get; }

        public BillingViewModel()
        {
            SearchCommand = new RelayCommand(ExecuteSearch);
        }

        private void ExecuteSearch(object? parameter)
        {
            // Replace with actual search logic
            System.Diagnostics.Debug.WriteLine($"Searching for: PartyName={PartyName}, InvoiceNo={InvoiceNo}, InvoiceDate={InvoiceDate}");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
