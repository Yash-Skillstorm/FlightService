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
        public IEnumerable<Passenger> GetPassengers()
        {
            List<Passenger> passengerList = new List<Passenger>();

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
                        Passenger temp = new Passenger(
                            reader[ConstantString.PassengerName].ToString(),
                            Convert.ToInt32(reader[ConstantString.PassengerAge]),
                            reader[ConstantString.PassengerEmail].ToString());
                        temp.Id = Convert.ToInt32(reader[ConstantString.PassengerId]);
                        passengerList.Add(temp);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get all passengers \n{0}", ex.Message);
                }

            }
            return passengerList;
        }

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
                        singlePassenger = new Passenger(reader[ConstantString.PassengerName].ToString(),
                            Convert.ToInt32(reader[ConstantString.PassengerAge]),
                            reader[ConstantString.PassengerEmail].ToString());
                        singlePassenger.Id = Convert.ToInt32(reader[ConstantString.PassengerId]);
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not get single passenger \n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }

            }
            return singlePassenger;
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
                    Console.WriteLine("Could not add passenger\n{0}", ex.Message);
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

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Could not update passenger \n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void DeletePassenger(int id)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionStringClass.GetConnectionString()))
            {
                string sql = "[dbo].[DeletePassengerByID]";
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
                    Console.WriteLine("Could not delete passenger \n{0}", ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
