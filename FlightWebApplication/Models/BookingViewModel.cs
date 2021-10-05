using FlightWebApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightWebApplication.Models
{
    public class BookingViewModel
    {
        public string SelectedItem { get; set; }
        public int Passenger_id { get; set; }
        public IEnumerable<Flight> flightData { get; set; }
    }
}
