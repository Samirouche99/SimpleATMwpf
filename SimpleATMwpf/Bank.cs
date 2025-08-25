using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using static SimpleBank.Customer;

namespace SimpleBank
{
    class Bank
    {
        private static Bank instance;
        private List<Customer> customers;

        public Bank()
        {
            customers = new List<Customer>();
        }

        public static Bank Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Bank();
                }
                   
                return instance;
            }
        }
        public void populate()
        {
            Customer fred = new Customer("Fred");
            fred.createCredit(500, 100);
            fred.CreditAcc.debit(300);
            fred.CreditAcc.debit(200);
            fred.CreditAcc.credit(100);

            Customer sam = new Customer("Sam");
            sam.createCredit(700, 10);
            sam.CreditAcc.debit(300);
            sam.CreditAcc.debit(150);
            sam.CreditAcc.credit(100);

            Customer tim = new Customer("Tim");
            tim.createCredit(200, 10);
            tim.CreditAcc.debit(100);
            tim.CreditAcc.credit(150);
            tim.CreditAcc.credit(100);

            addCustomer(sam);
            addCustomer(tim);
            addCustomer(fred);
        }
        public void addCustomer(Customer cus)
        {
          //  Customer customer = new Customer(cus);
            customers.Add(cus);
        }
        public string listCustomers()
        {
            string strout = "";
            foreach (Customer cus in customers)
            {
                strout += cus + "\t";
            }
            return strout;
        }

        public Customer findCustomer(string sname)
        {
            foreach (Customer cus in customers)
            {
                if (sname == cus.Name)
                {
                    //Console.WriteLine("Customer: " + cus.Name + " FOUND");
                    return cus; 
                }
            }
          //  Console.WriteLine("Sorry - Cannot find customer");
            return null;
        }

    }

}

