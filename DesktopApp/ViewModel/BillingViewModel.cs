using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModel
{
    /// <summary>
    /// Represents a single row/item in the invoice ListView.
    /// </summary>
    public class BillingItem : INotifyPropertyChanged
    {
        public string ProductName { get; set; } = string.Empty;
        public string BatchNo { get; set; } = string.Empty;
        public string Hsn { get; set; } = string.Empty;
        public string Pac { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public string Fr { get; set; } = string.Empty;
        public string Exp { get; set; } = string.Empty;
        public decimal Mrp { get; set; }
        public decimal Ptr { get; set; }
        public decimal Value { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }

    /// <summary>
    /// The main ViewModel that manages the entire Billing page.
    /// </summary>
    public class BillingViewModel : INotifyPropertyChanged
    {
        #region Collections for UI
        public ObservableCollection<BillingItem> InvoiceItems { get; set; }
        public ObservableCollection<string> CompanyNames { get; set; }
        public ObservableCollection<string> MedicineList { get; set; }
        #endregion

        #region Properties for Data Binding
        // FIX: Initialize string fields to avoid nullable warnings.
        private string _selectedCompany = string.Empty;
        public string SelectedCompany
        {
            get => _selectedCompany;
            set { _selectedCompany = value; OnPropertyChanged(nameof(SelectedCompany)); }
        }

        // FIX: Initialize string fields to avoid nullable warnings.
        private string _newProduct = string.Empty;
        public string NewProduct
        {
            get => _newProduct;
            set { _newProduct = value; OnPropertyChanged(nameof(NewProduct)); }
        }

        private int _newQty = 1;
        public int NewQty
        {
            get => _newQty;
            set { _newQty = value; OnPropertyChanged(nameof(NewQty)); }
        }
        #endregion

        #region Properties for Summary Calculations
        private decimal _subTotal;
        public decimal SubTotal { get => _subTotal; set { _subTotal = value; OnPropertyChanged(nameof(SubTotal)); } }
        private decimal _sgst;
        public decimal Sgst { get => _sgst; set { _sgst = value; OnPropertyChanged(nameof(Sgst)); } }
        private decimal _cgst;
        public decimal Cgst { get => _cgst; set { _cgst = value; OnPropertyChanged(nameof(Cgst)); } }
        private decimal _roundOff;
        public decimal RoundOff { get => _roundOff; set { _roundOff = value; OnPropertyChanged(nameof(RoundOff)); } }
        private decimal _grandTotal;
        public decimal GrandTotal { get => _grandTotal; set { _grandTotal = value; OnPropertyChanged(nameof(GrandTotal)); } }
        #endregion

        #region Commands
        public ICommand AddToInvoiceCommand { get; }
        public ICommand ClearInvoiceCommand { get; }
        public ICommand PrintInvoiceCommand { get; }
        #endregion

        public BillingViewModel()
        {
            InvoiceItems = new ObservableCollection<BillingItem>();
            CompanyNames = new ObservableCollection<string>();
            MedicineList = new ObservableCollection<string>();
            InvoiceItems.CollectionChanged += (s, e) => UpdateInvoiceSummary();

            // FIX: Adapt parameterless methods to the RelayCommand constructor using a lambda expression.
            AddToInvoiceCommand = new RelayCommand(_ => AddToInvoice(), _ => CanAddToInvoice());
            ClearInvoiceCommand = new RelayCommand(_ => ClearInvoice());
            PrintInvoiceCommand = new RelayCommand(_ => PrintInvoice());

            LoadInitialData();
        }

        private void LoadInitialData()
        {
            CompanyNames.Add("BALAJI OPTICALS");
            CompanyNames.Add("MedCare Hospital");
            MedicineList.Add("DEWMAT GEL");
            MedicineList.Add("DEWMAT");
            MedicineList.Add("MATMOX DM");
            MedicineList.Add("Paracetamol 500mg");
        }

        private void AddToInvoice()
        {
            var newItem = new BillingItem
            {
                ProductName = this.NewProduct,
                Quantity = this.NewQty,
                BatchNo = "BATCH001",
                Hsn = "300490",
                Pac = "10ML",
                Fr = "7",
                Exp = "09/26",
                Mrp = 140.00m,
                Ptr = 108.00m,
                Value = this.NewQty * 108.00m
            };

            InvoiceItems.Add(newItem);

            // FIX: Set to string.Empty instead of null.
            NewProduct = string.Empty;
            NewQty = 1;
        }

        private bool CanAddToInvoice()
        {
            return !string.IsNullOrEmpty(NewProduct) && NewQty > 0;
        }

        private void UpdateInvoiceSummary()
        {
            SubTotal = InvoiceItems.Sum(item => item.Value);
            Sgst = SubTotal * 0.06m;
            Cgst = SubTotal * 0.06m;
            decimal totalBeforeRoundOff = SubTotal + Sgst + Cgst;
            GrandTotal = Math.Round(totalBeforeRoundOff, MidpointRounding.AwayFromZero);
            RoundOff = GrandTotal - totalBeforeRoundOff;
        }

        private void ClearInvoice()
        {
            InvoiceItems.Clear();
            // FIX: Set to string.Empty instead of null.
            SelectedCompany = string.Empty;
        }

        private void PrintInvoice()
        {
            MessageBox.Show("Printing logic will be implemented here.", "Print Invoice");
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion
    }
}