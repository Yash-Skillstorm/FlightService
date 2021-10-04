using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public class BookingDAO : IBookingDAO
    {        
        public void AddBooking(Booking Booking)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Booking> GetBooking()
        {
            List<Booking> bookingList = new List<Booking>();

            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * from dbo.BookingTable;", conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Booking temp = new Booking(
                            Convert.ToInt32(reader["flight_Id"]),
                             Convert.ToInt32(reader["Passenger_Id"]),
                            Convert.ToInt32(reader["Reservation_Number"]));
                            
                        temp.Id = Convert.ToInt32(reader["Booking_Id"]);
                        bookingList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all homes \n{0}", ex.Message);
                }

            }
            return bookingList;
        }
    }
}
