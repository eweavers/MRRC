using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRC
{
    public enum Gender
    {
        Male,
        Female,
        Other
    };
    public class Customer
    {
        public int customerID;
        public string title;
        public string firstName;
        public string lastName;
        public DateTime DOB;
        public Gender gender;
       

        // This constructor should construct a customer with the provided attributes.
        public Customer(int ID, string title, string firstName, string lastName, Gender gender,
         DateTime DOB)
        {
            customerID = ID;
            this.title = title;
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.DOB = DOB;

        }
        //This method puts the info into a table format
        public string toTable()
        {
            string table = String.Format("{0,-10}   |   {1,-10}   |   {2,-10}   |   {3,-10}   |   {4, 10}   |   {5, 10}   |", customerID, title, firstName, lastName, gender, DOB);
            return table;
        }
// This method should return a CSV representation of the customer that is consistent
// with the provided data files.
        public string ToCSVString()
        {
         return customerID + "," + title +"," +firstName +"," + lastName +"," + gender + "," + DOB.ToString("dd/MM/yyyy");

        }
// This method should return a string representation of the attributes of the customer.
        /*
        public override string ToString()
        {

        }*/

    }
}
