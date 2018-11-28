using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PC_Verwaltung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static Database database = new Database("127.0.0.1", "pc_verwaltung", "PCV", "1234");
        public static User currentUser;

        public MainWindow()
        {
            InitializeComponent();
            if (!database.connect())
            {
                MessageBoxResult r = MessageBox.Show("Es konnte keine Verbindung zur Datenbank aufgebaut werden.\nWollen sie es erneut versuchen?", "Fehler beim Zugriff auf die Datenbank", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if(r == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
                else
                {
                    Application.Current.Shutdown();
                }
                
            }
        }

        /*
         * EVENT HANDLER
         */

        //Drag and Drop the Login Window (Click everywhere)
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        //When the User clicks on the Exit Button
        private void click_close(object sender, MouseButtonEventArgs e)
        {
            System.Environment.Exit(1);
        }

        // Redirect to login() Method, when the Button 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            login();
        }

        private void key_down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                login();
            }
        }

        private void select_input(object sender, RoutedEventArgs e)
        {
            tb_notification.Text = "";
        }

        private void focus_pw(object sender, RoutedEventArgs e)
        {
            tb_notification.Text = "";
        }

        /*
         * Methods
         */

        private void login()
        {
            string username = tb_username.Text.Trim();

            if (Regex.IsMatch(username, @"[a-zA-Z]+\w+") && database.GetUser(username).username == username)
            {

                currentUser = database.GetUser(username);

                if(currentUser != null && currentUser.password == User.sha256(pb_password.Password))
                {
                    this.Close();
                    MessageBox.Show("Herzlich Wilkommen, Admin!");
                }
            }
            else
            {
                tb_notification.Foreground = Brushes.Red;
                tb_notification.Text = "Username oder Passwort falsch";
            }
        }

    }

}
