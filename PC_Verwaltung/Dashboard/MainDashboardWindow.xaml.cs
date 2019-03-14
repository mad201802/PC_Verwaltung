using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PC_Verwaltung.Dashboard
{
    /// <summary>
    /// Interaktionslogik für MainDashboardWindow.xaml
    /// </summary>
    public partial class MainDashboardWindow : Window
    {
        public User current;

        public MainDashboardWindow(User current)
        {
            InitializeComponent();
            this.current = current;
            this.Visibility = Visibility.Collapsed;

            chip_currentuser.Content = current.name + " " + current.surname;

            Console.WriteLine();

            if (string.IsNullOrEmpty(current.name + current.surname))
            {
                chip_currentuser.Content = "Admin";
            }

            ScrollGrid.Children.Add(new UserControls.MonitoringUC());

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                    this.DragMove();
            }
            catch (Exception ex)
            {

            }
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);

            Console.WriteLine(((ListViewItem)((ListView)sender).SelectedItem).Name);

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "dashboard":
                    current_topic.Text = "Dashboard";
                    ScrollGrid.Children.Clear();
                    ScrollGrid.Children.Add(new UserControls.MonitoringUC());
                    break;
                case "my_computer":
                    current_topic.Text = "Mein Computer";
                    ScrollGrid.Children.Clear();
                    break;
            }
        }

        private void Btn_close_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Wollen sie das Fenster wirklich schließen?", "PC Verwaltung", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                Environment.Exit(1);
            }
        }

        private void MoveCursorMenu(int index)
        {
            MenuCursor.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }

        private void Btn_logout_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Btn_settings_Click(object sender, RoutedEventArgs e)
        {
            ScrollGrid.Children.Clear();
            ScrollGrid.Children.Add(new UserControls.Settings());
        }

    }
}
