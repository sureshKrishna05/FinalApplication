using DesktopApp.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace DesktopApp.Views
{
    public partial class ProductEntryPage : UserControl
    {
        public ProductEntryPage()
        {
            InitializeComponent();
            // Connect the view to its corresponding ViewModel
            this.DataContext = new ProductEntryViewModel();
            // Set the initial state to "Add" mode
            SetSelectedButton(Add);
        }

        private void AddMedicineClick(object sender, RoutedEventArgs e)
        {
            SetSelectedButton(Add);
            Add.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A4967"));
            Add.Foreground = Brushes.White;

            Edit.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E9E9E9"));
            Edit.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A4967"));

            // Show the TextBox for entering a new product name and hide the ComboBox
            Name_Product.Visibility = Visibility.Visible;
            Combo_Product.Visibility = Visibility.Collapsed;

            // Reset the form fields by calling the ViewModel's ResetCommand
            if (DataContext is ProductEntryViewModel vm)
            {
                vm.ResetCommand.Execute(null);
            }
        }

        private void EditMedicineClick(object sender, RoutedEventArgs e)
        {
            SetSelectedButton(Edit);
            Edit.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A4967"));
            Edit.Foreground = Brushes.White;

            Add.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E9E9E9"));
            Add.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A4967"));

            // Hide the TextBox and show the ComboBox for selecting an existing product
            Name_Product.Visibility = Visibility.Collapsed;
            Combo_Product.Visibility = Visibility.Visible;
        }

        private void SetSelectedButton(Button selected)
        {
            // Remove the shadow effect from both buttons first
            Add.Effect = null;
            Edit.Effect = null;

            // Apply the shadow effect to the newly selected button
            selected.Effect = new DropShadowEffect
            {
                Color = Colors.Black,
                Direction = 320,
                ShadowDepth = 5,
                Opacity = 0.5,
                BlurRadius = 10
            };
        }
    }
}