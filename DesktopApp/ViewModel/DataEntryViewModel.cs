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
    public class DataEntryViewModel : INotifyPropertyChanged
    {
        // Fields
        private string _companyName = string.Empty;
        private string _contactNumber = string.Empty;
        private string _gstin = string.Empty;
        private string _pan = string.Empty;
        private string _email = string.Empty;
        private string _companyType = string.Empty;
        private string _addressLine1 = string.Empty;
        private string _addressLine2 = string.Empty;
        private string _addressLine3 = string.Empty;
        private string _zipCode = string.Empty;
        private string _nickName = string.Empty;
        private string _website = string.Empty;
        private double _discount;

        private string _selectedCompany = string.Empty;

        // Properties
        public ObservableCollection<string> CompanyNames { get; set; } = new();

        public string SelectedCompany
        {
            get => _selectedCompany;
            set
            {
                _selectedCompany = value;
                OnPropertyChanged(nameof(SelectedCompany));
                LoadCompanyDetails(_selectedCompany);
            }
        }

        public string CompanyName
        {
            get => _companyName;
            set { _companyName = value; OnPropertyChanged(nameof(CompanyName)); }
        }

        public string ContactNumber
        {
            get => _contactNumber;
            set { _contactNumber = value; OnPropertyChanged(nameof(ContactNumber)); }
        }

        public string GSTIN
        {
            get => _gstin;
            set { _gstin = value; OnPropertyChanged(nameof(GSTIN)); }
        }

        public string PAN
        {
            get => _pan;
            set { _pan = value; OnPropertyChanged(nameof(PAN)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(nameof(Email)); }
        }

        public string CompanyType
        {
            get => _companyType;
            set { _companyType = value; OnPropertyChanged(nameof(CompanyType)); }
        }

        public string AddressLine1
        {
            get => _addressLine1;
            set { _addressLine1 = value; OnPropertyChanged(nameof(AddressLine1)); }
        }

        public string AddressLine2
        {
            get => _addressLine2;
            set { _addressLine2 = value; OnPropertyChanged(nameof(AddressLine2)); }
        }

        public string AddressLine3
        {
            get => _addressLine3;
            set { _addressLine3 = value; OnPropertyChanged(nameof(AddressLine3)); }
        }

        public string ZipCode
        {
            get => _zipCode;
            set { _zipCode = value; OnPropertyChanged(nameof(ZipCode)); }
        }

        public string NickName
        {
            get => _nickName;
            set { _nickName = value; OnPropertyChanged(nameof(NickName)); }
        }

        public string Website
        {
            get => _website;
            set { _website = value; OnPropertyChanged(nameof(Website)); }
        }

        public double Discount
        {
            get => _discount;
            set { _discount = value; OnPropertyChanged(nameof(Discount)); }
        }

        // Commands
        public ICommand SaveCommand { get; }
        public ICommand ResetCommand { get; }

        // Constructor
        public DataEntryViewModel()
        {
            SaveCommand = new RelayCommand(SaveData, CanSave);
            ResetCommand = new RelayCommand(_ => ResetData());

            LoadCompanyNames(); // Populate ComboBox
        }

        // Load list of company names
        private void LoadCompanyNames()
        {
            using var db = new AppDbContext();
            var names = db.CompanyInfos.Select(c => c.CompanyName).Distinct().ToList();
            CompanyNames.Clear();
            foreach (var name in names)
                CompanyNames.Add(name);
        }

        // Load data when ComboBox changes
        private void LoadCompanyDetails(string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName)) return;

            using var db = new AppDbContext();
            var company = db.CompanyInfos.FirstOrDefault(c => c.CompanyName == companyName);
            if (company != null)
            {
                CompanyName = company.CompanyName;
                ContactNumber = company.ContactNumber;
                GSTIN = company.GSTIN;
                PAN = company.PAN;
                Email = company.Email;
                CompanyType = company.CompanyType;
                AddressLine1 = company.AddressLine1;
                AddressLine2 = company.AddressLine2;
                AddressLine3 = company.AddressLine3;
                ZipCode = company.ZipCode;
                NickName = company.NickName;
                Website = company.Website;
                Discount = company.Discount;
            }
        }

        // Save Logic
        private void SaveData(object? obj)
        {
            try
            {
                using var db = new AppDbContext();
                var company = new CompanyInfo
                {
                    CompanyName = CompanyName,
                    ContactNumber = ContactNumber,
                    GSTIN = GSTIN,
                    PAN = PAN,
                    Email = Email,
                    CompanyType = CompanyType,
                    AddressLine1 = AddressLine1,
                    AddressLine2 = AddressLine2,
                    AddressLine3 = AddressLine3,
                    ZipCode = ZipCode,
                    NickName = NickName,
                    Website = Website,
                    Discount = Discount
                };
                db.CompanyInfos.Add(company);
                db.SaveChanges();
                MessageBox.Show("Saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadCompanyNames(); // Refresh dropdown
            }
            catch (Exception ex)
            {
                var msg = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show($"Error saving to DB: {msg}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Validation
        private bool CanSave(object? obj) =>
            !string.IsNullOrWhiteSpace(CompanyName);

        // Reset Logic
        private void ResetData()
        {
            CompanyName = string.Empty;
            ContactNumber = string.Empty;
            GSTIN = string.Empty;
            PAN = string.Empty;
            Email = string.Empty;
            CompanyType = string.Empty;
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
            AddressLine3 = string.Empty;
            ZipCode = string.Empty;
            NickName = string.Empty;
            Website = string.Empty;
            Discount = 0;
        }

        // INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
