using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public class ReferanceTableData
    {
        public IEnumerable<ActiveFlight> GetTable()
        {
            List<ActiveFlight> flightList = new List<ActiveFlight>();

            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * from dbo.ActiveFlights;", conn);
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
                    Console.WriteLine("Could not get all homes \n{0}", ex.Message);
                }

            }
            return flightList;
        }

        public IEnumerable<Passenger> GetPassengerTable()
        {
            List<Passenger> PassengerList = new List<Passenger>();

            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * from dbo.PassengerTable;", conn);
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
                    Console.WriteLine("Could not get all homes \n{0}", ex.Message);
                }

            }
            return PassengerList;
        }

        public IEnumerable<Flight> GetFlightsTable()
        {
            List<Flight> flightList = new List<Flight>();

            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("select * from dbo.FlightTable as F Join dbo.ActiveFlights as AF on AF.activeflight_id = F.Flight_Num;", conn);
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
                            TimeSpan.Parse(reader["Arrival_Time"].ToString()));
                        temp.Id = Convert.ToInt32(reader["flight_Id"]);
                        flightList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all homes \n{0}", ex.Message);
                }

            }
            return flightList;
        }
    }
}
