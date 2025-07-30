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
    public class BillingItem : INotifyPropertyChanged
    {
        private int _freeQty;
        public int FreeQty { get => _freeQty; set { _freeQty = value; OnPropertyChanged(); } }

        private string _productName = string.Empty;
        public string ProductName { get => _productName; set { _productName = value; OnPropertyChanged(); } }
        private string _batchNo = string.Empty;
        public string BatchNo { get => _batchNo; set { _batchNo = value; OnPropertyChanged(); } }
        private string _hsn = string.Empty;
        public string Hsn { get => _hsn; set { _hsn = value; OnPropertyChanged(); } }
        private string _pac = string.Empty;
        public string Pac { get => _pac; set { _pac = value; OnPropertyChanged(); } }
        private int _quantity;
        public int Quantity { get => _quantity; set { _quantity = value; OnPropertyChanged(); } }
        private string _fr = string.Empty;
        public string Fr { get => _fr; set { _fr = value; OnPropertyChanged(); } }
        private string _exp = string.Empty;
        public string Exp { get => _exp; set { _exp = value; OnPropertyChanged(); } }
        private decimal _mrp;
        public decimal Mrp { get => _mrp; set { _mrp = value; OnPropertyChanged(); } }
        private decimal _ptr;
        public decimal Ptr { get => _ptr; set { _ptr = value; OnPropertyChanged(); } }
        private decimal _value;
        public decimal Value { get => _value; set { _value = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class BillingViewModel : INotifyPropertyChanged
    {
        #region Collections
        public ObservableCollection<BillingItem> InvoiceItems { get; set; }
        public ObservableCollection<CompanyInfo> CompanyList { get; set; }
        public ObservableCollection<ProductInfo> MedicineList { get; set; }
        public ObservableCollection<string> SalesRepList { get; set; }
        #endregion

        #region Bound Properties
        private CompanyInfo? _selectedCompany;
        public CompanyInfo? SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                _selectedCompany = value;
                OnPropertyChanged(nameof(SelectedCompany));
                if (value != null && value.CompanyName != ClientSearchText)
                {
                    ClientSearchText = value.CompanyName;
                }
                if (value != null)
                {
                    IsNewClientEntryVisible = false;
                }
            }
        }

        private ProductInfo? _newProduct;
        public ProductInfo? NewProduct
        {
            get => _newProduct;
            set
            {
                _newProduct = value;
                OnPropertyChanged(nameof(NewProduct));
                if (value != null)
                {
                    NewItemPtr = value.PTR;
                    TotalGstPercent = 12; // Default GST
                }
            }
        }

        private int _newQty = 1;
        public int NewQty { get => _newQty; set { _newQty = value; OnPropertyChanged(nameof(NewQty)); } }

        private decimal _newItemPtr;
        public decimal NewItemPtr { get => _newItemPtr; set { _newItemPtr = value; OnPropertyChanged(nameof(NewItemPtr)); } }

        private int _newItemFreeQty;
        public int NewItemFreeQty { get => _newItemFreeQty; set { _newItemFreeQty = value; OnPropertyChanged(nameof(NewItemFreeQty)); } }

        private decimal _totalGstPercent;
        public decimal TotalGstPercent { get => _totalGstPercent; set { _totalGstPercent = value; OnPropertyChanged(nameof(TotalGstPercent)); UpdateInvoiceSummary(); } }

        private string _dispatchSource = "Own Inventory";
        public string DispatchSource { get => _dispatchSource; set { _dispatchSource = value; OnPropertyChanged(nameof(DispatchSource)); } }

        private string? _selectedSalesRep;
        public string? SelectedSalesRep { get => _selectedSalesRep; set { _selectedSalesRep = value; OnPropertyChanged(nameof(SelectedSalesRep)); } }

        public string BillerName { get; } = "Suresh Krishna";

        private string _clientSearchText = string.Empty;
        public string ClientSearchText { get => _clientSearchText; set { if (_clientSearchText != value) { _clientSearchText = value; OnPropertyChanged(nameof(ClientSearchText)); CheckForNewClient(); } } }

        private string _newClientGstin = string.Empty;
        public string NewClientGstin { get => _newClientGstin; set { _newClientGstin = value; OnPropertyChanged(nameof(NewClientGstin)); } }

        private string _newClientAddress = string.Empty;
        public string NewClientAddress { get => _newClientAddress; set { _newClientAddress = value; OnPropertyChanged(nameof(NewClientAddress)); } }

        private bool _isNewClientEntryVisible;
        public bool IsNewClientEntryVisible { get => _isNewClientEntryVisible; set { _isNewClientEntryVisible = value; OnPropertyChanged(nameof(IsNewClientEntryVisible)); } }
        #endregion

        #region Summary Properties
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
        public ICommand SaveNewClientCommand { get; }
        #endregion

        public BillingViewModel()
        {
            InvoiceItems = new ObservableCollection<BillingItem>();
            CompanyList = new ObservableCollection<CompanyInfo>();
            MedicineList = new ObservableCollection<ProductInfo>();
            SalesRepList = new ObservableCollection<string>();
            InvoiceItems.CollectionChanged += (s, e) => UpdateInvoiceSummary();

            AddToInvoiceCommand = new RelayCommand(_ => AddToInvoice(), _ => CanAddToInvoice());
            ClearInvoiceCommand = new RelayCommand(_ => ClearInvoice());
            PrintInvoiceCommand = new RelayCommand(_ => PrintInvoice());
            SaveNewClientCommand = new RelayCommand(_ => SaveNewClient(), _ => CanSaveNewClient());

            LoadInitialData();
        }

        private void LoadInitialData()
        {
            CompanyList.Clear();
            MedicineList.Clear();
            SalesRepList.Clear();
            try
            {
                using (var db = new AppDbContext())
                {
                    var companies = db.CompanyInfos.ToList();
                    foreach (var company in companies) { CompanyList.Add(company); }
                    var products = db.Products.ToList();
                    foreach (var product in products) { MedicineList.Add(product); }
                }
                SalesRepList.Add("Ramesh Kumar");
                SalesRepList.Add("Sathish");
            }
            catch (Exception ex) { MessageBox.Show($"Error loading data from database: {ex.Message}", "Database Error"); }
        }

        private void AddToInvoice()
        {
            if (NewProduct == null) return;
            int billableQuantity = NewQty - NewItemFreeQty;
            if (billableQuantity < 0) billableQuantity = 0;

            var newItem = new BillingItem
            {
                ProductName = this.NewProduct.ProductName,
                Quantity = this.NewQty,
                FreeQty = this.NewItemFreeQty,
                BatchNo = this.NewProduct.BatchNo ?? "N/A",
                Hsn = this.NewProduct.HSNCode ?? "N/A",
                Pac = this.NewProduct.Packaging ?? "N/A",
                Exp = this.NewProduct.ExpiryDate ?? "N/A",
                Mrp = this.NewProduct.MRP,
                Ptr = this.NewItemPtr,
                Value = billableQuantity * this.NewItemPtr
            };
            InvoiceItems.Add(newItem);

            NewProduct = null;
            NewQty = 1;
            NewItemPtr = 0;
            NewItemFreeQty = 0;
            TotalGstPercent = 0;
        }

        private bool CanAddToInvoice() { return NewProduct != null && NewQty > 0; }

        private void UpdateInvoiceSummary()
        {
            SubTotal = InvoiceItems.Sum(item => item.Value);
            decimal gstRate = TotalGstPercent / 100;
            Sgst = SubTotal * (gstRate / 2);
            Cgst = SubTotal * (gstRate / 2);
            decimal totalBeforeRoundOff = SubTotal + Sgst + Cgst;
            GrandTotal = Math.Round(totalBeforeRoundOff, MidpointRounding.AwayFromZero);
            RoundOff = GrandTotal - totalBeforeRoundOff;
        }

        private void ClearInvoice()
        {
            InvoiceItems.Clear();
            SelectedCompany = null;
            ClientSearchText = string.Empty;
            SelectedSalesRep = null;
            TotalGstPercent = 0;
        }

        private void PrintInvoice() { MessageBox.Show("Printing logic will be implemented here.", "Print Invoice"); }

        private void CheckForNewClient()
        {
            if (!string.IsNullOrWhiteSpace(ClientSearchText) && !CompanyList.Any(c => c.CompanyName.Equals(ClientSearchText, StringComparison.OrdinalIgnoreCase)))
            {
                IsNewClientEntryVisible = true;
            }
            else
            {
                IsNewClientEntryVisible = false;
            }
        }

        private void SaveNewClient()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var newCompany = new CompanyInfo
                    {
                        CompanyName = this.ClientSearchText,
                        GSTIN = this.NewClientGstin,
                        AddressLine1 = this.NewClientAddress,
                        ContactNumber = "N/A",
                        PAN = "N/A",
                        CompanyType = "N/A",
                        AddressLine2 = "",
                        AddressLine3 = "",
                        ZipCode = "",
                        NickName = "",
                        Website = ""
                    };
                    db.CompanyInfos.Add(newCompany);
                    db.SaveChanges();
                    MessageBox.Show("New client saved successfully!", "Success");
                    NewClientGstin = string.Empty;
                    NewClientAddress = string.Empty;
                    IsNewClientEntryVisible = false;
                    LoadInitialData();
                    SelectedCompany = CompanyList.FirstOrDefault(c => c.CompanyName == newCompany.CompanyName);
                }
            }
            catch (Exception ex) { MessageBox.Show($"Error saving new client: {ex.Message}", "Database Error"); }
        }

        private bool CanSaveNewClient()
        {
            return IsNewClientEntryVisible &&
                   !string.IsNullOrWhiteSpace(ClientSearchText) &&
                   !string.IsNullOrWhiteSpace(NewClientGstin) &&
                   !string.IsNullOrWhiteSpace(NewClientAddress);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}