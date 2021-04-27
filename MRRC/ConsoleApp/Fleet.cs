using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Fleet
    {
        /*
        // If there is no fleet file at the specified location, this constructor constructors
        // an empty fleet and empty rentals. Otherwise it loads the fleet and rentals from the
        // specified files (see the assignment specification).
        public Fleet(string fleetFile, string rentalsFile)
// Adds the provided vehicle to the fleet assuming the vehicle registration does not
// already exist. It returns true if the add was successful (the registration did not
// already exist in the fleet), and false otherwise.
        public bool AddVehicle(Vehicle vehicle)
// This method removes the vehicle with the provided rego from the fleet if it is not
// currently rented. It returns true if the removal was successful and false otherwise.
        public bool RemoveVehicle(string registration)
// This method returns the fleet (as a list of Vehicles).
public List<Vehicle> GetFleet()
// This method returns a subset of vehicles in the fleet depending on whether they are
// currently rented. If rented is true, this method returns all rented vehicles. If it
// false, this method returns all not yet rented vehicles.
        public List<Vehicle> GetFleet(bool rented)
// This method returns whether the vehicle with the provided registration is currently
// being rented.
        public bool IsRented(string registration)
        */
// This method returns whether the customer with the provided customer ID is currently
// renting a vehicle.
        public bool IsRenting(int customerID)
        {

        }
// This method returns the customer ID of the current renter of the vehicle. If it is
// rented by no one, it returns -1. Technically this method can replace IsRented.
        /*public int RentedBy(string registration)
// This method rents the vehicle with the provided registration to the customer with
// the provided ID, if the vehicle is not currently being rented. It returns true if
// the rental was possible, and false otherwise.
        public bool RentVehicle(string registration, int customerID)
// This method returns a vehicle. If the return is successful (it was currently being
// rented) it returns the customer ID of the customer who was renting it, otherwise it
// returns -1.
        public int ReturnVehicle(string registration)
// This method saves the current list of vehicles to file.
public void SaveVehiclesToFile()
// This method saves the current list of rentals to file.
public void SaveRentalsToFile()
// This method loads the list of vehicles from the file.
private void LoadVehiclesFromFile()
// This method loads the list of rentals from the file.
private void LoadRentalsFromFile()
    }
}
*/