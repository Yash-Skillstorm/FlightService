using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public class Booking
    {
        public int Id { get; set; }
        public int Flight_Id { get; set; }
        public int Flight_Num { get; set; }
        public int Passenger_id { get; set; }
        public string Passenger_Name { get; set;}        
        public int Reservation_Num { get; set; }       
        public Booking(int flig_Id, int Flig_Num, int Passg_Id, string Pass_Name, int rese_Num)
        {
            this.Flight_Id = flig_Id;
            this.Flight_Num = Flig_Num;
            this.Passenger_id = Passg_Id;
            this.Passenger_Name = Pass_Name;
            this.Reservation_Num = rese_Num;
        }
        public Booking()
        {
        }
    }
}
