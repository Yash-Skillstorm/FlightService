using FlightWebApplication.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightWebApplication.Models
{
    public class BookingViewModel
    {
        [Required]
        public int SelectedItem { get; set; }
        [Required]
        [Display(Name = "Passenger Name")]
        public int Passenger_id { get; set; }
        public IEnumerable<Flight> flightData { get; set; }
    }
}
