using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public interface IBookingDAO
    {
        public IEnumerable<Booking> GetBookings();
        public Booking GetBooking(int id);
        public int AddBooking(Booking Booking);
        public void DeleteBooking(int id);       
        public void UpdateBooking(Booking Booking);
    }
}
