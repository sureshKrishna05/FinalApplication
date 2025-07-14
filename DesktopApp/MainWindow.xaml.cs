using DesktopApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dashboard dash = new Dashboard();
        History his = new History();
        DataEntryPage dta = new DataEntryPage();
        Billing bill = new Billing();

        public MainWindow()
        {

            InitializeComponent();

            dash.HorizontalAlignment = HorizontalAlignment.Stretch;
            //  contentArea.Children.Add(new Views.Dashboard());
            LV.SelectedItem = DashboardButton;
            LoadPage("DashBoard");

        }

        /*  private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
          {
              // Set tooltip visibility

              if (Tg_Btn.IsChecked == true)
              {
                  tt_home.Visibility = Visibility.Collapsed;
                  tt_contacts.Visibility = Visibility.Collapsed;
                  tt_messages.Visibility = Visibility.Collapsed;
                  tt_maps.Visibility = Visibility.Collapsed;
                  tt_settings.Visibility = Visibility.Collapsed;
                  tt_signout.Visibility = Visibility.Collapsed;
              }
              else
              {
                  tt_home.Visibility = Visibility.Visible;
                  tt_contacts.Visibility = Visibility.Visible;
                  tt_messages.Visibility = Visibility.Visible;
                  tt_maps.Visibility = Visibility.Visible;
                  tt_settings.Visibility = Visibility.Visible;
                  tt_signout.Visibility = Visibility.Visible;
              }
          }

          private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
          {

          }

          private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
          {

          }*/

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void MimimizeButton(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void GridText(string message)
        {

            Header.Text = message;
        }
        private void LoadPage(string page)
        {
            contentArea.Children.Clear();

            switch (page)
            {
                case "DashBoard":
                    contentArea.Children.Clear();
                    GridText("DashBoard");
                    dash.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                    contentArea.Children.Add(new Views.Dashboard());
                    break;

                case "History":
                    contentArea.Children.Clear();
                    GridText("Order History");
                    his.HorizontalAlignment = HorizontalAlignment.Stretch;
                    contentArea.Children.Add(new Views.History());
                    break;

                case "Invoice":
                    contentArea.Children.Clear();
                    GridText("Test");
                    dta.HorizontalAlignment = HorizontalAlignment.Stretch;
                    contentArea.Children.Add(new Views.DataEntryPage());
                    break;

                case "Billing":
                    contentArea.Children.Clear();
                    GridText("Billing");
                    bill.HorizontalAlignment = HorizontalAlignment.Stretch;
                    contentArea.Children.Add(new Views.Billing());
                    break;
            }
        }
        private void LVItemClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item && item.Tag is string pageTag)
            {
                LoadPage(pageTag);
            }
        }

        private void LoginBtnClick(object sender, RoutedEventArgs e)
        {
            SetSelectedButton(Login);

            var imagePath = "pack://application:,,,/Assets/login2.png";
            // Set background and foreground colors
            Login.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#E9E9E9"));
            loginText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#565656"));

            // Set new image source properly
            loginImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            MessageBox.Show("Button Clicked");
        }
        private void SetSelectedButton(Button selected)
        {
            // Remove shadow from all buttons first
            Login.Effect = null;

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
