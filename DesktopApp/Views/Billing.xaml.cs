using DesktopApp.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for Billing.xaml
    /// </summary>
    public partial class Billing : UserControl
    {
        public Billing()
        {
            InitializeComponent();
            DataContext = new BillingViewModel();
            SetSelectedButton(SellingBillsButton);
        }

        private void SellingBillsButton_Click(object sender, RoutedEventArgs e)
        {
            SetSelectedButton(SellingBillsButton);
            // Set active styles for the Selling Bills button
            SellingBillsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A4967"));
            SellingBillsButton.Foreground = Brushes.White;

            // Set inactive styles for the Purchase Bills button
            PurchaseBillsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E9E9E9"));
            PurchaseBillsButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A4967"));

            // TODO: Add any logic needed when switching to "Selling" mode
        }

        private void PurchaseBillsButton_Click(object sender, RoutedEventArgs e)
        {
            SetSelectedButton(PurchaseBillsButton);
            // Set active styles for the Purchase Bills button
            PurchaseBillsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A4967"));
            PurchaseBillsButton.Foreground = Brushes.White;

            // Set inactive styles for the Selling Bills button
            SellingBillsButton.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E9E9E9"));
            SellingBillsButton.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A4967"));

            // TODO: Add any logic needed when switching to "Purchase" mode
        }

        private void SetSelectedButton(Button selected)
        {
            // Remove the shadow effect from both buttons first
            SellingBillsButton.Effect = null;
            PurchaseBillsButton.Effect = null;

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