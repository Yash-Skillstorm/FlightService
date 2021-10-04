using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public interface IBookingDAO
    {
        public void AddBooking(Booking Booking);

        public IEnumerable<Booking> GetBooking();
    }
}
