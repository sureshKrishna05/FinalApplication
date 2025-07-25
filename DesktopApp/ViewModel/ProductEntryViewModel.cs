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
        // This field will track the ID of the product being edited.
        // If it's 0, we are in "Add" mode. Otherwise, we are in "Edit" mode.
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

        public ObservableCollection<ProductInfo> Products { get; set; }

        private ProductInfo? _selectedProduct;
        public ProductInfo? SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                // When a product is selected from the ComboBox, load its details.
                if (value != null)
                {
                    LoadProductDetails(value);
                }
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
            using (var db = new AppDbContext())
            {
                var products = db.Products.ToList();
                foreach (var p in products)
                {
                    Products.Add(p);
                }
            }
        }

        /// <summary>
        /// Populates the form fields with the data of the selected product for editing.
        /// </summary>
        private void LoadProductDetails(ProductInfo product)
        {
            _editingProductId = product.Id; // Store the ID, indicating we are in "Edit" mode.
            ProductName = product.ProductName;
            BatchNo = product.BatchNo ?? "";
            HSNCode = product.HSNCode ?? "";
            Packaging = product.Packaging ?? "";
            StockQuantity = product.StockQuantity;
            ExpiryDate = product.ExpiryDate ?? "";
            MRP = product.MRP;
            PTR = product.PTR;
        }

        /// <summary>
        /// Saves data. Updates an existing product if in edit mode, otherwise creates a new one.
        /// </summary>
        private void SaveData()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    // If _editingProductId is not 0, we are UPDATING an existing product.
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

                            db.Products.Update(existingProduct);
                            MessageBox.Show("Product updated successfully!", "Success");
                        }
                    }
                    // Otherwise, we are ADDING a new product.
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
                            PTR = this.PTR
                        };
                        db.Products.Add(newProduct);
                        MessageBox.Show("Product saved successfully!", "Success");
                    }

                    db.SaveChanges();
                    ResetData();
                    LoadProducts(); // Refresh the product list for the ComboBox
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

        /// <summary>
        /// Clears all input fields and resets the form to "Add" mode.
        /// </summary>
        private void ResetData()
        {
            _editingProductId = 0; // Crucial step to exit "Edit" mode.
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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            CommandManager.InvalidateRequerySuggested();
        }
    }
}