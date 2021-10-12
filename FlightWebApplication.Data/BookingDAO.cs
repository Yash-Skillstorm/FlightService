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
                SqlCommand cmd = new SqlCommand("select * from dbo.BookingTable as B, dbo.FlightTable as F, dbo.PassengerTable as P, dbo.ActiveFlights as AF Where F.flight_Id = B.Flight_Id AND P.passenger_Id = b.Passenger_Id And AF.activeflight_Id = F.flight_Num; ", conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Booking temp = new Booking(
                            Convert.ToInt32(reader["flight_Id"]),
                            Convert.ToInt32(reader["flight_Number"]),
                             Convert.ToInt32(reader["Passenger_Id"]),
                             reader["Passenger_Name"].ToString(),
                            Convert.ToInt32(reader["Reservation_Number"]),
                            reader["Departure_Airport"].ToString(),
                            reader["Arrival_Airport"].ToString());
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
                           Convert.ToInt32(reader["flight_Id"]),
                            Convert.ToInt32(reader["flight_Number"]),
                             Convert.ToInt32(reader["Passenger_Id"]),
                             reader["Passenger_Name"].ToString(),
                            Convert.ToInt32(reader["Reservation_Number"]),
                            reader["Departure_Airport"].ToString(),
                            reader["Arrival_Airport"].ToString());
                        singleBooking.Id = Convert.ToInt32(reader["Booking_Id"]);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get single homes \n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return singleBooking;
        }

        public void DeleteBooking(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[DeleteBookingByID]";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get single homes \n{0}", ex.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public int AddBooking(Booking booking)
        {
            List<SeatCapacity> model = GetTableData.CheckSeatCapacityTable(booking.Flight_Id,1);
            var flag = false;
            var TotalSeatCapacity = 0;
            if (model.Count != 0)
            {
                IEnumerable<Flight> modFlight = GetTableData.GetFlightsTable();
                foreach(var item in modFlight)
                {
                    if(item.Id == booking.Flight_Id)
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
                        Console.WriteLine("Could not add Homes\n{0}", ex.Message);
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

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
