using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Collections;

namespace MRRC
{
    public class Fleet
    {
        public List<Vehicle> vehicles = new List<Vehicle>();
        private string fleetFile;
        private string rentalFile;
        Dictionary<string, int> rentals = new Dictionary<string, int>();
        //ArrayList vehicles; // vehiclesin the fleet
        public SortedList attributeSets = new SortedList(); // sets of vehicles (regos) with a given attribute value

        // If there is no fleet file at the specified location, this constructor constructors
        // an empty fleet and empty rentals. Otherwise it loads the fleet and rentals from the
        // specified files (see the assignment specification).
        public Fleet(string fleetFile, string rentalFile)
        {
            if (File.Exists(fleetFile))
            {
                this.fleetFile = fleetFile;
                this.rentalFile = rentalFile;
                LoadVehiclesFromFile();
                LoadRentalsFromFile();
                //vehicles = new ArrayList();
                attributeSets = new SortedList();

            }
            else
            {
                this.fleetFile = @"..\..\..\Data\fleet.csv";
                this.rentalFile = @"..\..\..\Data\rentals.csv";
                vehicles = new List<Vehicle>();
            }
        }
        // Adds the provided vehicle to the fleet assuming the vehicle registration does not
        // already exist. It returns true if the add was successful (the registration did not
        // already exist in the fleet), and false otherwise.

        // https://stackoverflow.com/questions/4371008/check-format-of-a-string

        private static readonly Regex regoRegex = new Regex(@"\b\d{3}[a-zA-Z]{3}\b");

        public bool AddVehicleRegoCheck(string registration)
        {
            // ^\d{ 3}[a-zA-Z]{3}$
            if (regoRegex.IsMatch(registration))
            {
                foreach (Vehicle item in vehicles)
                {
                    if(item.vehicleRego == registration)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public bool AddVehicleRegoCheck1(string registration)
        {
            // ^\d{ 3}[a-zA-Z]{3}$
            if (regoRegex.IsMatch(registration))
            {
                foreach (Vehicle item in vehicles)
                {
                    if (item.vehicleRego == registration)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return false;
        }

        public bool AddVehicle(Vehicle vehicle)
        {
            int counter = 0;
            foreach (Vehicle item in vehicles)
            {
                if (item.vehicleRego == vehicle.vehicleRego)
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
                vehicles.Add(vehicle);
                return true;
            }
        }
// This method removes the vehicle with the provided rego from the fleet if it is not
// currently rented. It returns true if the removal was successful and false otherwise.
        public bool RemoveVehicle(string registration)
        {
            if (rentals.ContainsKey(registration))
            {
                return false;
            }
            else
            {
                {
                    foreach (Vehicle item in vehicles)
                    {

                        if (item.vehicleRego == registration)
                        {
                            vehicles.Remove(item);
                            return true;
                        }

                    }
                    return false;
                }
            }
        }
// This method returns the fleet (as a list of Vehicles).
        public List<Vehicle> GetFleet()
        {
            return vehicles;

        }

        public double calccost(string registration, int time)
        {
            foreach(Vehicle item in vehicles)
            {
                if (item.vehicleRego == registration)
                {
                    return item.dailyRate * time;
                }
            }
            return 0.00;
        }

        public string displayfleet()
        {
            List<Vehicle> vehichles = GetFleet();
            string table = "";

            table += ("----------------------------------------\n");
            foreach (Vehicle V in vehicles)
            {
                table += V.toTable() + "\n";
            }
            table += ("---------------------------------------\n");

            return table;
        }

        public string displayrentals()
        {
            bool rented = true;
            List<Vehicle> vehichles = GetFleet(rented);
            string table1 = "";

            table1 += ("----------------------------------------\n");
            foreach (Vehicle V in vehichles)
            {
                table1 += V.toTable() + "\n";
            }
            table1 += ("---------------------------------------\n");

            return table1;
        }
// This method returns a subset of vehicles in the fleet depending on whether they are
// currently rented. If rented is true, this method returns all rented vehicles. If it
// false, this method returns all not yet rented vehicles.
        public List<Vehicle> GetFleet(bool rented)
        {
            List<Vehicle> result = new List<Vehicle>();
            if (rented == true)
            {
                foreach (Vehicle item in vehicles)
                {
                    if (rentals.ContainsKey(item.vehicleRego))
                    {
                        result.Add(item);
                    }
                }
            }
            else
            {
                foreach (var item in vehicles)
                {
                    if (!rentals.ContainsKey(item.vehicleRego))
                    {
                        result.Add(item);
                    }
                }

            }
            return result;
        }
// This method returns whether the vehicle with the provided registration is currently
// being rented.
        public bool IsRented(string registration)
        {
            if (rentals.ContainsKey(registration))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
// This method returns whether the customer with the provided customer ID is currently
// renting a vehicle.
        public bool IsRenting(int customerID)
        {
            if (rentals.ContainsValue(customerID))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Vehicle GetVehicle(string registration)
        {
            //LoadFromFile();
            foreach (Vehicle item in vehicles)
            {
                if (item.vehicleRego == registration)
                {
                    return item;
                }

            }
            return null;

        }

        public bool GetVehicleEdit(string registration, int choice)
        {
            Vehicle vehicle = GetVehicle(registration);
            if (vehicle == null)
            {
                Console.WriteLine("The entered registration is not valid or doesn't exist\n");
                return false;
            }

            bool validcheck = false;
            if (choice == 1)
            {
                Console.WriteLine("Registration Exists\n\n Modify Registration: ");
                string newvehicleRego = Console.ReadLine();
                newvehicleRego = newvehicleRego.ToUpper();

                if (!String.IsNullOrWhiteSpace(newvehicleRego))
                {
                    foreach (Vehicle item in vehicles)
                    {
                        if (item.vehicleRego == registration)
                        {
                            if (AddVehicleRegoCheck(newvehicleRego))
                            {
                                item.vehicleRego = newvehicleRego;
                                Console.WriteLine("\nSuccess\n");
                                return true;

                            }
                            else
                            {
                                validcheck = false;
                            }
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
                Console.WriteLine("ID Exists\n\n Modify Grade: ");
                string newgrade = Console.ReadLine();
                bool gradeset = false;

                if (!String.IsNullOrWhiteSpace(newgrade))
                {

                    if (String.Equals(newgrade, "economy", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Economy");
                                gradeset = true;
                                Console.WriteLine("\nSuccess\n");


                                return true;
                            }
                        }
                    }

                    if (String.Equals(newgrade, "family", StringComparison.OrdinalIgnoreCase) && gradeset == false)
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Family");
                                gradeset = true;
                                Console.WriteLine("\nSuccess\n");


                                return true;
                            }
                        }
                    }

                    if (String.Equals(newgrade, "luxury", StringComparison.OrdinalIgnoreCase) && gradeset == false)
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Luxury");
                                gradeset = true;
                                Console.WriteLine("\nSuccess\n");


                                return true;
                            }
                        }
                    }

                    if (String.Equals(newgrade, "commercial", StringComparison.OrdinalIgnoreCase) && gradeset == false)
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), "Commercial");
                                gradeset = true;
                                Console.WriteLine("\nSuccess\n");


                                return true;
                            }
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
                Console.WriteLine("ID Exists\n\n Modify Make: ");
                string newmake = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(newmake))
                {
                    foreach (Vehicle item in vehicles)
                    {
                        if (item.vehicleRego == registration)
                        {
                            item.make = newmake;
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
                Console.WriteLine("ID Exists\n\n Modify Model: ");
                string newmodel = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(newmodel))
                {
                    foreach (Vehicle item in vehicles)
                    {
                        if (item.vehicleRego == registration)
                        {
                            item.model = newmodel;
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
            if (choice == 5)
            {
                Console.WriteLine("ID Exists\n\n Modify Year: ");
                string newyear = Console.ReadLine();
                int tyear = 0;

                if (!String.IsNullOrWhiteSpace(newyear))
                {
                    if (int.TryParse(newyear, out tyear))
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.year = tyear;
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
                else
                {
                    validcheck = false;
                }
            }
            if (choice == 6)
            {
                Console.WriteLine("ID Exists\n\n Modify Number of Seats: ");
                string newnumSeats = Console.ReadLine();
                int tnumSeats = 0;

                if (!String.IsNullOrWhiteSpace(newnumSeats))
                {
                    if (int.TryParse(newnumSeats, out tnumSeats))
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.numSeats = tnumSeats;
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
                else
                {
                    validcheck = false;
                }

            }
            if (choice == 7)
            {
                Console.WriteLine("ID Exists\n\n Modify Transmission Type: ");
                string newtransmission = Console.ReadLine();
                bool transet = false;

                if (!String.IsNullOrWhiteSpace(newtransmission))
                {

                    if (String.Equals(newtransmission, "manual", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.transmission = (TransmissionType)Enum.Parse(typeof(TransmissionType), "Manual");
                                transet = true;
                                Console.WriteLine("\nSuccess\n");


                                return true;
                            }
                        }
                    }

                    if (String.Equals(newtransmission, "automatic", StringComparison.OrdinalIgnoreCase) && transet == false)
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.transmission = (TransmissionType)Enum.Parse(typeof(TransmissionType), "Automatic");
                                transet = true;
                                Console.WriteLine("\nSuccess\n");


                                return true;
                            }
                        }
                    }

                }
                else
                {
                    validcheck = false;
                }

            }
            if (choice == 8)
            {
                Console.WriteLine("ID Exists\n\n Modify Fuel Type: ");
                string newfueltype = Console.ReadLine();
                bool fuelset = false;

                if (!String.IsNullOrWhiteSpace(newfueltype))
                {

                    if (String.Equals(newfueltype, "petrol", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.fuel = (FuelType)Enum.Parse(typeof(FuelType), "Petrol");
                                fuelset = true;
                                Console.WriteLine("\nSuccess\n");


                                return true;
                            }
                        }
                    }

                    if (String.Equals(newfueltype, "diesel", StringComparison.OrdinalIgnoreCase) && fuelset == false)
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.fuel = (FuelType)Enum.Parse(typeof(FuelType), "Diesel");
                                fuelset = true;
                                Console.WriteLine("\nSuccess\n");


                                return true;
                            }
                        }
                    }

                }
                else
                {
                    validcheck = false;
                }
            }
            if (choice == 9)
            {
                Console.WriteLine("ID Exists\n\n Modify GPS: ");
                string newGPS = Console.ReadLine();
                bool tGPS = false;

                if (!String.IsNullOrWhiteSpace(newGPS))
                {
                    if (bool.TryParse(newGPS, out tGPS))
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.GPS = tGPS;
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
                else
                {
                    validcheck = false;
                }

            }
            if (choice == 10)
            {
                Console.WriteLine("ID Exists\n\n Modify Sun Roof: ");
                string newsunRoof = Console.ReadLine();
                bool tsunRoof = false;

                if (!String.IsNullOrWhiteSpace(newsunRoof))
                {
                    if (bool.TryParse(newsunRoof, out tsunRoof))
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.sunRoof = tsunRoof;
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
                else
                {
                    validcheck = false;
                }

            }
            if (choice == 11)
            {
                Console.WriteLine("ID Exists\n\n Modify Daily Rate: ");
                string newdailyRate = Console.ReadLine();
                double tdailyRate = 0.0;

                if (!String.IsNullOrWhiteSpace(newdailyRate))
                {
                    if (double.TryParse(newdailyRate, out tdailyRate))
                    {
                        foreach (Vehicle item in vehicles)
                        {
                            if (item.vehicleRego == registration)
                            {
                                item.dailyRate = tdailyRate;
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
                else
                {
                    validcheck = false;
                }

            }
            if (choice == 12)
            {
                Console.WriteLine("ID Exists\n\n Modify Colour: ");
                string newcolour = Console.ReadLine();

                if (!String.IsNullOrWhiteSpace(newcolour))
                {
                    foreach (Vehicle item in vehicles)
                    {
                        if (item.vehicleRego == registration)
                        {
                            item.colour = newcolour;
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

            if (validcheck == false)
            {
                Console.WriteLine("\nInvalid\n");
                return false;
            }
            return true;

        }

            // This method returns the customer ID of the current renter of the vehicle. If it is
            // rented by no one, it returns -1. Technically this method can replace IsRented.
        public int RentedBy(string registration)
        {
            int id;
            if (rentals.ContainsKey(registration))
            {
                id = rentals[registration];
            }
            else
            {
                id = -1;
            }
            return id;
        }

        public bool checkcustomer(int customerID)
        {
            if (rentals.ContainsValue(customerID))
            {
                return true;

            }
            else
            {
                return false;
            }
        }

            // This method returns a vehicle. If the return is successful (it was currently being
            // rented) it returns the customer ID of the customer who was renting it, otherwise it
            // returns -1.
        public int ReturnVehicle(string registration)
        {
            int ID;
            if (!rentals.ContainsKey(registration))
            {
                return -1;
            }
            else
            {
            ID = rentals[registration];
            rentals.Remove(registration);
            return ID;
            }
        }
            // This method saves the current list of vehicles to file.
            public void SaveVehiclesToFile()
        {
            using (StreamWriter writer = new StreamWriter(fleetFile))
            {
                writer.Write("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", "Registration", "Grade", "Make", "Model", "Year", "numSeats", "Transmission", "Fuel", "GPS", "SunRoof", "dailyRate", "Colour\n");
                foreach (Vehicle item in vehicles)
                {
                    writer.WriteLine(item.ToCSVString());
                }
                writer.Close();
            }
        }

        // This method saves the current list of rentals to file.
        public void SaveRentalsToFile()
        {
            using (StreamWriter writer = new StreamWriter(rentalFile))
            {
                writer.Write("{0},{1}", "Registration", "CustomerID\n");
                foreach (var item in rentals)
                {
                    writer.WriteLine("{0},{1}", item.Key, item.Value);
                }
                writer.Close();
            }
        }
        // This method rents the vehicle with the provided registration to the customer with
        // the provided ID, if the vehicle is not currently being rented. It returns true if
        // the rental was possible, and false otherwise.
        public bool RentCar(string vehicleRego1, int customerID)
        {
            int counter = 0;
            bool flag = false;
            foreach (Vehicle item in vehicles)
            {
                if (item.vehicleRego == vehicleRego1)
                {
                    counter++;
                }
            }
            try
            {
                if (rentals.ContainsKey(vehicleRego1))
                {
                    string message = "Vehicle Rego already rented";
                    throw new AlreadyExistException(message);

                }
                else
                {
                    try
                    {
                        if (counter > 0)
                        {
                            rentals.Add(vehicleRego1, customerID);
                            flag = true;
                            return flag;


                        }
                        else
                        {
                            string message = "Vehicle Rego doesn't exist";
                            throw new DoesntExistException(message);
                        }
                    }
                    catch (DoesntExistException)
                    {
                        Console.WriteLine("Vehicle Rego doesn't exist");
                        return false;
                    }

                }
            }
            catch (AlreadyExistException)
            {
                Console.WriteLine("Vehicle Rego already rented");
                return false;
            }
        }

        // This method loads the list of vehicles from the file.
        public void LoadVehiclesFromFile()
        {
            using (StreamReader reader = new StreamReader(fleetFile))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    string[] tokens = line.Split(',');
                    string registration = tokens[0];
                    VehicleGrade grade = (VehicleGrade)Enum.Parse(typeof(VehicleGrade), tokens[1]);
                    string make = tokens[2];
                    string model = tokens[3];
                    int year = int.Parse(tokens[4]);
                    int numSeats = int.Parse(tokens[5]);
                    TransmissionType transmission = (TransmissionType)Enum.Parse(typeof(TransmissionType), tokens[6]);
                    FuelType fuel = (FuelType)Enum.Parse(typeof(FuelType), tokens[7]);
                    bool GPS = bool.Parse(tokens[8]);
                    bool SunRoof = bool.Parse(tokens[9]);
                    double dailyrate = double.Parse(tokens[10]);
                    string colour = tokens[11];


                    vehicles.Add(new Vehicle(registration, grade, make, model, year, numSeats, transmission, fuel, GPS, SunRoof, dailyrate, colour));
                }
            }
        }
// This method loads the list of rentals from the file.
        public void LoadRentalsFromFile(){
            using (StreamReader reader = new StreamReader(rentalFile))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {

                    string line = reader.ReadLine();
                    string[] tokens = line.Split(',');
                    string registration = tokens[0];
                    int ID = int.Parse(tokens[1]);

                    rentals.Add(registration, ID);
                }
            }
        }

        public void displayVehicle(int idx)
        {
            // a better approach could be based on overloading the toString() method
            Vehicle v = (Vehicle)vehicles[idx];
            Console.WriteLine("VehicleRego Grade Make Model Year numSeats transmission fuel GPS sunRoof dailyRate colour");
            Console.WriteLine("{0,-7} | {1,-6} | {2,-12} | {3,-5} | {4,-5} | {5,-5} | {6,-5} | {7,-5} | {8,-5} | {9,-5} | {10,-5} | {11,-5}\n", v.vehicleRego, v.grade, v.make, v.model, v.year, v.numSeats, v.transmission, v.fuel, v.GPS, v.sunRoof, v.dailyRate, v.colour);
        }

        public void blankquery()
        {
            foreach(Vehicle item in vehicles)
            {
                if (!IsRented(item.vehicleRego))
                {
                    displayVehicle(item.vehicleRego);
                }
            }
        }

        //_________________________________________________  
        //This code is from Shlomo Geva's lecture in week 9
        //__________________________________________________
        public void displayVehicle(string rego)
        {   // display vehicle by registration plate (rego)
            Vehicle v;
            for (int i = 0; i < vehicles.Count; i++)
            {   // go through, find the vehicle by rego
                v = (Vehicle)vehicles[i];
                if (v.vehicleRego == rego)
                {
                    displayVehicle(i);
                    break;
                }
            }
        }

        public void init_attributes()
        {
            int idx;
            HashSet<string> hs;

            foreach (Vehicle item in vehicles)
            {
                if (!IsRented(item.vehicleRego))
                {
                    foreach (string attribute in item.GetAttributeList())
                    {
                        if (attributeSets.ContainsKey(attribute))
                        {
                            idx = attributeSets.IndexOfKey(attribute);
                            if (idx >= 0)
                            {   // here if Colour set found
                                hs = (HashSet<string>)attributeSets.GetByIndex(idx); // get set
                                hs.Add(item.vehicleRego); // add to set
                                attributeSets.SetByIndex(idx, hs);  // save set (replaces old set)
                            }
                        }
                        else
                        {
                            attributeSets.Add(attribute, new HashSet<string>());
                            idx = attributeSets.IndexOfKey(attribute);
                            hs = (HashSet<string>)attributeSets.GetByIndex(idx); // get set
                            hs.Add(item.vehicleRego); // add to set
                            attributeSets.SetByIndex(idx, hs);  // save set (replaces old set)
                            //hs = (HashSet<string>attributeSets.)
                            //attributeSets.Add(attribute, item);
                        }
                    }
                }
            }
        }


        //_________________________________________________  
        //This code is from Shlomo Geva's lecture in week 9
        //__________________________________________________
        public void search(out string[] result, RPN rpn)
        {
            init_attributes();
            // Create and instantiate a new empty Stack for result sets.
            Stack setStack = new Stack();
            HashSet<string> hs1;
            HashSet<string> hs2;
            HashSet<string> hs;
            int idx;
            String[] temp = new string[] { };
            for (int i = 0; i < rpn.Postfix.Count; i++)
            {
                if (rpn.Postfix[i].Equals("AND"))
                {
                    // pop two sets off the stack and apply Intersect, push back result
                    hs1 = (HashSet<string>)setStack.Pop();
                    hs2 = (HashSet<string>)setStack.Pop();
                    temp = hs1.ToArray<string>(); // copy the elements of the set hs1
                    hs = new HashSet<string>(temp); // make a deep copy of hs1
                    hs.IntersectWith(hs2);// apply the Intersect to the new set
                    setStack.Push(hs); // push a reference to a new set
                }
                else if (rpn.Postfix[i].Equals("OR"))
                {
                    // pop two sets off the stack and apply Union
                    hs1 = (HashSet<string>)setStack.Pop();
                    hs2 = (HashSet<string>)setStack.Pop();
                    temp = hs1.ToArray<string>(); // copy the elements of the set hs1
                    hs = new HashSet<string>(temp); // make a deep copy of hs1
                    hs.UnionWith(hs2); // apply the Union to the new set
                    setStack.Push(hs); // push a reference to a new set
                }
                else
                {
                    // here if an operand
                    idx = attributeSets.IndexOfKey(rpn.Postfix[i]); // identify attribute set
                    if (idx >= 0)
                    {
                        hs1 = (HashSet<string>)attributeSets.GetByIndex(idx);
                        setStack.Push(hs1); // note: pushing a reference, not the actual set
                    }
                    else
                    {
                        throw new FormatException("Invalid attribute" + " " + rpn.Postfix[i]);
                    }
                }
            }
            if (setStack.Count == 1)
            {
                //hs1 = (HashSet<string>)attributeSets.GetByIndex(1);
                hs1 = (HashSet<string>)setStack.Pop();
                result = hs1.ToList().ToArray();
            }
            else
            {
                throw new Exception("Invalid query");
            }
        }



    }
}
