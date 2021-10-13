using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public class ReferanceTableData
    {
        public IEnumerable<ActiveFlight> GetActiveFlightTable()
        {
            List<ActiveFlight> flightList = new List<ActiveFlight>();

            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[GetAllActiveFlights]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        flightList.Add(new ActiveFlight
                        {
                            Flight_Number = Convert.ToInt32(reader["flight_Number"]),
                            Id = Convert.ToInt32(reader["activeflight_Id"])
                        });

                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all activeflights \n{0}", ex.Message);
                }

            }
            return flightList;
        }

        public IEnumerable<Passenger> GetPassengerTable()
        {
            List<Passenger> PassengerList = new List<Passenger>();

            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[GetAllPassengers]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        PassengerList.Add(new Passenger
                        {
                            Passenger_Name = reader["Passenger_Name"].ToString(),
                            Id = Convert.ToInt32(reader["Passenger_Id"])
                        });
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all passengers \n{0}", ex.Message);
                }

            }
            return PassengerList;
        }

        public IEnumerable<Flight> GetFlightsTable()
        {
            List<Flight> flightList = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[GetFlightsWithActiveFlights]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Flight temp = new Flight(
                            Convert.ToInt32(reader["flight_Number"]),
                            reader["Departure_Airport"].ToString(),
                            reader["Arrival_Airport"].ToString(),
                            Convert.ToDateTime(reader["Departure_Date"]),
                            Convert.ToDateTime(reader["Arrival_Date"]),
                            TimeSpan.Parse(reader["Departure_Time"].ToString()),
                            TimeSpan.Parse(reader["Arrival_Time"].ToString()),
                            Convert.ToInt32(reader["Seat_Capacity"]));
                        temp.Id = Convert.ToInt32(reader["flight_Id"]);
                        flightList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all Flights with Active flights \n{0}", ex.Message);
                }

            }
            return flightList;
        }

        public List<SeatCapacity> CheckSeatCapacityTable(int Id, int flag)
        {
            List<SeatCapacity> TotalDataAvailable = new List<SeatCapacity>();
            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "";
                if (flag == 1)
                {
                    sql = "[dbo].[GetSingleFlightBookingCount]";
                }
                else
                {
                    sql = "[dbo].[GetSinglePassengerBookingCount]";
                }

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", Id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TotalDataAvailable.Add(new SeatCapacity
                        {
                            //seatCapacity_FlightID = Convert.ToInt32(reader["Flight_Id"]),
                            TotalCount = Convert.ToInt32(reader["TotalCount"])
                        });
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return TotalDataAvailable;
        }
    }
}
