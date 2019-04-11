using System;
using System.Data.SqlClient;
using System.Linq;
using AdoNetExercises;

namespace Problem8
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int[] minionsId = Console.ReadLine().Split().Select(x => int.Parse(x)).ToArray();

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                UpdateMinionsNameById(minionsId, connection);

                PrintMinionsFromDB(connection);
            }
        }

        private static void UpdateMinionsNameById(int[] minionsId, SqlConnection connection)
        {
            string updateSql = @"UPDATE Minions
   SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
 WHERE Id = @Id";

            foreach (var id in minionsId)
            {
                using (SqlCommand command = new SqlCommand(updateSql, connection))
                {

                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();

                }
            }
        }

        private static void PrintMinionsFromDB(SqlConnection connection)
        {
            string selectSql = @"SELECT Name, Age FROM Minions";

            using (SqlCommand command = new SqlCommand(selectSql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader[0].ToString() == null || reader[0].ToString() == "")
                        {
                            continue;
                        }
                        string minionName = (string)reader[0];
                        int minionAge = (int)reader[1];

                        Console.WriteLine($"{minionName} {minionAge}");
                    }
                }
            }
        }
    }
}
