using DesktopApp.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace DesktopApp.Views
{
    public partial class DataEntryPage : UserControl
    {
        public DataEntryPage()
        {
            InitializeComponent();
            this.DataContext = new DataEntryViewModel();
            SetSelectedButton(Add);
        }

        private void AddCompanyClick(object sender, RoutedEventArgs e)
        {
            SetSelectedButton(Add);
            Add.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A4967"));
            Add.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));

            Edit.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E9E9E9"));
            Edit.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A4967"));

            Name_company.Visibility = Visibility.Visible;
            Combo_company.Visibility = Visibility.Collapsed;
        }

        private void EditCompanyClick(object sender, RoutedEventArgs e)
        {
            SetSelectedButton(Edit);
            Edit.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A4967"));
            Edit.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));

            Add.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E9E9E9"));
            Add.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A4967"));

            Name_company.Visibility = Visibility.Collapsed;
            Combo_company.Visibility = Visibility.Visible;

        }

        private void SetSelectedButton(Button selected)
        {
            // Remove shadow from all buttons first
            Add.Effect = null;
            Edit.Effect = null;

            // Add shadow to selected one
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
