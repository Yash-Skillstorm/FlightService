using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public class ConstantString
    {
        //Flight Table
        public const string FlightId = "flight_Id";
        public const string FlightName = "flight_Num";
        public const string DepartureAirport = "Departure_Airport";
        public const string ArrivalAirport = "Arrival_Airport";
        public const string DepartureDate = "Departure_Date";
        public const string ArrivalDate = "Arrival_Date";
        public const string DepartureTime = "Departure_Time";
        public const string ArrivalTime = "Arrival_Time";
        public const string SeatCapacity = "Seat_Capacity";


        //Passenger Table
        public const string PassengerId = "Passenger_Id";
        public const string PassengerName = "Passenger_Name";
        public const string PassengerAge = "Passenger_Age";
        public const string PassengerEmail = "Passenger_Email";

    }
}
