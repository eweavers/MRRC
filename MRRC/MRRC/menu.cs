using MRRC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MRRC
{

    /*public enum Gender
    {
        Male,
        Female,
        Other
    };*/


    public class Menu
    {
        public static Fleet fleeet = new Fleet(@"..\..\..\Data\fleet.csv", @"..\..\..\Data\rentals.csv");
        public static CRM crmm = new CRM(@"..\..\..\Data\customers.csv");
        const int NUM_OPTIONS1 = 3;
        const int NUM_OPTIONS2 = 4;
        const int NUM_OPTIONS3 = 2;
        const int NUM_OPTIONS4 = 6;
        public static string s2;

        //self explanatory
        public static void WelcomeMessage()
        {

            Console.WriteLine("### Mates-Rates Rent-a-Car Operation Menu ###\n");
            Console.WriteLine("You may press the ESC key at any time to exit. Press BACKSPACE key to return to the previous menu.\n");
        }

        //self explanatory
        public static void ExitProgram()
        {
            Console.Write("\n\nPress enter to exit.");
            Console.ReadLine();
            crmm.SaveToFile();
            fleeet.SaveVehiclesToFile();
            fleeet.SaveRentalsToFile();
            System.Environment.Exit(0);
        }

        //simply asks for an id to be input
        public static void GetCustomerContents()
        {
            Console.WriteLine("Please enter an ID");
            GetCustomer();
        }

        //This displays the single customer that the user wanted to find
        public static void GetCustomer()
        {

            int ID;
            string inputID = Console.ReadLine();


            if (int.TryParse(inputID, out ID))
            {
                Customer c = crmm.GetCustomer(ID);
                if (c != null)
                {

                    Console.WriteLine("\n{0} {1} {2} {3} {4} {5}\n", c.customerID, c.title, c.firstName, c.lastName, c.gender, c.DOB);

                }
                else
                {
                    Console.WriteLine("The ID you have entered doesnt exist");
                    GetCustomerContents();
                }
            }
            else
            {
                Console.WriteLine("The ID you have entered isnt a number");
                GetCustomerContents();
            }

        }

        //This is a menu displaying options for the user
        public static void EditCustomerMenu()
        {
            Console.WriteLine("What category would you like to edit\n");
            Console.WriteLine("1) CustomerID");
            Console.WriteLine("2) Title");
            Console.WriteLine("3) First Name");
            Console.WriteLine("4) Last Name");
            Console.WriteLine("5) Gender");
            Console.WriteLine("6) DOB\n");

        }
        //string registration, VehicleGrade grade, string make, string model,
        ///int year, int numSeats, TransmissionType transmission, FuelType fuel,
        //bool GPS, bool sunRoof, double dailyRate, string colour
        public static void EditVehicleMenu()
        {
            Console.WriteLine("What category would you like to edit\n");
            Console.WriteLine("1) Registration");
            Console.WriteLine("2) Grade");
            Console.WriteLine("3) Make");
            Console.WriteLine("4) Model");
            Console.WriteLine("5) Year");
            Console.WriteLine("6) Number of Seats");
            Console.WriteLine("7) Transmission Type");
            Console.WriteLine("8) Fuel Type");
            Console.WriteLine("9) GPS");
            Console.WriteLine("10) Sun Roof");
            Console.WriteLine("11) Daily Rate");
            Console.WriteLine("12) Colour");

        }

        public static void EditVehicleMenuReceiveID(int choice)
        {

            Console.WriteLine("Enter a Vehicle Registration: \n");
            int choice1 = choice;
            string tempregistration = Console.ReadLine();
            tempregistration = tempregistration.ToUpper();

            if (!String.IsNullOrWhiteSpace(tempregistration))
            {
                if (fleeet.AddVehicleRegoCheck1(tempregistration))
                {
                    fleeet.GetVehicleEdit(tempregistration, choice);

                }
                else
                {
                    Console.WriteLine("Rego doesnt exist or not valid input type must be 3 numbers and 3 letters (123ABC)");
                    EditVehicleMenuReceiveID(choice1);
                }
            }
            else
            {
                Console.WriteLine("Cannot be empty");
                EditVehicleMenuReceiveID(choice1);
            }
        }

        //this method collects the choice that they made checks to see if there input is an int
        public static void EditCustomerMenuReceiveID(int choice)
        {

            Console.WriteLine("Enter a customer ID: \n");
            int choice1 = choice;
            string tempID = Console.ReadLine();
            int ID = 0;


            if (int.TryParse(tempID, out ID))
            {
                crmm.GetCustomerEdit(ID, choice1);
            }
            else
            {
                Console.WriteLine("Not a valid input type must be an integer");
                EditCustomerMenuReceiveID(choice1);
            }
        }

        //This method checks the customer ID method and displays if the removal was successfull or not
        public static void removeCustomer()
        {
            Console.WriteLine("Enter a customer ID: \n");
            string tempID = Console.ReadLine();
            int ID;

            if (int.TryParse(tempID, out ID))
            {
                if(crmm.RemoveCustomer(ID, fleeet))
                {
                    Console.WriteLine("\nSuccess\n");
                }
                else
                {
                    Console.WriteLine("\nFailed\n");

                }
            }
            else
            {
                Console.WriteLine("Not a valid input type must be an integer");
            }

        }

        public static void removeVehicle()
        {
            Console.WriteLine("Enter a Vehicle Registration: \n");
            string tempRego = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(tempRego))
            {
                {
                    if (fleeet.RemoveVehicle(tempRego))
                    {
                        Console.WriteLine("\nSuccess\n");
                    }
                    else
                    {
                        Console.WriteLine("\nFailed\n");

                    }
                }
            }
            else
            {
                Console.WriteLine("Cannot be empty");
            }

        }
        //This validates the input to check if they input a correct number and to check if they hit backspace or escape
        public static int ReadOption3()
        {
            ConsoleKeyInfo choice;
            int choice1;
            bool okayChoice;

            do
            {
                choice = Console.ReadKey(true);


                if (choice.Key == ConsoleKey.Escape)
                {
                    ExitProgram();
                    okayChoice = true;
                }
                if (choice.Key == ConsoleKey.Backspace)
                {
                    choice1 = 11;
                    okayChoice = true;
                    return choice1;
                }
                if (char.IsDigit(choice.KeyChar))
                {
                    choice1 = int.Parse(choice.KeyChar.ToString());
                    okayChoice = true;
                }
                else
                {
                    choice1 = -1;
                    okayChoice = false;
                }
                //Console.WriteLine(choice);
                if (okayChoice == false || choice1 < 0 || choice1 > NUM_OPTIONS3)
                {
                    okayChoice = false;
                    Console.WriteLine("You did not enter a correct option.\n\n \tPlease try again\n");
                    MainMenu();
                }
            } while (okayChoice == false);

            return choice1;
        }

        public static int ReadOption5()
        {
            ConsoleKeyInfo choice;
            int choice1;
            bool okayChoice;

            do
            {
                choice = Console.ReadKey(true);


                if (choice.Key == ConsoleKey.Escape)
                {
                    ExitProgram();
                    okayChoice = true;
                }
                if (choice.Key == ConsoleKey.Backspace)
                {
                    choice1 = 11;
                    okayChoice = true;
                    return choice1;
                }
                if (char.IsDigit(choice.KeyChar))
                {
                    choice1 = int.Parse(choice.KeyChar.ToString());
                    okayChoice = true;
                }
                else
                {
                    choice1 = -1;
                    okayChoice = false;
                }
                //Console.WriteLine(choice);
                if (okayChoice == false || choice1 < 0 || choice1 > NUM_OPTIONS1)
                {
                    okayChoice = false;
                    Console.WriteLine("You did not enter a correct option.\n\n \tPlease try again\n");
                    MainMenu();
                }
            } while (okayChoice == false);

            return choice1;
        }

        //This validates the input to check if they input a correct number and to check if they hit backspace or escape
        public static int ReadOption()
        {
            ConsoleKeyInfo choice;
            int choice1;
            bool okayChoice;

            do
            {
                choice = Console.ReadKey(true);


                if (choice.Key == ConsoleKey.Escape)
                {
                    ExitProgram();
                    okayChoice = true;
                }

                if (char.IsDigit(choice.KeyChar))
                {
                    choice1 = int.Parse(choice.KeyChar.ToString());
                    okayChoice = true;
                }
                else
                {
                    choice1 = -1;
                    okayChoice = false;
                }
                //Console.WriteLine(choice);
                if (okayChoice == false || choice1 < 0 || choice1 > NUM_OPTIONS1)
                {
                    okayChoice = false;
                    Console.WriteLine("You did not enter a correct option.\n\n \tPlease try again\n");
                    MainMenu();
                }
            } while (okayChoice == false);

            return choice1;
        }

        //This validates the input to check if they input a correct number and to check if they hit backspace or escape
        public static void EditMenuWarning()
        {
            string menuoptions = "Please enter a number from the options (Backspace and Escape will not work in the next menu):\n" +
                                  "\n" +
                                  "1) Go Back\n" +
                                  "2) Exit\n" +
                                  "3) Go forward";

            Console.WriteLine(menuoptions);
        }

        public static int ReadEditVehicleOption()
        {
            string choice;
            int choice1;
            bool okayChoice;

            do
            {
                choice = Console.ReadLine();
                 
                if (int.TryParse(choice, out choice1))
                {
                    okayChoice = true;
                }
                else
                {
                    choice1 = -1;
                    okayChoice = false;
                }
                //Console.WriteLine(choice);
                if (okayChoice == false || choice1 < 0 || choice1 > 12)
                {
                    okayChoice = false;
                    Console.WriteLine("You did not enter a correct option.\n\n \tPlease try again\n");
                    MainMenu();
                }
            } while (okayChoice == false);

            return choice1;
        }

        public static int ReadEditOption()
        {
            ConsoleKeyInfo choice;
            int choice1;
            bool okayChoice;

            do
            {
                choice = Console.ReadKey(true);

                if (choice.Key == ConsoleKey.Backspace)
                {
                    choice1 = 11;
                    okayChoice = true;
                    return choice1;
                }
                if (choice.Key == ConsoleKey.Escape)
                {
                    ExitProgram();
                    okayChoice = true;
                }
                if (char.IsDigit(choice.KeyChar))
                {
                    choice1 = int.Parse(choice.KeyChar.ToString());
                    okayChoice = true;
                }
                else
                {
                    choice1 = -1;
                    okayChoice = false;
                }
                //Console.WriteLine(choice);
                if (okayChoice == false || choice1 < 0 || choice1 > NUM_OPTIONS4)
                {
                    okayChoice = false;
                    Console.WriteLine("You did not enter a correct option.\n\n \tPlease try again\n");
                    MainMenu();
                }
            } while (okayChoice == false);

            return choice1;
        }

        //This validates the input to check if they input a correct number and to check if they hit backspace or escape
        public static int ReadOption2(int subchoice)
        {
            ConsoleKeyInfo choice;
            int choice1;
            bool okayChoice;

            do
            {
                choice = Console.ReadKey(true);
                if (choice.Key == ConsoleKey.Escape)
                {
                    ExitProgram();
                    okayChoice = true;
                }
                if (choice.Key == ConsoleKey.Backspace)
                {
                    choice1 = 11;
                    okayChoice = true;
                    return choice1;
                }
                if (char.IsDigit(choice.KeyChar))
                {
                    choice1 = int.Parse(choice.KeyChar.ToString());
                    okayChoice = true;
                }
                else
                {
                    choice1 = -1;
                    okayChoice = false;
                }
                //Console.WriteLine(choice);
                if (!okayChoice || choice1 < 0 || choice1 > NUM_OPTIONS2)
                {
                    okayChoice = false;
                    Console.WriteLine("You did not enter a correct option.\n\n \tPlease try again\n");
                    SubMenu(subchoice);
                }

            } while (!okayChoice);

            return choice1;
        }

        public static void MainMenu()
        {
            //shows the intial menu
            string menuoptions1 = "Please enter a number from the options below:\n" +
                                  "\n" +
                                  "1) Customer Management\n" +
                                  "2) Fleet Mangement\n" +
                                  "3) Rental Mangement\n";

            Console.WriteLine(menuoptions1);

        }

        //this prints out the Display of the customers function
        public static void DisplayCustomer()
        {
            Console.WriteLine(crmm.displaycustomer());
        }

        public static void DisplayRentals()
        {
            Console.WriteLine(fleeet.displayrentals());
        }
        public static void Return()
        {
            Console.WriteLine("Enter a vehicle registration");
            string vehiclerego = Console.ReadLine();
            vehiclerego = vehiclerego.ToUpper();

            try
            {
                if (fleeet.AddVehicleRegoCheck1(vehiclerego))
                {
                    if (fleeet.IsRented(vehiclerego))
                    {
                        fleeet.ReturnVehicle(vehiclerego);
                        Console.WriteLine("You have successfully returned a car\n");

                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("The vehicle registration either isnt rented or not valid\n");
            }
        }

        //_________________________________________________  
        //This code is from Shlomo Geva's lecture in week 9
        //__________________________________________________
        public static void search(RPN rpn, Fleet flt)
        {
            fleeet.init_attributes();
            Console.WriteLine("\n------------ Searching ----------");

            Console.Write("\nInfix expression:    ");
            foreach (string token in rpn.Infix)
            {
                Console.Write(token + " ");
            }
            Console.WriteLine();

            Console.Write("Postfix expression:  ");
            foreach (string token in rpn.Postfix)
            {
                Console.Write(token + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
            flt.search(out string[] result, rpn);
            if (result.Length == 0)
            {
                throw new Exception("No match found");
            }
            foreach (string v in result)
            {
                fleeet.displayVehicle(v);
            }

        }

        public static string getquery(out ArrayList query)
        {
            // accept user query and do some validation
            // note that some non-sensical queries can still pass validation
            // for instance,  AND RED (( OR ) BLUE)
            // but these will be caught later 
            query = new ArrayList();
            string queryText = "";
            Console.Write("\nEnter your query, or just enter q to quit or b to go back:  ");
            string temp = Console.ReadLine();
            if(temp == "q")
            {
                query.Add("QUIT");

            }
            if (temp == "b")
            {
                query.Add("BACK");
            }
            if (temp.Length > 0)
            {
                // separate parenthesis before splitting string
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].Equals('(') || temp[i].Equals(')'))
                    {
                        queryText += " ";
                        queryText += temp[i];
                        queryText += " ";
                    }
                    else
                    {
                        queryText += temp[i];
                    }
                    
                }
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //test1 - fail;
                List<string> s1 = new List<string>();
                string[] test = queryText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for(int j = 0; j < test.Length; j++){

                    if (!test[j].Equals("(") && !test[j].Equals(")") && !test[j].Equals("AND") && !test[j].Equals("OR")) //&& j == test.Length - 1)
                    {
                        
                        s1.Add(test[j]);
                        s2 = string.Join(" ", s1);
                        if (j == test.Length - 1)
                        {
                            query.Add(s2);
                            s1.Clear();
                        }
                        
                    }
                    else
                    {
                        if (j == 0)
                        {
                            //s1.Add(test[j]);
                            query.Add(test[j]);
                            s1.Clear();
                        }
                        else
                        {
                            s1.Clear();
                            if (!String.IsNullOrEmpty(s2))
                            {
                                query.Add(s2);
                            }
                            query.Add(test[j]);
                            s1.Clear();
                            s2 = "";
                     
                        }

                    }
                    
                }
                
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //queryText = queryText.ToUpper();
                //query.AddRange(queryText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)); // split to tokens (default delimiter is space)
            }
            else
            {
                query.Add("BLANK");
            }
            return temp;
        }

        public static void blankquery()
        {
            fleeet.blankquery();
        }
        public static void rentcar()
        {
            Console.WriteLine("Enter a vehicle registration: ");
            string vehiclerego = Console.ReadLine();
            vehiclerego = vehiclerego.ToUpper();

            try
            {
                if (fleeet.AddVehicleRegoCheck1(vehiclerego))
                {
                    Console.WriteLine("Enter a customer ID: ");
                    string customerid = Console.ReadLine();
                    int ID;
                    if (int.TryParse(customerid, out ID))
                    {
                        if (fleeet.checkcustomer(ID) == true)
                        {
                            string message = "";
                            throw new AlreadyExistException(message);
                        }
                        Console.WriteLine("How long is he renting it for (days)");
                        string time = Console.ReadLine();
                        int time2;
                        if(int.TryParse(time, out time2))
                        {
                            fleeet.RentCar(vehiclerego, ID);
                            Console.WriteLine("You have successfully rented a car it will cost ${0}\n", fleeet.calccost(vehiclerego, time2));
                        }
                        else
                        {
                            throw new ArgumentException();
                        }
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch(ArgumentException)
            {
                Console.WriteLine("Check if the vehicle rego, customer Id or days entered is valid");
            }
            catch (AlreadyExistException)
            {
                Console.WriteLine("Customer is already renting\n");
            }
            
        }

        //this prints out the displayfleet function
        public static void DisplayFleet()
        {
            Console.WriteLine(fleeet.displayfleet());


        }

        //This the validation and the gateway between the menu to the actual adding of them to the CRM for customers
        //manually inputting all values
        public static void NewCustomer()
        {
            string title = "";
            string firstName = "";
            string lastName = "";
            Gender gender = (Gender)Enum.Parse(typeof(Gender), "Other");
            DateTime DOB = Convert.ToDateTime("01/01/2001");

            //comment here
            Console.Write("Please fill the following fields (fields marked with * are required)\n\n" +
                                "Title*: ");
            string temp1 = Console.ReadLine();
            bool ready = false;
            if (!String.IsNullOrWhiteSpace(temp1))
            {
                title = temp1;
            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewCustomer();
            }
            Console.Write("FirstName*: ");
            string temp2 = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(temp2))
            {
                firstName = temp2;
            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewCustomer();
            }

            Console.Write("LastName*: ");
            string temp3 = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(temp3))
            {
                lastName = temp3;
            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewCustomer();
            }

            Console.Write("Gender*: ");
            string temp4 = Console.ReadLine();
            bool genderset = false;


            if (!String.IsNullOrWhiteSpace(temp4))
            {
                // checks if the string is male and is case insenstive if it is set the variable to the input else is incorrect
                //found https://stackoverflow.com/questions/3121957/how-can-i-do-a-case-insensitive-string-comparison
                if (String.Equals(temp4, "male", StringComparison.OrdinalIgnoreCase))
                {
                    gender = (Gender)Enum.Parse(typeof(Gender), "Male");
                    genderset = true;
                }

                if (String.Equals(temp4, "female", StringComparison.OrdinalIgnoreCase) && genderset == false)
                {
                    gender = (Gender)Enum.Parse(typeof(Gender), "Female");
                    genderset = true;

                }

                if (String.Equals(temp4, "other", StringComparison.OrdinalIgnoreCase) && genderset == false)
                {
                    gender = (Gender)Enum.Parse(typeof(Gender), "Other");
                    genderset = true;

                }

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewCustomer();
            }
            if(genderset == false)
            {
                Console.WriteLine("This is not valid gender");
                NewCustomer();

            }

            Console.Write("DOB (DD/MM/YYYY)*: ");

            string temp5 = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(temp5))
            {
                DateTime tempDate;
                if(DateTime.TryParse(temp5, out tempDate))
                {
                    String.Format("{0:d/MM/yyyy}", tempDate);
                    DOB = tempDate;
                    ready = true;

                }
                else
                {
                    Console.WriteLine("Not a valid Date\n");
                    NewCustomer();

                }
                
            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewCustomer();
            }

            if (ready == true)
            {
                int ID = 1;
                ID = crmm.CustomerCount(ID);
                Customer customer = new Customer(ID, title, firstName, lastName, gender, DOB);
                if (crmm.AddCustomer(customer) == true)
                {
                    Console.WriteLine("Successfully Added Customer");
                }
                else
                {
                    Console.WriteLine("Failure");

                }

            }

        }

        //This the validation and the gateway between the menu to the actual adding of them to the fleet for vehicles
        //manually inputting all values
        public static void NewVehicle()
        {
            string registration = "";
            VehicleGrade grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Economy");
            string make = "";
            string model = "";
            int year = 0;
            int numseats = 0;
            TransmissionType transmission = (TransmissionType)Enum.Parse(typeof(TransmissionType), "Manual");
            FuelType fuel = (FuelType)Enum.Parse(typeof(FuelType), "Petrol");
            bool GPS = false;
            bool SunRoof = false;
            double dailyrate = 0.0;
            string colour = "black";



            //comment here
            Console.Write("Please fill the following fields (fields marked with * are required)\n\n" +
                                "Registration*: ");
            string temp1 = Console.ReadLine();
            temp1 = temp1.ToUpper();
            bool ready = false;
            if (!String.IsNullOrWhiteSpace(temp1))
            {
                if (fleeet.AddVehicleRegoCheck(temp1))
                {
                    registration = temp1;

                }
                else
                {
                    Console.WriteLine("\nThe registration already exists or doesnt follow the correct format of 3 numbers and 3 letters e.g(123ABC)\n");
                    NewVehicle();

                }
            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }
            //////////////////////////////////////////////////////////////////////

            Console.Write("Vehicle Grade*: ");
            string temp2 = Console.ReadLine();
            bool vehiclegradeset = false;
            if (!String.IsNullOrWhiteSpace(temp2))
            {
                // checks if the string is economy and is case insenstive if it is set the variable to the input else is incorrect
                //found https://stackoverflow.com/questions/3121957/how-can-i-do-a-case-insensitive-string-comparison
                if (String.Equals(temp2, "Economy", StringComparison.OrdinalIgnoreCase))
                {
                    grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Economy"); 
                    vehiclegradeset = true;
                }

                if (String.Equals(temp2, "Family", StringComparison.OrdinalIgnoreCase) && vehiclegradeset == false)
                {
                    grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Family");
                    vehiclegradeset = true;

                }

                if (String.Equals(temp2, "Luxury", StringComparison.OrdinalIgnoreCase) && vehiclegradeset == false)
                {
                    grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Luxury");
                    vehiclegradeset = true;

                }

                if (String.Equals(temp2, "Commercial", StringComparison.OrdinalIgnoreCase) && vehiclegradeset == false)
                {
                    grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Commercial");
                    vehiclegradeset = true;

                }

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }
            if (vehiclegradeset == false)
            {
                Console.WriteLine("This is not valid vehicle grade\n");
                NewVehicle();

            }
            //////////////////////////////////////////////////////////////////////

            Console.Write("Make*: ");
            string temp3 = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(temp3))
            {
                make = temp3;
            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }
            //////////////////////////////////////////////////////////////////////

            Console.Write("Model*: ");
            string temp4 = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(temp4))
            {
                model = temp4;

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }

            //////////////////////////////////////////////////////////////////////

            Console.Write("Year*: ");
            string temp5 = Console.ReadLine();
            int tyear = 0;

            if (!String.IsNullOrWhiteSpace(temp5))
            {
                if(int.TryParse(temp5 , out tyear))
                {
                    year = tyear;
                }
                else
                {
                    Console.WriteLine("Input isnt an integer\n");
                    NewVehicle();
                }

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }

            //////////////////////////////////////////////////////////////////////

            Console.Write("Number of Seats*: ");
            string temp6 = Console.ReadLine();
            int tnumseats = 0;

            if (!String.IsNullOrWhiteSpace(temp6))
            {
                if (int.TryParse(temp6, out tnumseats))
                {
                    numseats = tnumseats;
                }
                else
                {
                    Console.WriteLine("Input isnt an integer\n");
                    NewVehicle();
                }

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }

            //////////////////////////////////////////////////////////////////////

            Console.Write("Transmission Type: ");
            string temp7 = Console.ReadLine();
            bool transset = false;

            if (!String.IsNullOrWhiteSpace(temp7))
            {
                // checks if the string is manual and is case insenstive if it is set the variable to the input else is incorrect
                //found https://stackoverflow.com/questions/3121957/how-can-i-do-a-case-insensitive-string-comparison
                if (String.Equals(temp7, "manual", StringComparison.OrdinalIgnoreCase))
                {
                    transmission = (TransmissionType)Enum.Parse(typeof(TransmissionType), "Manual");
                    transset = true;
                }

                if (String.Equals(temp7, "automatic", StringComparison.OrdinalIgnoreCase) && transset == false)
                {
                    transmission = (TransmissionType)Enum.Parse(typeof(TransmissionType), "Automatic");
                    transset = true;

                }

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }
            if (transset == false)
            {
                Console.WriteLine("This is not valid transmission type\n");
                NewVehicle();

            }
            //////////////////////////////////////////////////////////////////////

            Console.Write("Fuel Type: ");
            string temp8 = Console.ReadLine();
            bool fuelset = false;

            if (!String.IsNullOrWhiteSpace(temp8))
            {
                // checks if the string is petrol and is case insenstive if it is set the variable to the input else is incorrect
                //found https://stackoverflow.com/questions/3121957/how-can-i-do-a-case-insensitive-string-comparison
                if (String.Equals(temp8, "petrol", StringComparison.OrdinalIgnoreCase))
                {
                    fuel = (FuelType)Enum.Parse(typeof(FuelType), "Petrol");
                    fuelset = true;
                }

                if (String.Equals(temp8, "diesel", StringComparison.OrdinalIgnoreCase) && fuelset == false)
                {
                    fuel = (FuelType)Enum.Parse(typeof(FuelType), "Diesel");
                    fuelset = true;

                }

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }
            if (fuelset == false)
            {
                Console.WriteLine("This is not valid fuel type\n");
                NewVehicle();

            }
            //////////////////////////////////////////////////////////////////////
            Console.Write("GPS*: ");
            string temp9 = Console.ReadLine();
            bool tgps = false;

            if (!String.IsNullOrWhiteSpace(temp9))
            {
                if (bool.TryParse(temp9, out tgps))
                {
                    GPS = tgps;
                }
                else
                {
                    Console.WriteLine("Input isnt a boolean\n");
                    NewVehicle();
                }

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }
            //////////////////////////////////////////////////////////////////////
            Console.Write("Sun Roof*: ");
            string temp10 = Console.ReadLine();
            bool tsunroof;

            if (!String.IsNullOrWhiteSpace(temp10))
            {
                if (bool.TryParse(temp10, out tsunroof))
                {
                    SunRoof = tsunroof;
                }
                else
                {
                    Console.WriteLine("Input isnt a boolean\n");
                    NewVehicle();
                }

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }
            //////////////////////////////////////////////////////////////////////
            Console.Write("Daily Rate*: ");
            string temp11 = Console.ReadLine();
            double tdailyrate;

            if (!String.IsNullOrWhiteSpace(temp11))
            {
                if (double.TryParse(temp11, out tdailyrate))
                {
                    dailyrate = tdailyrate;
                }
                else
                {
                    Console.WriteLine("Input isnt a double\n");
                    NewVehicle();
                }

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }
            //////////////////////////////////////////////////////////////////////

            Console.Write("Colour *: ");
            string temp12 = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(temp12))
            {
                colour = temp12;
                ready = true;
            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }


            if (ready == true)
            {

                Vehicle vehicle = new Vehicle(registration, grade, make, model, year, numseats, transmission, fuel, GPS, SunRoof, dailyrate, colour );
                if (fleeet.AddVehicle(vehicle) == true)
                {
                    Console.WriteLine("Successfully Added Vehicle");
                }
                else
                {
                    Console.WriteLine("Failure");

                }

            }

        }

        //This the validation and the gateway between the menu to the actual adding of them to the fleet for vehicles
        //using default values
        public static void NewVehicleWDefaults()
        {
            string registration = "";
            VehicleGrade grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Economy");
            string make = "";
            string model = "";
            int year = 0;
            
            //comment here
            Console.Write("Please fill the following fields (fields marked with * are required)\n\n" +
                                "Registration*: ");
            string temp1 = Console.ReadLine();
            temp1 = temp1.ToUpper();
            bool ready = false;
            if (!String.IsNullOrWhiteSpace(temp1))
            {
                if(fleeet.AddVehicleRegoCheck(temp1))
                {
                    registration = temp1;

                }
                else
                {
                    Console.WriteLine("\nThe registration already exists or doesnt follow the correct format of 3 numbers and 3 letters e.g(123ABC)\n");
                    NewVehicle();

                }
            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }
            //////////////////////////////////////////////////////////////////////

            Console.Write("Vehicle Grade*: ");
            string temp2 = Console.ReadLine();
            bool vehiclegradeset = false;
            if (!String.IsNullOrWhiteSpace(temp2))
            {
                // checks if the string is economy and is case insenstive if it is set the variable to the input else is incorrect
                //found https://stackoverflow.com/questions/3121957/how-can-i-do-a-case-insensitive-string-comparison
                if (String.Equals(temp2, "Economy", StringComparison.OrdinalIgnoreCase))
                {
                    grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Economy");
                    vehiclegradeset = true;
                }

                if (String.Equals(temp2, "Family", StringComparison.OrdinalIgnoreCase) && vehiclegradeset == false)
                {
                    grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Family");
                    vehiclegradeset = true;

                }

                if (String.Equals(temp2, "Luxury", StringComparison.OrdinalIgnoreCase) && vehiclegradeset == false)
                {
                    grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Luxury");
                    vehiclegradeset = true;

                }

                if (String.Equals(temp2, "Commercial", StringComparison.OrdinalIgnoreCase) && vehiclegradeset == false)
                {
                    grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Commercial");
                    vehiclegradeset = true;

                }

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }
            if (vehiclegradeset == false)
            {
                Console.WriteLine("This is not valid vehicle grade\n");
                NewVehicle();

            }
            //////////////////////////////////////////////////////////////////////

            Console.Write("Make*: ");
            string temp3 = Console.ReadLine();
            if (!String.IsNullOrWhiteSpace(temp3))
            {
                make = temp3;
            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }
            //////////////////////////////////////////////////////////////////////

            Console.Write("Model*: ");
            string temp4 = Console.ReadLine();

            if (!String.IsNullOrWhiteSpace(temp4))
            {
                model = temp4;

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }

            //////////////////////////////////////////////////////////////////////

            Console.Write("Year*: ");
            string temp5 = Console.ReadLine();
            int tyear = 0;

            if (!String.IsNullOrWhiteSpace(temp5))
            {
                if (int.TryParse(temp5, out tyear))
                {
                    year = tyear;
                    ready = true;

                }
                else
                {
                    Console.WriteLine("Input isnt an integer\n");
                    NewVehicle();
                }

            }
            else
            {
                Console.WriteLine("Input cannot be empty\n");
                NewVehicle();
            }

            //////////////////////////////////////////////////////////////////////

            if (ready == true)
            {

                Vehicle vehicle = new Vehicle(registration, grade, make, model, year);
                if (fleeet.AddVehicle(vehicle) == true)
                {
                    Console.WriteLine("Successfully Added Vehicle\n");
                }
                else
                {
                    Console.WriteLine("Failure\n");

                }

            }

        }

        //This writes out to the screen the menu options and question
        public static void newvehiclesubmenu()
        {
            string menuoptions = "Please enter a number from the options below:\n" +
                       "\n" +
                       "1) Manual Input in all fields\n" +
                       "2) Input through defaults\n";

            Console.WriteLine(menuoptions);
        }

        //This writes out to the screen the menu options and question
        public static void subsubmenu()
        {
            string menuoptions = "Please enter a number from the options below:\n" +
                                  "\n" +
                                  "1) Display All\n" +
                                  "2) Display a single customer\n";

            Console.WriteLine(menuoptions);
        }


        //This writes out to the screen the menu options and question
        public static void fleetsubsubmenu()
        {
            string menuoptions = "Please enter a number from the options below:\n" +
                                  "\n" +
                                  "1) Display All\n";

            Console.WriteLine(menuoptions);
        }

        //This writes out to the screen the menu options and question
        public static void SubMenu(int subchoice)
        {
            string menuoptions1 = " ";


            //comment
            if (subchoice == 1)
            {
                menuoptions1 = "Please enter a number from the options below:\n" +
                                  "\n" +
                                  "1) Display Customer\n" +
                                  "2) New Customer\n" +
                                  "3) Modify Customer\n" +
                                  "4) Delete Customer\n";

            }
            else if (subchoice == 2)
            {
                menuoptions1 = "Please enter a number from the options below:\n" +
                                  "\n" +
                                  "1) Display Fleet\n" +
                                  "2) New Vehicle\n" +
                                  "3) Modify Vehicle\n" +
                                  "4) Delete Vehicle\n";

            }
            else if (subchoice == 3)
            {
                menuoptions1 = "Please enter a number from the options below:\n" +
                                  "\n" +
                                  "1) Display Rentals\n" +
                                  "2) Search Vehicles\n" +
                                  "3) Rent Vehicle\n" +
                                  "4) Return Vehicle\n";

            }


            Console.WriteLine(menuoptions1);

        }
    }
}
