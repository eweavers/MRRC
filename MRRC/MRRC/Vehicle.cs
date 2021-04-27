using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRRC
{
    public enum VehicleGrade
    {
        Economy,
        Family,
        Luxury,
        Commercial
    };

    public enum FuelType
    {
        Petrol,
        Diesel
    };

    public enum TransmissionType
    {
        Manual,
        Automatic
    };

    public class Vehicle
    {
        // This constructor provides values for all parameters of the vehicle.

        public string vehicleRego;
        public string make;
        public string model;
        public int year;
        public VehicleGrade grade;
        public int numSeats;
        public TransmissionType transmission;
        public FuelType fuel;
        public bool GPS;
        public bool sunRoof;
        public double dailyRate;
        public string colour;
        public Vehicle(string registration, VehicleGrade grade, string make, string model,
        int year, int numSeats, TransmissionType transmission, FuelType fuel,
        bool GPS, bool sunRoof, double dailyRate, string colour)
        {
            this.vehicleRego = registration;
            this.grade = grade;
            this.make = make;
            this.model = model;
            this.year = year;
            this.numSeats = numSeats;
            this.transmission = transmission;
            this.fuel = fuel;
            this.GPS = GPS;
            this.sunRoof = sunRoof;
            this.dailyRate = dailyRate;
            this.colour = colour;

        }
// This constructor provides only the mandatory parameters of the vehicle. Others are
// set based on the defaults of each class.
// Overall defaults:
// - 4-seater
// - Manual transmission
// - Petrol fuel
// - No GPS
// - No sun-roof
// - $50/day
// - Black
// Economy vehicles:
// - Automatic transmission
// Family vehicles:
// - $80/day
// Luxury vehicles:
// - Has GPS
// - Has sun-roof
// - $120/day
// Commercial vehicle:
// - Diesel fuel
// - $130/day

        public Vehicle(string registration, VehicleGrade grade, string make, string model, int year)
        {
            this.vehicleRego = registration;
            this.grade = grade;
            this.make = make;
            this.model = model;
            this.year = year;

            numSeats = 4;
            transmission = TransmissionType.Manual;
            fuel = FuelType.Petrol;
            GPS = false;
            sunRoof = false;
            dailyRate = 50.0;
            colour = "Black";

            if(grade == VehicleGrade.Economy)
            {
                transmission = TransmissionType.Automatic;

            }
            if (grade == VehicleGrade.Family)
            {
                dailyRate = 80.0;
            }
            if (grade == VehicleGrade.Luxury)
            {
                GPS = true;
                sunRoof = true;
                dailyRate = 120.0;
            }
            if (grade == VehicleGrade.Commercial)
            {
                fuel = FuelType.Diesel;
                dailyRate = 130.0;
            }
        }

        //this method puts the information into a table format
        public string toTable()
        {
            string table = String.Format(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11} |", vehicleRego, grade, make, model, year, numSeats, transmission, fuel, GPS, sunRoof, dailyRate, colour);
            return table;
        }

        public string toTableRentals()
        {
            string table = String.Format(" {0} | {1} | {2} | {3} | {4} | {5} | {6} | {7} | {8} | {9} | {10} | {11} |", vehicleRego, grade, make, model, year, numSeats, transmission, fuel, GPS, sunRoof, dailyRate, colour);
            return table;
        }
        // This method should return a CSV representation of the vehicle that is consistent
        // with the provided data files.
        public string ToCSVString()
        {
            return vehicleRego + "," + grade + "," + make + "," + model + "," + year + "," + numSeats + "," + transmission + "," + fuel + "," + GPS + "," + sunRoof + "," + dailyRate + "," + colour;

        }
        /*public string ToCSVStringrentals()
        {
            return vehicleRego + "," + customer;

        }*/
        // This method should return a string representation of the attributes of the vehicle.
        public override string ToString()
        {
            string theString = "";
            theString = theString + vehicleRego + " " + grade + " " + make + " " + model + " " + year.ToString() + " " + numSeats.ToString() + " " + transmission + " " + fuel + " " + GPS.ToString() + " " + sunRoof.ToString() + " " + dailyRate.ToString() + " " + colour;
            return theString;
        }

        // This method should return a list of strings which represent each attribute. Values
        // should be made to be unique (e.g. numSeats should not be written as “4” but as “4
        // Seater”, sunroof should not be written as “True” but as “sunroof” or with no string
        // added if there is no sunroof). Vehicle rego, grade, make, model, year, transmission
        // type, fuel type, and colour can all be assumed to not overlap (i.e. if the make
        // “Mazda” exists, “Mazda” will not exist in other attributes). You do not need to
        // maintain this restriction, only assume it is true. You do not need to add the daily
        // rate to this list.
        public List<string> GetAttributeList()
        {
            List<string> GetAttributes = new List<string>();
            GetAttributes.Add(vehicleRego);
            GetAttributes.Add(grade.ToString());
            GetAttributes.Add(make);
            GetAttributes.Add(model);
            GetAttributes.Add(year.ToString());
            GetAttributes.Add(numSeats.ToString() + " Seater");
            GetAttributes.Add(transmission.ToString());
            GetAttributes.Add(fuel.ToString());
            if (GPS == true)
            {
                GetAttributes.Add("GPS");
            }
            else
            {
                GetAttributes.Add(" ");
            }
            if (sunRoof == true)
            {
                GetAttributes.Add("Sunroof");
            }
            else
            {
                GetAttributes.Add(" ");
            }
            GetAttributes.Add("$" + dailyRate.ToString());
            GetAttributes.Add(colour);
            return new List<string>(GetAttributes.Distinct());
        }

    }
}