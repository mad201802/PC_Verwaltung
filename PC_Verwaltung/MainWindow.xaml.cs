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

        public static Database database = new Database("localhost", "pc_verwaltung", "root", "");
        public static User currentUser;

        public MainWindow()
        {
            InitializeComponent();
            card_reveal_pw.Visibility = Visibility.Collapsed;
            
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
            Loaded += (sender, e) =>
            MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        #region Events
        
        //Drag and Drop des Anmeldefensters
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                    this.DragMove();
            }catch(Exception ex)
            {

            }
        }

        //Wenn auf das X in der rechten, oberen Ecke gedrückt wird
        private void click_close(object sender, MouseButtonEventArgs e)
        {
            System.Environment.Exit(1);
        }

        //Wenn der Button zum Einloggen gedrückt wird
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            login();
        }

        //Wenn Enter gedrückt wird, anstatt auf Login zu klicken
        private void key_down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                login();
            }
        }

        //Löschen der Anzeige-Textbox
        private void select_input(object sender, RoutedEventArgs e)
        {
            tb_notification.Text = "";
        }

        //Löschen der Anzeige-Textbox
        private void focus_pw(object sender, RoutedEventArgs e)
        {
            tb_notification.Text = "";
        }

        //Anzeigen des Passwortes in einer separaten Card
        private void click_eye(object sender, MouseButtonEventArgs e)
        {
            if (card_reveal_pw.Visibility == Visibility.Collapsed)
            {
                card_reveal_pw.Visibility = Visibility.Visible;
                packicon_show.Kind = MaterialDesignThemes.Wpf.PackIconKind.Eye;
            }
            else
            {
                card_reveal_pw.Visibility = Visibility.Collapsed;
                packicon_show.Kind = MaterialDesignThemes.Wpf.PackIconKind.EyeOff;
            }
        }

        //Updaten des Textes in der Password-Card
        private void on_password_change(object sender, RoutedEventArgs e)
        {
            card_reveal_pw_text.Text = pb_password.Password;
        }

        //Was passiert, wenn man unten auf "Passwort vergessen" klickt
        private void click_resetpassword(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Diese Funktion wurde noch nicht implementiert\nFalls sie ihr Passwort vergessen haben, kontaktieren sie bitte einen Entwickler!", "Noch nicht implementierte Funktion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        //Öffnen eines separaten Registrationsfensters
        private void click_registeruser(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Diese Funktion wurde noch nicht implementiert\nBitte kontaktieren sie den Entwickler", "Noch nicht implementierte Funktion", MessageBoxButton.OK, MessageBoxImage.Information);
            Registration r = new Registration();
            this.Close();
            r.Show();
        }

        #endregion

        #region Methoden

        //Methode die die Eingaben überprüft und durch Database Klasse den User anmeldet
        private void login()
        {
            currentUser = database.GetUser(tb_username.Text.Trim());

                if(currentUser != null && currentUser.password == User.sha256(pb_password.Password))
                {
                    this.Close();
                    MessageBox.Show("Herzlich Wilkommen, " + currentUser.username + "!");
                }
                else
                {
                    tb_notification.Foreground = Brushes.Red;
                    tb_notification.Text = "Username oder Passwort falsch";
                }
            
        }

        #endregion

    }

}
