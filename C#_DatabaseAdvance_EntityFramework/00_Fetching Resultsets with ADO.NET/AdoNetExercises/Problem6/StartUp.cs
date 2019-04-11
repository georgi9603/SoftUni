using System;
using System.Data.SqlClient;
using AdoNetExercises;

namespace Problem6
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int villainId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string villainName = "";

                string selectVillainSql = @"SELECT Name FROM Villains WHERE Id = @villainId";

                using (SqlCommand command = new SqlCommand(selectVillainSql,connection))
                {
                    command.Parameters.AddWithValue("@villainId", villainId);
                    villainName = (string)command.ExecuteScalar();
                }

                if (villainName == null)
                {
                    Console.WriteLine("No such villain was found.");
                    return;
                }

                Console.WriteLine($"{villainName} was deleted.");

                string deleteMinionsVillainSql = @"DELETE FROM MinionsVillains 
                WHERE VillainId = @villainId";

                using (SqlCommand command = new SqlCommand(deleteMinionsVillainSql,connection))
                {
                    command.Parameters.AddWithValue("@villainId", villainId);
                    int minionsReleased = command.ExecuteNonQuery();

                    Console.WriteLine($"{minionsReleased} minions were released.");
                }

                string deleteVillainSql = @"DELETE FROM Villains
      WHERE Id = @villainId";

                using (SqlCommand command = new SqlCommand(deleteVillainSql,connection))
                {
                    command.Parameters.AddWithValue("@villainId", villainId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
