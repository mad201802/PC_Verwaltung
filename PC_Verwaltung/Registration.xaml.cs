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

namespace PC_Verwaltung
{
    /// <summary>
    /// Interaktionslogik für Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        /*
        public string name { get; private set; }
        public string surname { get; private set; }
        public string email { get; private set; }
        public string password { get; private set; }
        public string passwordconfirm { get; private set; }
        */

        public Registration()
        {
            InitializeComponent();
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

        private void register()
        {
            
        }
    }
}
