using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for newCustomer.xaml
    /// </summary>
    public partial class newCustomer : Window
    {
        private Bank thebank;
        public Customer cus { get; set; }
        private string custName;
        private string custStreet;
        private string custTown;
        private string custPostcode;

        public newCustomer()
        {
            thebank = Bank.Instance;
            InitializeComponent();
            
        }
     
        public void getDetails()
        {
            custName = txtAddName.Text;
            custStreet = txtAddStreet.Text.ToString();
            custTown = txtAddTown.Text.ToString();
            custPostcode = txtAddPostcode.Text.ToString();

            string strout = "Name: " + custName + " \nAddress: " + custTown + " " + custStreet + " " + custPostcode;

            txtOutput.Text = strout;
        }

        public void createNew()
        {
            Customer add = new Customer(custName);
            thebank.addCustomer(add);

            add.setAddress("123", "Hamilton", "Whisleberry");

            add.createCredit(500, 100);
            
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            getDetails();
            btnAdd.Visibility = Visibility.Hidden;
            btnVerifyYes.Visibility = Visibility.Visible;
            btnVerifyNo.Visibility = Visibility.Visible;
            txtMessage.Visibility = Visibility.Visible;
        }

        private void btnVerifyYes_Click(object sender, RoutedEventArgs e)
        {
            if (thebank.findCustomer(custName) != null)
            {
                MessageBox.Show("customer allready exists");
                this.Close();
            }
            else
            {
                try
                {
                    
                    if (custName == "")
                        throw new Exception("Details missing");
                    
                    Customer cus = new Customer(custName);
                    cus.setAddress(custStreet, custTown , custPostcode);
                    thebank.addCustomer(cus);
                    cus.createCredit(0, 0);
                    MainWindow owner = (MainWindow)this.Owner;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("invalid details\n" + ex.Message);
                }
            }


            this.Close();
                
            this.Owner.Show();
            
            
        }

        private void btnVerifyNo_Click(object sender, RoutedEventArgs e)
        {
            btnAdd.Visibility = Visibility.Visible;
            btnVerifyYes.Visibility = Visibility.Hidden;
            btnVerifyNo.Visibility = Visibility.Hidden;
            txtMessage.Visibility = Visibility.Hidden;
            txtOutput.Text = "";

        }
    }
}
