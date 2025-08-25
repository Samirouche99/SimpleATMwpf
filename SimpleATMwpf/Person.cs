using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace People
{
    
    public class Person
    {
        //instance variables (attributes)
        string name;
        int yearOfBirth;
        Address address;

        //Accessor methods foe name and yearOfBirth
        //adds public properties that link to private fields
        public string Name //Name property
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public int Year //Year property
        {
            get
            {
                return yearOfBirth;
            }
            set
            {
                yearOfBirth = value;
            }
        }
        

        /********************************************
     * getter accessor for address - 
     * note no setter accessor
     * The property is read only
     * *****************************************/
        public Address Address
        {
            get
            {
                return address;
            }
        }


        

        /****************************************
 * setter method for address
 ***************************************/
        public void setAddress(string strst, string strtwn, string
       strpc)
        {
            address.Street = strst;
            address.Town = strtwn;
            address.Postcode = strpc;
        }

        public Person()
        {
            address = new Address();
            setAddress("unkown", "unknown", "unknown");
            Year = 0;
            Name = "no name";
        }

        public Person(String strn)
        {
            address = new Address();
            setAddress("unkown", "unknown", "unknown");
            Year = 0;
            Name = strn;               
        }

        public Person(String strn, int yr)
        {
            address = new Address();
            setAddress("unkown", "unknown", "unknown");
            Year = yr;
            Name = strn;           
        }

        public Person(String strn, string strst, string strtwn, string
        strpc, int yr)
        {
            address = new Address();
            setAddress(strst, strtwn, strpc);
            Year = yr;
            Name = strn;   
        }

        //operations 
        public override string ToString() 
        {
            string strout = Name + "\t" + Year;
            strout = strout + address;
            return strout; 
        }
    }
}
