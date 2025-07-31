using DesktopApp.Data;
using DesktopApp.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DesktopApp.ViewModel
{
    public class ProductEntryViewModel : INotifyPropertyChanged
    {
        #region Fields & Properties
        private int _editingProductId = 0;

        private string _productName = string.Empty;
        public string ProductName { get => _productName; set { _productName = value; OnPropertyChanged(nameof(ProductName)); } }

        private string _batchNo = string.Empty;
        public string BatchNo { get => _batchNo; set { _batchNo = value; OnPropertyChanged(nameof(BatchNo)); } }

        private string _hsnCode = string.Empty;
        public string HSNCode { get => _hsnCode; set { _hsnCode = value; OnPropertyChanged(nameof(HSNCode)); } }

        private string _packaging = string.Empty;
        public string Packaging { get => _packaging; set { _packaging = value; OnPropertyChanged(nameof(Packaging)); } }

        private int _stockQuantity;
        public int StockQuantity { get => _stockQuantity; set { _stockQuantity = value; OnPropertyChanged(nameof(StockQuantity)); } }

        private string _expiryDate = string.Empty;
        public string ExpiryDate { get => _expiryDate; set { _expiryDate = value; OnPropertyChanged(nameof(ExpiryDate)); } }

        private decimal _mrp;
        public decimal MRP { get => _mrp; set { _mrp = value; OnPropertyChanged(nameof(MRP)); } }

        private decimal _ptr;
        public decimal PTR { get => _ptr; set { _ptr = value; OnPropertyChanged(nameof(PTR)); } }

        private decimal _cost;
        public decimal COST { get => _cost; set { _cost = value; OnPropertyChanged(nameof(COST)); } }

        private string? _productCategory;
        public string? ProductCategory { get => _productCategory; set { _productCategory = value; OnPropertyChanged(nameof(ProductCategory)); } }

        // --- NEW COLLECTION FOR THE COMBOBOX ---
        public ObservableCollection<string> ProductCategories { get; set; }

        public ObservableCollection<ProductInfo> Products { get; set; }

        private ProductInfo? _selectedProduct;
        public ProductInfo? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                if (value != null) LoadProductDetails(value);
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }
        #endregion

        #region Commands
        public ICommand SaveCommand { get; }
        public ICommand ResetCommand { get; }
        #endregion

        public ProductEntryViewModel()
        {
            Products = new ObservableCollection<ProductInfo>();
            // --- INITIALIZE THE CATEGORIES LIST ---
            ProductCategories = new ObservableCollection<string> { "Tablet", "Syrup", "Ointment", "Injection", "Capsule", "Other" };

            SaveCommand = new RelayCommand(_ => SaveData(), _ => CanSave());
            ResetCommand = new RelayCommand(_ => ResetData());
            LoadProducts();
        }

        private void LoadProducts()
        {
            Products.Clear();
            using (var db = new AppDbContext())
            {
                var products = db.Products.ToList();
                foreach (var p in products) { Products.Add(p); }
            }
        }

        private void LoadProductDetails(ProductInfo product)
        {
            _editingProductId = product.Id;
            ProductName = product.ProductName;
            BatchNo = product.BatchNo ?? "";
            HSNCode = product.HSNCode ?? "";
            Packaging = product.Packaging ?? "";
            StockQuantity = product.StockQuantity;
            ExpiryDate = product.ExpiryDate ?? "";
            MRP = product.MRP;
            PTR = product.PTR;
            COST = product.COST;
            ProductCategory = product.ProductCategory ?? "";
        }

        private void SaveData()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    if (_editingProductId != 0)
                    {
                        var existingProduct = db.Products.Find(_editingProductId);
                        if (existingProduct != null)
                        {
                            existingProduct.ProductName = this.ProductName;
                            existingProduct.BatchNo = this.BatchNo;
                            existingProduct.HSNCode = this.HSNCode;
                            existingProduct.Packaging = this.Packaging;
                            existingProduct.StockQuantity = this.StockQuantity;
                            existingProduct.ExpiryDate = this.ExpiryDate;
                            existingProduct.MRP = this.MRP;
                            existingProduct.PTR = this.PTR;
                            existingProduct.COST = this.COST;
                            existingProduct.ProductCategory = this.ProductCategory;
                            db.Products.Update(existingProduct);
                            MessageBox.Show("Product updated successfully!", "Success");
                        }
                    }
                    else
                    {
                        var newProduct = new ProductInfo
                        {
                            ProductName = this.ProductName,
                            BatchNo = this.BatchNo,
                            HSNCode = this.HSNCode,
                            Packaging = this.Packaging,
                            StockQuantity = this.StockQuantity,
                            ExpiryDate = this.ExpiryDate,
                            MRP = this.MRP,
                            PTR = this.PTR,
                            ProductCategory = this.ProductCategory,
                            COST = this.COST
                        };
                        db.Products.Add(newProduct);
                        MessageBox.Show("Product saved successfully!", "Success");
                    }

                    db.SaveChanges();
                    ResetData();
                    LoadProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving to database: {ex.Message}", "Database Error");
            }
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(ProductName);
        }

        private void ResetData()
        {
            _editingProductId = 0;
            ProductName = string.Empty;
            BatchNo = string.Empty;
            HSNCode = string.Empty;
            Packaging = string.Empty;
            StockQuantity = 0;
            ExpiryDate = string.Empty;
            MRP = 0;
            PTR = 0;
            ProductCategory = null;
            COST = 0;
            SelectedProduct = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}