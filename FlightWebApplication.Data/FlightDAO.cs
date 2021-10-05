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
                SqlCommand cmd = new SqlCommand("Select * from dbo.FlightTable;", conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Flight temp = new Flight(
                            Convert.ToInt32(reader["flight_Num"]),
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
                            Convert.ToInt32(reader["flight_Num"]),
                            reader["Departure_Airport"].ToString(),
                            reader["Arrival_Airport"].ToString(),
                            Convert.ToDateTime(reader["Departure_Date"]),
                            Convert.ToDateTime(reader["Arrival_Date"]),
                            TimeSpan.Parse(reader["Departure_Time"].ToString()),
                            TimeSpan.Parse(reader["Arrival_Time"].ToString()));
                        singleFlight.Id = Convert.ToInt32(reader["flight_Id"]);
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
            return singleFlight;
        }

        public void DeleteFlight(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[DeleteFlightByID]";
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
                    Console.WriteLine("Could not add Homes\n{0}", ex.Message);
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

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        //public IEnumerable<ActiveFlight> GetTable()
        //{
        //    List<ActiveFlight> flightList = new List<ActiveFlight>();

        //    using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
        //    {
        //        SqlCommand cmd = new SqlCommand("Select * from dbo.ActiveFlights;", conn);
        //        try
        //        {
        //            conn.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                flightList.Add(new ActiveFlight
        //                {       Flight_Number = Convert.ToInt32(reader["flight_Number"]),
        //                        Id = Convert.ToInt32(reader["activeflight_Id"])
        //                });
                        
        //            }
        //        }
        //        catch (SqlException ex)
        //        {
        //            Console.WriteLine("Could not get all homes \n{0}", ex.Message);
        //        }

        //    }
        //    return flightList;
        //}
       
    }
}
