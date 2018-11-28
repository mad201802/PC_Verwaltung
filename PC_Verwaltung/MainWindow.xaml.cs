using System;
using System.Collections.Generic;
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

        public static Database database = new Database("192.168.43.204", "pc_verwaltung", "root", "");

        public MainWindow()
        {
            InitializeComponent();
            //Try to connect to Database
            if (!database.connect())
            {
                //MessageBox.Show("Es konnte keine Verbindung zur Datnbank hergestellt werden", "Fehler beim Verbinden", MessageBoxButton.OK, MessageBoxImage.Error);
                //System.Environment.Exit(1);
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
            //The Username should just contain Numbers and Alphabetis Chars. The Max. Lengh is set to 20 characters.
            string username = tb_username.Text.Trim();

            //Get the Database Information from username
            User loginUser = database.getUser(username);

            //Prevent SQL Injection, not allowed characters, and a null return
            if (loginUser != null && Regex.IsMatch(username, @"[a-zA-Z]+\w+") && database.getUser(username).username == username)
            {
                if(loginUser.password == User.sha256(pb_password.Password))
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
