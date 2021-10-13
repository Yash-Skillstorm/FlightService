using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public class FlightDAO : IFlightDAO
    {
        public IEnumerable<Flight> GetFlights()
        {
            List<Flight> flightList = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[GetAllFlight]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Flight temp = new Flight(
                            Convert.ToInt32(reader[ConstantString.FlightName]),
                            reader[ConstantString.DepartureAirport].ToString(),
                            reader[ConstantString.ArrivalAirport].ToString(),
                            Convert.ToDateTime(reader[ConstantString.DepartureDate]),
                            Convert.ToDateTime(reader[ConstantString.ArrivalDate]),
                            TimeSpan.Parse(reader[ConstantString.DepartureTime].ToString()),
                            TimeSpan.Parse(reader[ConstantString.ArrivalTime].ToString()),
                            Convert.ToInt32(reader[ConstantString.SeatCapacity]));
                        temp.Id = Convert.ToInt32(reader[ConstantString.FlightId]);
                        flightList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all flights \n{0}", ex.Message);
                }

            }
            return flightList;
        }

        public Flight GetFlight(int id)
        {
            Flight singleFlight = new Flight();

            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[GetFlightByID]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        singleFlight = new Flight(
                            Convert.ToInt32(reader[ConstantString.FlightName]),
                            reader[ConstantString.DepartureAirport].ToString(),
                            reader[ConstantString.ArrivalAirport].ToString(),
                            Convert.ToDateTime(reader[ConstantString.DepartureDate]),
                            Convert.ToDateTime(reader[ConstantString.ArrivalDate]),
                            TimeSpan.Parse(reader[ConstantString.DepartureTime].ToString()),
                            TimeSpan.Parse(reader[ConstantString.ArrivalTime].ToString()),
                            Convert.ToInt32(reader[ConstantString.SeatCapacity]));                        
                        singleFlight.Id = Convert.ToInt32(reader[ConstantString.FlightId]);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get single flight \n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            return singleFlight;
        }

        public void AddFlight(Flight flight)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[AddNewFlight]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flight_Num", flight.Flight_Num);
                cmd.Parameters.AddWithValue("@Departure_Date", flight.Departure_Date);
                cmd.Parameters.AddWithValue("@Arrival_Date", flight.Arrival_Date);
                cmd.Parameters.AddWithValue("@Departure_Time", flight.Departure_Time);
                cmd.Parameters.AddWithValue("@Arrival_Time", flight.Arrival_Time);
                cmd.Parameters.AddWithValue("@Departure_Airport", flight.Departure_Airport);
                cmd.Parameters.AddWithValue("@Arrival_Airport", flight.Arrival_Airport);
                cmd.Parameters.AddWithValue("@Seat_Capacity", flight.Seat_Capacity);
                cmd.Parameters.Add("@Flight_Id", SqlDbType.Int).Direction = ParameterDirection.Output;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    id = (int)cmd.Parameters["@Flight_Id"].Value;
                    flight.Id = id;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not add flight\n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void UpdateFlight(Flight flight)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[UpdateFlightByID]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flight_Id", flight.Id);
                cmd.Parameters.AddWithValue("@Flight_Num", flight.Flight_Num);
                cmd.Parameters.AddWithValue("@Departure_Date", flight.Departure_Date);
                cmd.Parameters.AddWithValue("@Arrival_Date", flight.Arrival_Date);
                cmd.Parameters.AddWithValue("@Departure_Time", flight.Departure_Time);
                cmd.Parameters.AddWithValue("@Arrival_Time", flight.Arrival_Time);
                cmd.Parameters.AddWithValue("@Departure_Airport", flight.Departure_Airport);
                cmd.Parameters.AddWithValue("@Arrival_Airport", flight.Arrival_Airport);
                cmd.Parameters.AddWithValue("@Seat_Capacity", flight.Seat_Capacity);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not update flight \n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeleteFlight(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[DeleteFlightByID]";
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
                    Console.WriteLine("Could not delete flight \n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
