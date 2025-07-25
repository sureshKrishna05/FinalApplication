using DesktopApp.Data;
using DesktopApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModel
{
    /// <summary>
    /// Represents a single row/item in the invoice ListView.
    /// </summary>
    public class BillingItem : INotifyPropertyChanged
    {
        // Backing fields for properties
        private string _productName = string.Empty;
        private string _batchNo = string.Empty;
        private string _hsn = string.Empty;
        private string _pac = string.Empty;
        private int _quantity;
        private string _fr = string.Empty;
        private string _exp = string.Empty;
        private decimal _mrp;
        private decimal _ptr;
        private decimal _value;

        // Public properties that the UI binds to
        public string ProductName { get => _productName; set { _productName = value; OnPropertyChanged(); } }
        public string BatchNo { get => _batchNo; set { _batchNo = value; OnPropertyChanged(); } }
        public string Hsn { get => _hsn; set { _hsn = value; OnPropertyChanged(); } }
        public string Pac { get => _pac; set { _pac = value; OnPropertyChanged(); } }
        public int Quantity { get => _quantity; set { _quantity = value; OnPropertyChanged(); } }
        public string Fr { get => _fr; set { _fr = value; OnPropertyChanged(); } }
        public string Exp { get => _exp; set { _exp = value; OnPropertyChanged(); } }
        public decimal Mrp { get => _mrp; set { _mrp = value; OnPropertyChanged(); } }
        public decimal Ptr { get => _ptr; set { _ptr = value; OnPropertyChanged(); } }
        public decimal Value { get => _value; set { _value = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// The main ViewModel that manages the entire Billing page.
    /// </summary>
    public class BillingViewModel : INotifyPropertyChanged
    {
        #region Collections for UI
        public ObservableCollection<BillingItem> InvoiceItems { get; set; }
        public ObservableCollection<CompanyInfo> CompanyNames { get; set; }
        public ObservableCollection<ProductInfo> MedicineList { get; set; }
        #endregion

        #region Properties for Data Binding
        private CompanyInfo? _selectedCompany;
        public CompanyInfo? SelectedCompany
        {
            get => _selectedCompany;
            set { _selectedCompany = value; OnPropertyChanged(nameof(SelectedCompany)); }
        }

        private ProductInfo? _newProduct;
        public ProductInfo? NewProduct
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
            CompanyNames = new ObservableCollection<CompanyInfo>();
            MedicineList = new ObservableCollection<ProductInfo>();
            InvoiceItems.CollectionChanged += (s, e) => UpdateInvoiceSummary();

            AddToInvoiceCommand = new RelayCommand(_ => AddToInvoice(), _ => CanAddToInvoice());
            ClearInvoiceCommand = new RelayCommand(_ => ClearInvoice());
            PrintInvoiceCommand = new RelayCommand(_ => PrintInvoice());

            LoadInitialData();
        }

        private void LoadInitialData()
        {
            CompanyNames.Clear();
            MedicineList.Clear();
            try
            {
                using (var db = new AppDbContext())
                {
                    // Load companies from the database
                    var companies = db.CompanyInfos.ToList();
                    foreach (var company in companies)
                    {
                        CompanyNames.Add(company);
                    }

                    // Load products from the database
                    var products = db.Products.ToList();
                    foreach (var product in products)
                    {
                        MedicineList.Add(product);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data from database: {ex.Message}\n\nHave you run the migrations?", "Database Error");
            }
        }

        private void AddToInvoice()
        {
            if (NewProduct == null) return; // Safety check

            var newItem = new BillingItem
            {
                ProductName = this.NewProduct.ProductName,
                Quantity = this.NewQty,
                BatchNo = this.NewProduct.BatchNo ?? "N/A",
                Hsn = this.NewProduct.HSNCode ?? "N/A",
                Pac = this.NewProduct.Packaging ?? "N/A",
                Exp = this.NewProduct.ExpiryDate ?? "N/A",
                Mrp = this.NewProduct.MRP,
                Ptr = this.NewProduct.PTR,
                Value = this.NewQty * this.NewProduct.PTR, // Value = Qty * PTR
                Fr = "N/A" // Placeholder
            };

            InvoiceItems.Add(newItem);

            NewProduct = null;
            NewQty = 1;
        }

        private bool CanAddToInvoice()
        {
            return NewProduct != null && NewQty > 0;
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
            SelectedCompany = null;
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