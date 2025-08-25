using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People;

namespace SimpleBank
{


    public class Customer : Person
    {
        private CreditAccount creditAcc;
        private DepositAccount depositAcc;

        //add in public properties 
        public CreditAccount CreditAcc
        {
        
            get
            {
                return creditAcc;
            }
        }
        public DepositAccount DepositAcc
        {
            get
            {
                return depositAcc;
            }
        }

        public Customer Cus { get; }

        //Constructor
        public Customer(string name)
            : base(name)
        {
            creditAcc = null;
            depositAcc = null;
        }

        public Customer(Customer cus)
        {
            Cus = cus;
        }

        public void createCredit(decimal b, decimal od)
        {
            creditAcc = new CreditAccount(b, od);
        }

        public void createDeposit(decimal b, double rate)
        {
            depositAcc = new DepositAccount(b, rate);
        }



    }
}