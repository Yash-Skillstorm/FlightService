using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public class Booking
    {
        public int Id { get; set; }        
        public int Flight_Id { get; set; }
        
        [Display(Name = "Flight No.")]
        public int Flight_Num { get; set; }
        public int Passenger_id { get; set; }

        [Display(Name = "Passenger Name")]
        public string Passenger_Name { get; set;}

        [Display(Name = "Reservation No.")]
        public int Reservation_Num { get; set; }
        [Required]
        [Display(Name = "Departure")]
        public string Departure_Airport { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string Arrival_Airport { get; set; }
        
        public Booking(int flig_Id, int Flig_Num, int Passg_Id, string Pass_Name, int rese_Num, string dep_Airpot, string arr_Airport)
        {
            this.Flight_Id = flig_Id;
            this.Flight_Num = Flig_Num;
            this.Passenger_id = Passg_Id;
            this.Passenger_Name = Pass_Name;
            this.Reservation_Num = rese_Num;
            this.Departure_Airport = dep_Airpot;
            this.Arrival_Airport = arr_Airport;
        }
        public Booking()
        {
        }
    }
}
