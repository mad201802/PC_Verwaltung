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
using PC_Verwaltung.Dasboard.Canvas;

namespace PC_Verwaltung.Dasboard
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
            if(string.IsNullOrEmpty(current.name + current.surname))
            {
                chip_currentuser.Content = "Admin";
            }

            MainGrid.Children.Add(new DashboardUC());

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

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "dashboard":
                    current_topic.Text = "Dashboard"; 
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new DashboardUC());
                    break;
                case "item1":
                    current_topic.Text = "Example Item 1";
                    break;
                case "item2":
                    current_topic.Text = "Example Items 2";
                    break;
                default:
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new DashboardUC());
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
    }
}
