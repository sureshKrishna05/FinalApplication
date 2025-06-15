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
        Billing bill = new Billing();
        DataEntryPage dta = new DataEntryPage();
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
                    bill.HorizontalAlignment = HorizontalAlignment.Stretch;
                    contentArea.Children.Add(new Views.Billing());
                    break;

                case "Invoice":
                    contentArea.Children.Clear();
                    GridText("Test");
                    dta.HorizontalAlignment = HorizontalAlignment.Stretch;
                    contentArea.Children.Add(new Views.DataEntryPage());
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


    }
}
