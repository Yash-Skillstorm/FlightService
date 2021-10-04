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
        public int Passenger_id { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual Passenger Passenger { get; set; }
        public int Reservation_Num { get; set; }
        public Booking(int flig_Id, int Passg_Id, int rese_Num)
        {
            this.Flight_Id = flig_Id;
            this.Passenger_id = Passg_Id;
            this.Reservation_Num = rese_Num;

        }
    }
}
