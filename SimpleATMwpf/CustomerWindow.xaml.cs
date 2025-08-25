using System;
using System.Collections.Generic;
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
using SimpleBank;


namespace SimpleATMwpf
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        //customer logged in
        private Customer cus;
        //Constructer now takes reference to customer
        public CustomerWindow(Customer c)
        {
            cus = c;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this.Owner.Show();
        }

        private void setStandardState()
        {
            txtOutput.Text = "";
            btn10.Visibility = Visibility.Hidden;
            btn20.Visibility = Visibility.Hidden;

            btn30.Visibility = Visibility.Hidden;

            btn40.Visibility = Visibility.Hidden;

            btn50.Visibility = Visibility.Hidden;
            btn100.Visibility = Visibility.Hidden;
            btnBalance.Visibility = Visibility.Visible;
            btnCash.Visibility = Visibility.Visible;
            btnMiniStmnt.Visibility = Visibility.Visible;

        }

        private void setCashState()
        {
            txtOutput.Text = "";
            btn10.Visibility = Visibility.Visible;
            btn20.Visibility = Visibility.Visible;
            btn30.Visibility = Visibility.Visible;
            btn40.Visibility = Visibility.Visible;
            btn50.Visibility = Visibility.Visible;
            btn100.Visibility = Visibility.Visible;
            btnBalance.Visibility = Visibility.Hidden;
            btnCash.Visibility = Visibility.Hidden;
            btnMiniStmnt.Visibility = Visibility.Hidden;


        }

        private void btnBalance_Click(object sender, RoutedEventArgs e)

        {
           
            string strout = string.Format("Balance :{0:c}", cus.CreditAcc.Balance);
            txtOutput.Text = strout;
        }

        private void btnCash_Click(object sender, RoutedEventArgs e)
        {
            setCashState();
        }

        private async void withdrawCash(object sender, RoutedEventArgs e)
        {
            Button caller = (Button)sender;
            string stramount = caller.Content.ToString();
            stramount = stramount.TrimStart('£');
            decimal amount = decimal.Parse(stramount);
            try
            {
                
                cus.CreditAcc.debit(amount);
                txtOutput.Text = "Please take your money";
            }
            catch(Exception ex)
            {
                txtOutput.Text = "transaction cancelled \n" + ex.Message;
            }


            await Task.Delay(2000);
            setStandardState();
        }

        private void btnMiniStmnt_Click(object sender, RoutedEventArgs e)
        {
            string strout = cus.CreditAcc.getStatement();
            txtOutput.Text = strout;
        }
    }
}
