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
        private string _productName = string.Empty;
        public string ProductName
        {
            get => _productName;
            set { _productName = value; OnPropertyChanged(nameof(ProductName)); }
        }

        private string _batchNo = string.Empty;
        public string BatchNo
        {
            get => _batchNo;
            set { _batchNo = value; OnPropertyChanged(nameof(BatchNo)); }
        }

        private string _hsnCode = string.Empty;
        public string HSNCode
        {
            get => _hsnCode;
            set { _hsnCode = value; OnPropertyChanged(nameof(HSNCode)); }
        }

        private string _packaging = string.Empty;
        public string Packaging
        {
            get => _packaging;
            set { _packaging = value; OnPropertyChanged(nameof(Packaging)); }
        }

        private int _stockQuantity;
        public int StockQuantity
        {
            get => _stockQuantity;
            set { _stockQuantity = value; OnPropertyChanged(nameof(StockQuantity)); }
        }

        private string _expiryDate = string.Empty;
        public string ExpiryDate
        {
            get => _expiryDate;
            set { _expiryDate = value; OnPropertyChanged(nameof(ExpiryDate)); }
        }

        private decimal _mrp;
        public decimal MRP
        {
            get => _mrp;
            set { _mrp = value; OnPropertyChanged(nameof(MRP)); }
        }

        private decimal _ptr;
        public decimal PTR
        {
            get => _ptr;
            set { _ptr = value; OnPropertyChanged(nameof(PTR)); }
        }

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
            SaveCommand = new RelayCommand(_ => SaveData(), _ => CanSave());
            ResetCommand = new RelayCommand(_ => ResetData());
            LoadProducts();
        }

        private void LoadProducts()
        {
            Products.Clear();
            try
            {
                using (var db = new AppDbContext())
                {
                    var products = db.Products.ToList();
                    foreach (var p in products)
                    {
                        Products.Add(p);
                    }
                }
            }
            catch (Exception ex)
            {
                // This might happen if the migration hasn't been run yet.
                // It's okay to ignore here as the list will just be empty.
                System.Diagnostics.Debug.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        private void LoadProductDetails(ProductInfo product)
        {
            ProductName = product.ProductName;
            BatchNo = product.BatchNo ?? "";
            HSNCode = product.HSNCode ?? "";
            Packaging = product.Packaging ?? "";
            StockQuantity = product.StockQuantity;
            ExpiryDate = product.ExpiryDate ?? "";
            MRP = product.MRP;
            PTR = product.PTR;
        }

        // --- IMPLEMENTATION ---
        /// <summary>
        /// Saves the new product data to the database.
        /// </summary>
        private void SaveData()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var product = new ProductInfo
                    {
                        ProductName = this.ProductName,
                        BatchNo = this.BatchNo,
                        HSNCode = this.HSNCode,
                        Packaging = this.Packaging,
                        StockQuantity = this.StockQuantity,
                        ExpiryDate = this.ExpiryDate,
                        MRP = this.MRP,
                        PTR = this.PTR
                    };
                    db.Products.Add(product);
                    db.SaveChanges();
                    MessageBox.Show("Product saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    ResetData(); // Clear the form
                    LoadProducts(); // Refresh the product list for the "Edit" dropdown
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving product to database: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Determines if the Save button should be enabled.
        /// </summary>
        private bool CanSave()
        {
            // Basic validation: requires a product name and a price.
            return !string.IsNullOrWhiteSpace(ProductName) && MRP > 0 && PTR > 0;
        }

        /// <summary>
        /// Clears all input fields on the form.
        /// </summary>
        private void ResetData()
        {
            ProductName = string.Empty;
            BatchNo = string.Empty;
            HSNCode = string.Empty;
            Packaging = string.Empty;
            StockQuantity = 0;
            ExpiryDate = string.Empty;
            MRP = 0;
            PTR = 0;
            SelectedProduct = null;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // Ensures the CanSave method is re-evaluated when properties change
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion
    }
}