using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace AsyncWeb.UserInterface {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e) {
            using var client = new HttpClient();
            // Bad code on the client side using .Result, as it blocks the thread, when you start this the UI becomes frozen for 10 sec
           
            // Good code, now ui doesn't freeze
            var response = await client.GetAsync("http://localhost:5000/");
            var result = await response.Content.ReadAsStringAsync();
            MessageBox.Show(result);
        }
    }
}
