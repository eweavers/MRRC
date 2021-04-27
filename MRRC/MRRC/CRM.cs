using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MRRC
{
    public class CRM
    {
        private List<Customer> customers = new List<Customer>();
        private string crmFile;

        // If there is no CRM file at the specified location, this constructor constructors an
        // empty CRM with no customers. Otherwise it loads the customers from the specified file
        // (see the assignment specification).
        public CRM(string crmFile)
        {
            if (File.Exists(crmFile))
            {
                this.crmFile = crmFile;
                LoadFromFile();
            }

            else
            {
                this.crmFile = @"..\..\..\Data\customers.csv";
                customers = new List<Customer>();
            }

        }

        //This checks the ID for duplication and if there is it will +1 to it so its unique
        public int CustomerCount(int ID)
        {
            int counter = ID;
            foreach (Customer item in customers)
            {
                if (item.customerID > counter)
                {
                    counter = item.customerID;
                }

            }
            return counter + 1;
        }

        // This method adds the provided customer to the customer list if the customer ID doesn’t
        // already exist in the CRM. It returns true if the addition was successful (the customer
        // ID didn’t already exist in the CRM) and false otherwise.
        public bool AddCustomer(Customer customer)
        {

            int counter = 0;
            foreach (Customer item in customers)
            {
                if (item.customerID == customer.customerID)
                {
                    counter++;
                }
            }
            if (counter > 0)
            {
                return false;
            }
            else
            {
                customers.Add(customer);
                return true;
            }
        }

        // This method removes the customer with the provided customer ID from the CRM if they
        // are not currently renting a vehicle. It returns true if the removal was successful,
        // otherwise it returns false.
        public bool RemoveCustomer(int ID, Fleet fleet)
        {

            if (fleet.IsRenting(ID))
            {
                return false;

            }
            else
            {
                foreach (Customer item in customers)
                {

                    if (item.customerID == ID)
                    {
                        customers.Remove(item);
                        return true;
                    }

                }
                return false;
            }
        }
        // This method returns the list of current customers.
        public List<Customer> GetCustomers()
        {
            //LoadFromFile();

            return customers;

        }

        //displays the customer with a table format
        public string displaycustomer()
        {
            List<Customer> customers = GetCustomers();
            string table = "";

            table += ("----------------------------------------\n");
            foreach (Customer C in customers)
            {
                table += C.toTable() + "\n";
            }
            table += ("---------------------------------------\n");

            return table;
        }
        // This method returns the customer who matches the provided ID.
        public Customer GetCustomer(int ID)
        {
            //LoadFromFile();
            foreach (Customer item in customers)
            {
                if (item.customerID == ID)
                {
                    return item;
                }

            }
            return null;

        }

        //this method collects the input for the choice and the ID they entered and validates the input and checks it the
        //existing customer and manually changes what they asked to be changed
        public bool GetCustomerEdit(int ID, int choice)
        {
            Customer customer = GetCustomer(ID);
            if (customer == null)
            {
                Console.WriteLine("The entered ID is not valid or doesn't exist\n");
                return false;
            }

            bool validcheck = false;
            if (choice == 1)
            {
                Console.WriteLine("ID Exists\n\n Modify CustomerID: ");
                string newcustomerID = Console.ReadLine();
                int newID = 0;

                if (int.TryParse(newcustomerID, out newID))
                {
                    foreach (Customer item in customers)
                    {
                        if (item.customerID == ID)
                        {
                            item.customerID = CustomerCount(ID);
                            Console.WriteLine("\nSuccess\n");
                            return true;
                        }
                    }
                }
                else
                {
                    validcheck = false;

                }
            }

            if (choice == 2)
            {
                Console.WriteLine("ID Exists\n\n Modify Title: ");
                string newtitle = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(newtitle))
                {
                    foreach (Customer item in customers)
                    {
                        if (item.customerID == ID)
                        {
                            item.title = newtitle;
                            Console.WriteLine("\nSuccess\n");

                            return true;
                        }
                    }
                }
                else
                {
                    validcheck = false;
                }

            }
            if (choice == 3)
            {
                Console.WriteLine("ID Exists\n\n Modify First Name: ");
                string newfirstname = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(newfirstname))
                {
                    foreach (Customer item in customers)
                    {
                        if (item.customerID == ID)
                        {
                            item.firstName = newfirstname;
                            Console.WriteLine("\nSuccess\n");

                            return true;
                        }
                    }
                }
                else
                {
                    validcheck = false;
                }
            }
            if (choice == 4)
            {
                Console.WriteLine("ID Exists\n\n Modify Last Name: ");
                string newlastname = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(newlastname))
                {
                    foreach (Customer item in customers)
                    {
                        if (item.customerID == ID)
                        {
                            item.title = newlastname;
                            Console.WriteLine("Success\n");

                            return true;
                        }
                    }
                }
                else
                {
                    validcheck = false;
                }
            }
            if (choice == 5)
            {
                Console.WriteLine("ID Exists\n\n Modify Gender: ");
                string newgender = Console.ReadLine();
                bool genderset = false;

                if (!String.IsNullOrWhiteSpace(newgender))
                {
                    // checks if the string is male and is case insenstive if it is set the variable to the input else is incorrect
                    //found https://stackoverflow.com/questions/3121957/how-can-i-do-a-case-insensitive-string-comparison
                    if (String.Equals(newgender, "male", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (Customer item in customers)
                        {
                            if (item.customerID == ID)
                            {
                                item.gender = (Gender)Enum.Parse(typeof(Gender), "Male");
                                genderset = true;
                                Console.WriteLine("\nSuccess\n");


                                return true;
                            }
                        }
                    }

                    if (String.Equals(newgender, "female", StringComparison.OrdinalIgnoreCase) && genderset == false)
                    {
                        foreach (Customer item in customers)
                        {
                            if (item.customerID == ID)
                            {
                                item.gender = (Gender)Enum.Parse(typeof(Gender), "Female");
                                genderset = true;
                                Console.WriteLine("\nSuccess\n");


                                return true;
                            }
                        }

                    }

                    if (String.Equals(newgender, "other", StringComparison.OrdinalIgnoreCase) && genderset == false)
                    {
                        foreach (Customer item in customers)
                        {
                            if (item.customerID == ID)
                            {
                                item.gender = (Gender)Enum.Parse(typeof(Gender), "Other");
                                genderset = true;
                                Console.WriteLine("\nSuccess\n");


                                return true;
                            }
                        }

                    }

                }
            }
            if (choice == 6)
            {
                Console.WriteLine("ID Exists\n\n Modify DOB: ");
                string newdob = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(newdob))
                {
                    DateTime tempDate;
                    if (DateTime.TryParse(newdob, out tempDate))
                    {
                        foreach (Customer item in customers)
                        {
                            if (item.customerID == ID)
                            {
                                String.Format("{0:d/MM/yyyy}", tempDate);
                                item.DOB = tempDate;
                                Console.WriteLine("\nSuccess\n");

                                return true;
                            }
                        }

                    }
                }
            }
            if (validcheck == false)
            {
                Console.WriteLine("\nFail\n");
                return false;
            }
            return true;
        }
    
        

        // This method saves the current state of the CRM to file.
        public void SaveToFile()
        {
            //loop through customers list and foreach do the writeline
 
            using (StreamWriter writer = new StreamWriter(crmFile))
            {
                writer.Write("{0},{1},{2},{3},{4},{5}", "CustomerID", "Title", "FirstName", "LastName", "Gender", "DOB\n");
                foreach (Customer item in customers)
                {
                    writer.WriteLine(item.ToCSVString());
                }
                writer.Close();
            }
            

        }
        // This method loads the state of the CRM from file.
        public void LoadFromFile()
        {
            using (StreamReader reader = new StreamReader(crmFile))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    string[] tokens = line.Split(',');
                    int ID = int.Parse(tokens[0]);
                    string title = tokens[1];
                    string firstName = tokens[2];
                    string lastName = tokens[3];
                   
                    Gender gender = (Gender) Enum.Parse(typeof(Gender), tokens[4]);
                    DateTime DOB = DateTime.Parse(tokens[5]);

                    customers.Add(new Customer(ID, title, firstName, lastName, gender, DOB));
                }
            }
        }
    }
}
