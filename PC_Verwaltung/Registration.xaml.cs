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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MaterialDesignThemes.Wpf;
using PC_Verwaltung.Dashboard;

namespace PC_Verwaltung
{
    /// <summary>
    /// Interaktionslogik für Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Window w;
        public string name { get; set; }
        public string surname { get; set; }

        public string username { get; set; }

        public string email { get; set; }
        public string password { get; set; }
        public string passwordconfirm { get; set; }

        public Registration(Window w)
        {
            this.w = w;
            InitializeComponent();
            DataContext = this;
            card_reveal_pw.Visibility = Visibility.Collapsed;

            this.Activate();

            Loaded += (sender, e) =>
            MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void click_close(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(1);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Btn_register_Click(object sender, RoutedEventArgs e)
        {
            register();
        }

        private void Btn_abort_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            w.Show();
        }

        private void click_eye_password(object sender, MouseButtonEventArgs e)
        {
            if (card_reveal_pw.Visibility == Visibility.Collapsed)
            {
                card_reveal_pw.Visibility = Visibility.Visible;
                password_eye.Kind = MaterialDesignThemes.Wpf.PackIconKind.Eye;
            }
            else
            {
                card_reveal_pw.Visibility = Visibility.Collapsed;
                password_eye.Kind = MaterialDesignThemes.Wpf.PackIconKind.EyeOff;
            }
        }

        private void Pb_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            card_reveal_pw_text.Text = pb_password.Password;
            tb_notification.Text = "";
        }

        private void register()
        {
            //Check Username in Database
            if (!(string.IsNullOrEmpty(name) | string.IsNullOrEmpty(surname) | string.IsNullOrEmpty(username)))
            {
                if (pb_password.Password.Any(ch => !Char.IsLetterOrDigit(ch)) && pb_password.Password.Any(ch => !Char.IsSymbol(ch)))
                {
                    if(pb_password.Password == pb_passwordconfirm.Password)
                    {
                        if (cb_nb.IsChecked == true)
                        {
                            User newUser = new User(name, surname, username, email, pb_password.Password, true);
                            if (MainWindow.database.createNewUser(newUser) == true)
                            {
                                MainDashboardWindow dashboard = new MainDashboardWindow(newUser);
                                dashboard.Show();
                                this.Close();
                            }
                            else
                            {
                                tb_notification.Foreground = Brushes.Red;
                                tb_notification.Text = "Den Username oder die Email\nexistieren bereits!";
                            }
                        }
                        else
                        {
                            tb_notification.Foreground = Brushes.Red;
                            tb_notification.Text = "Bitte stimme den Nutzungsbedingungen zu";
                        }
                    }
                    else
                    {
                        tb_notification.Foreground = Brushes.Red;
                        tb_notification.Text = "Die Passwörter stimmen nicht überein!";
                    }
                }
                else
                {
                    tb_notification.Foreground = Brushes.Red;
                    tb_notification.Text = "Das Passwort muss: Groß- und Kleinschreibung\nNummern und Sonderzeichen enthalten!";
                }
            }
            else
            {
                tb_notification.Foreground = Brushes.Red;
                tb_notification.Text = "Bitte überprüfe deine Eingaben";
            }
        }

        private void Pb_password_LostFocus(object sender, RoutedEventArgs e)
        {
            card_reveal_pw.Visibility = Visibility.Hidden;
            password_eye.Kind = PackIconKind.EyeOff;
        }
    }
}
