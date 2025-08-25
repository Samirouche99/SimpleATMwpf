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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleBank;

namespace SimpleATMwpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bank theBank;
        public MainWindow()
        {
            theBank = Bank.Instance;
            theBank.populate();
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {//txtname.Text = "";
            Customer c = theBank.findCustomer(txtname.Text);
            
            if(c == null)
            {
                //no such customer
                MessageBox.Show("Error-"+ txtname.Text + " does not have credit account");
            }
            else
            {
                CustomerWindow cWindow = new CustomerWindow(c);
                cWindow.Owner = this;
                this.Hide();
                cWindow.Show();
            }
        }

        private void bntAddCus_Click(object sender, RoutedEventArgs e)
        {
            newCustomer cWindow = new newCustomer();

            cWindow.Owner = this;
            this.Hide();
            cWindow.Show();
        }
    }
}
