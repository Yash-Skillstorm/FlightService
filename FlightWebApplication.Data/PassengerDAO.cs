using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightWebApplication.Data
{
    public class PassengerDAO : IPassengerDAO
    {
        public Passenger GetPassenger(int id)
        {

            Passenger singlePassenger = new Passenger();

            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[GetPassengerByID]";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        singlePassenger = new Passenger(reader["Passenger_Name"].ToString(),
                           Convert.ToInt32(reader["Passenger_Age"]),
                           reader["Passenger_Email"].ToString());
                        singlePassenger.Id = Convert.ToInt32(reader["Passenger_Id"]);
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
            return singlePassenger;
        }

        public IEnumerable<Passenger> GetPassengers()
        {
            List<Passenger> passengerList = new List<Passenger>();

            //using statement close the connection if its failed to execute the query
            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * from dbo.PassengerTable;", conn);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Passenger temp = new Passenger(reader["Passenger_Name"].ToString(),
                           Convert.ToInt32(reader["Passenger_Age"]),
                           reader["Passenger_Email"].ToString());
                        temp.Id = Convert.ToInt32(reader["Passenger_Id"]);
                        passengerList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all homes \n{0}", ex.Message);
                }

            }
            return passengerList;
        }

        public void DeletePassenger(int id)
        {
            using (SqlConnection con = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[DeletePassengerByID]";
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

        public void AddPassenger(Passenger passenger)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[AddNewPassenger]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Passenger_Name", passenger.Passenger_Name);
                cmd.Parameters.AddWithValue("@Passenger_Age", passenger.Passenger_Age);
                cmd.Parameters.AddWithValue("@Passenger_Email", passenger.Passenger_Email);
                cmd.Parameters.Add("@Passenger_Id", SqlDbType.Int).Direction = ParameterDirection.Output;

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    id = (int)cmd.Parameters["@Passenger_Id"].Value;
                    passenger.Id = id;
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

        public void UpdatePassenger(Passenger passenger)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[UpdatePassengerByID]";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Passenger_Id", passenger.Id);
                cmd.Parameters.AddWithValue("@Passenger_Name", passenger.Passenger_Name);
                cmd.Parameters.AddWithValue("@Passenger_Age", passenger.Passenger_Age);
                cmd.Parameters.AddWithValue("@Passenger_Email", passenger.Passenger_Email);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
