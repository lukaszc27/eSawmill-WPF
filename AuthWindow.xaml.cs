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

namespace eSawmill_WPF
{
    /// <summary>
    /// Logika interakcji dla klasy AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void createCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            CreateCompanyWindow createCompany = new CreateCompanyWindow();
            createCompany.Show();
        }
    }
}
