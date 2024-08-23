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

namespace App_DongBo
{
    /// <summary>
    /// Interaction logic for Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
        }

        private void passwordBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if(passwordBox.Password == "DongBoChain")
                {
                    this.DialogResult = true;
                    Close();
                }
                else
                {
                    this.DialogResult = false;
                    MessageBox.Show("Nhập sai mật khẩu!");
                    Close();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == "DongBoChain")
            {
                this.DialogResult = true;
                Close();
            }
            else
            {
                this.DialogResult = false;
                MessageBox.Show("Nhập sai mật khẩu!");
                Close();
            }
        }
    }
}
