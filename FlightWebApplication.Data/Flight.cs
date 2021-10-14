using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public class Flight
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Flight No.")]
        public int Flight_Num { get; set; }

        [Required]
        [Display(Name = "Departure")]
        public string Departure_Airport { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string Arrival_Airport { get; set; }

        [Required]
        [Display(Name = "Departure Date")]
        [DataType(DataType.Date)]
        public DateTime Departure_Date { get; set; }

        [Required]
        [Display(Name = "Boarding Time")]
        [DataType(DataType.Time)]
        public TimeSpan Departure_Time { get; set; }

        [Required]
        [Display(Name = "Arrival Date")]
        [DataType(DataType.Date)]
        public DateTime Arrival_Date { get; set; }

        [Required]
        [Display(Name = "Arrival Time")]
        [DataType(DataType.Time)]
        public TimeSpan Arrival_Time { get; set; }

        [Display(Name ="Seat Capacity")]
        public int Seat_Capacity { get; set; }
        public Flight()
        {

        }
        public Flight(int Flg_Num, string Dep_Airport, string Arr_Airport, DateTime Dep_Date, DateTime Arr_Date, TimeSpan Dep_Time, TimeSpan Arr_Time, int Seat_Cap)
        {
            this.Flight_Num = Flg_Num;
            this.Departure_Airport = Dep_Airport;
            this.Arrival_Airport = Arr_Airport;
            this.Departure_Date = Dep_Date;
            this.Arrival_Date = Arr_Date;
            this.Departure_Time = Dep_Time;
            this.Arrival_Time = Arr_Time;
            this.Seat_Capacity = Seat_Cap;
        }
    }
}
