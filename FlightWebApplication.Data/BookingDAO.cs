using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public class BookingDAO : IBookingDAO
    {
        ReferanceTableData GetTableData = new ReferanceTableData();
        public IEnumerable<Booking> GetBookings()
        {
            List<Booking> bookingList = new List<Booking>();

            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[GetAllBooking]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Booking temp = new Booking(
                            Convert.ToInt32(reader[ConstantString.FlightId]),
                            Convert.ToInt32(reader[ConstantString.ActiveFlightNumber]),
                            Convert.ToInt32(reader[ConstantString.PassengerId]),
                            reader[ConstantString.PassengerName].ToString(),
                            Convert.ToInt32(reader[ConstantString.ReservationNumber]),
                            reader[ConstantString.DepartureAirport].ToString(),
                            reader[ConstantString.ArrivalAirport].ToString());
                        temp.Id = Convert.ToInt32(reader[ConstantString.BookingId]);
                        bookingList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all bookings \n{0}", ex.Message);
                }

            }
            return bookingList;
        }

        public Booking GetBooking(int id)
        {
            Booking singleBooking = new Booking();

            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[GetBookingByID]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        singleBooking = new Booking(
                            Convert.ToInt32(reader[ConstantString.FlightId]),
                            Convert.ToInt32(reader[ConstantString.ActiveFlightNumber]),
                            Convert.ToInt32(reader[ConstantString.PassengerId]),
                            reader[ConstantString.PassengerName].ToString(),
                            Convert.ToInt32(reader[ConstantString.ReservationNumber]),
                            reader[ConstantString.DepartureAirport].ToString(),
                            reader[ConstantString.ArrivalAirport].ToString());
                        singleBooking.Id = Convert.ToInt32(reader[ConstantString.BookingId]);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get single booking \n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return singleBooking;
        }

        public int AddBooking(Booking booking)
        {
            List<SeatCapacity> model = GetTableData.CheckSeatCapacityTable(booking.Flight_Id, 1);
            var flag = false;
            var TotalSeatCapacity = 0;
            if (model.Count != 0)
            {
                IEnumerable<Flight> modFlight = GetTableData.GetFlightsTable();
                foreach (var item in modFlight)
                {
                    if (item.Id == booking.Flight_Id)
                    {
                        TotalSeatCapacity = item.Seat_Capacity;
                    }
                }
                if (model[0].TotalCount < TotalSeatCapacity)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            else
            {
                flag = true;
            }
            if (flag)
            {
                Random rd = new Random();
                int rand_num = rd.Next(1000000, 10000000);
                int id = 0;
                using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
                {
                    string sql = "[dbo].[AddNewBooking]";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flight_Id", booking.Flight_Id);
                    cmd.Parameters.AddWithValue("@Passenger_Id", booking.Passenger_id);
                    cmd.Parameters.AddWithValue("@Reservation_Number", rand_num);
                    cmd.Parameters.Add("@Booking_Id", SqlDbType.Int).Direction = ParameterDirection.Output;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        id = (int)cmd.Parameters["@Booking_Id"].Value;
                        booking.Id = id;
                        return 1;
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Could not add Booking\n{0}", ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
            return 0;
        }

        public void UpdateBooking(Booking booking)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[UpdateBookingByID]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Booking_Id", booking.Id);
                cmd.Parameters.AddWithValue("@Flight_Id", booking.Flight_Id);
                cmd.Parameters.AddWithValue("@Passenger_Id", booking.Passenger_id);
                cmd.Parameters.AddWithValue("@Reservation_Number", booking.Reservation_Num);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not update booking \n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeleteBooking(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[DeleteBookingByID]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not delete booking \n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
