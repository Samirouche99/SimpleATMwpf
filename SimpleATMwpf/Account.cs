using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Transactions;
using System.Xml.Linq;

namespace SimpleBank
{
    public abstract class Account
    {
        //class variables
        private static long num = 0;
        private const string FILE_NAME = "dummyprinter.txt";

        //instance variables
        Stack<Transaction> transactions;
        private long number;
        private decimal balance;
        private decimal chkFunds;



        public string getStatement()
        {



            transactions.Reverse();



            string strout = "Account: " + this.ToString() + "\n";
            int count = 0;
            foreach (Transaction t in transactions)
            {
                if (count == 5)
                {
                    using (StreamWriter sw = File.CreateText(FILE_NAME))
                    {
                        sw.WriteLine(strout = strout + t + "\n");

                        sw.Close();

                    }
                    break;
                }
               
                strout = strout + t + "\n";
                count++;
            }
            transactions.Reverse();

            return strout;
        }

        public string getMonthlyStatement()
        {
            DateTime currentDate = DateTime.Now;
            int currentMonth = currentDate.Month;


            string monthName;

            switch (currentMonth)
            {
                case 1:
                    monthName = "January";
                    break;
                case 2:
                    monthName = "February";
                    break;
                case 3:
                    monthName = "March";
                    break;
                case 4:
                    monthName = "April";
                    break;
                case 5:
                    monthName = "May";
                    break;
                case 6:
                    monthName = "June";
                    break;
                case 7:
                    monthName = "July";
                    break;
                case 8:
                    monthName = "August";
                    break;
                case 9:
                    monthName = "September";
                    break;
                case 10:
                    monthName = "October";
                    break;
                case 11:
                    monthName = "November";
                    break;
                case 12:
                    monthName = "December";
                    break;
                default:
                    monthName = "Invalid Month";
                    break;
            }


            Console.WriteLine("The current month is: " + monthName);
            transactions.Reverse();
            string strout = "Account: " + this.ToString() + "\n";
            int count = 0;
            foreach (Transaction t in transactions)
            {
                if (t.Date.Month != currentMonth)
                {
                    break;
                }
                strout = strout + t + "\n";
                count++;
            }
            transactions.Reverse();

            return strout;
        }
        //Read only mehtod
        public long Number
        {
            get
            {
                return number;
            }
        }//End Getter

        public decimal Balance
        {
            get
            {
                return balance;
            }
        }//End Getter

        public decimal ChkFunds
        {
            get
            {
                return chkFunds;
            }
        }
        public Account()
        {
            transactions = new Stack<Transaction>();
            balance = 0;
            number = ++num;
        }//End Account Constructor 
        public Account(decimal amount)
        {
            transactions = new Stack<Transaction>();
            balance = amount;
        }//End Account Constructor

        public override string ToString()
        {
            string accout;
            accout = "Balance: £" + balance;
            return accout;
        }//End Override 

        public void credit(decimal amount)
        {
            balance = balance + amount;
            transactions.Push(new Transaction.CreditTransaction(balance, amount));


        }//End creidit Method

        virtual public void debit(decimal amount)
        {

            balance = balance - amount;
            transactions.Push(new Transaction.DebitTransaction(balance, amount));

        }//End debit method


    }//End Abstract class Account

    //CreditAccount class, concrete derived
    public class CreditAccount : Account
    {


        //instance variables
        private decimal ODLimit;


        //read only property called Limit that returns ODLimit
        public decimal Limit
        {
            get
            {
                return ODLimit;
            }
        }
        //CONSTRUCTORS
        public CreditAccount(decimal amount) : base(amount)
        {
            //the :base(amount) calls parent constructor
            //passes on parameter amount
            //so only need to initialise additional instance variables
            ODLimit = 100;
        }

        public CreditAccount() : base()
        {
            ODLimit = 100;

        }

        public CreditAccount(decimal amount, decimal limit) : base(amount)
        {

            ODLimit = limit;

        }



        //METHODS
        public override string ToString()
        {
            string accout;
            accout = "Credit Account \n" + base.ToString() + "\nOD Limit £" + ODLimit;
            return accout;
        }

        public override void debit(decimal amount)
        {
            decimal chkFunds = Balance + ODLimit;

            //test if sufficient funds – remember overdraft!
            if (amount > chkFunds)
            {
                Console.WriteLine("Insufficient funds - Transaction Cancelled");
            }
            else
            {
                base.debit(amount);
            }

        }


    }//End concrete class

    public class DepositAccount : Account
    {
        //instance variables 

        private double rate;
        //properties

        public double Rate
        {
            get
            {
                return rate;
            }
            set
            {
                Rate = this.rate;
            }
        }

        //constructors
        public DepositAccount(decimal amount) : base(amount)
        {
            //the :basr(amount) calls parent constructor 
            //passes on parameter amount
            //so only need to initialise additional instance variables
            rate = 0.0;
        }
        public DepositAccount() : base()
        {
            //the :basr(amount) calls parent constructor 
            //passes on parameter amount
            //so only need to initialise additional instance variables
            rate = 0.0;
        }

        public DepositAccount(decimal amount, double rt) : base(amount)
        {
            //the :basr(amount) calls parent constructor 
            //passes on parameter amount
            //so only need to initialise additional instance variables
            rate = 0.0;
        }

        public override string ToString()
        {
            string strout;
            strout = "Deposit Account\n" + base.ToString();
            strout = strout + string.Format("\n Rate:{0:f2}", rate);
            return strout;
        }

        public void Debit(decimal amount)
        {
            if (amount > Balance)
            {
                MessageBox.Show("Invalid Amount\n");
            }

      
            else
            {
                base.debit(amount);
            }
        }

        public void addInterest()
        {
            // Balance = Balance * ((decimal)rate) / 100;
            credit(Balance * ((decimal)rate) / 100);

        }
    }

    [Serializable]
    public abstract class Transaction
    {

        private DateTime date;
        private decimal amount;
        private decimal cbalance;

        /***********
         * add in public get properties
         * *********/

        public decimal Amount
        {

            get
            {
                return amount;
            }
        }
        public decimal Balance
        {
            get
            {
                return cbalance;
            }
        }
        public DateTime Date
        {
            get { return date; }
        }
        public Transaction(decimal b, decimal a)
        {
            cbalance = b;
            amount = a;

            date = DateTime.Now; //sets date and time 
        }

        public override string ToString()
        {
            string strout = date.ToShortDateString();
            strout = strout + "\t"+ date.ToShortTimeString();

            return strout; 
        }

        [Serializable]
        public class CreditTransaction : Transaction
        {
            public CreditTransaction(decimal b, decimal a)
                 : base(b, a)
            {
                //note how simply uses base
            }




            public override string ToString()
            {
                string strout;
                strout = base.ToString();
                strout = strout + string.Format("\t\t{0:c}\t{1:c}", Amount, Balance);
                return strout;
            }
        }

        [Serializable]
        public class DebitTransaction : Transaction
        {


            public DebitTransaction(decimal b, decimal a)
                 : base(b, a)
            {
                //note how simply uses base
            }

            public override string ToString()
            {
                string strout = base.ToString();

                strout = strout + string.Format("\t{0:c}\t\t{1:c}", Amount, Balance);
                return strout;
            }
        }
    }

}



