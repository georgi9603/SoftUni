using System;
using System.Data.SqlClient;
using System.Linq;
using AdoNetExercises;

namespace Problem4
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string[] minionTokens = Console.ReadLine().Split().ToArray();
            string[] villainTokens = Console.ReadLine().Split().ToArray();

            string minionName = minionTokens[1];
            int minionAge = int.Parse(minionTokens[2]);
            string minionCity = minionTokens[3];

            string villainName = villainTokens[1];

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                int? townId = GetTownByName(minionCity, connection);

                if (townId == null)
                {
                    AddTown(connection, minionCity);
                }

                townId = GetTownByName(minionCity, connection);
                
                AddMinion(connection, minionName, minionAge, townId);
                
                int? villainId = GetVillainByName(connection, villainName);

                if (villainId == null)
                {
                    AddVillain(connection, villainName);
                }

                villainId = GetVillainByName(connection, villainName);
                int minionId = GetMinionId(connection, minionName);
                AddMinionVillain(connection,minionId,villainId,minionName,villainName);
            }
        }

        private static void AddMinionVillain(SqlConnection connection, int minionId, int? villainId, string minionName, string villainName)
        {
            string insertMinionVillainQuery =
                "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";

            using (SqlCommand insertCommand = new SqlCommand(insertMinionVillainQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@villainId", villainId);
                insertCommand.Parameters.AddWithValue("@minionId", minionId);

                insertCommand.ExecuteNonQuery();
            }

            Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
        }

        private static int GetMinionId(SqlConnection connection, string minionName)
        {
            string selectMinionQuery = "SELECT Id FROM Minions WHERE Name = @Name";

            using (SqlCommand selectMinionCommand = new SqlCommand(selectMinionQuery, connection))
            {
                selectMinionCommand.Parameters.AddWithValue("@Name", minionName);
                return (int)selectMinionCommand.ExecuteScalar();
            }
        }

        private static void AddVillain(SqlConnection connection, string villainName)
        {
            string insertVillainQuery =
                "INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";

            using (SqlCommand insertCommand = new SqlCommand(insertVillainQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@villainName", villainName);

                insertCommand.ExecuteNonQuery();
            }

            Console.WriteLine($"Villain {villainName} was added to the database.");
        }

        private static int? GetVillainByName(SqlConnection connection, string villainName)
        {
            string selectVillainQuery = "SELECT Id FROM Villains WHERE Name = @Name";

            using (SqlCommand selectVillainCommand = new SqlCommand(selectVillainQuery, connection))
            {
                selectVillainCommand.Parameters.AddWithValue("@Name", villainName);
                return (int?)selectVillainCommand.ExecuteScalar();
            }
        }

        private static void AddMinion(SqlConnection connection, string minionName, int minionAge, int? townId)
        {
            string insertMinionQuery =
                    "INSERT INTO Minions (Name, Age, TownId) VALUES (@name, @age, @townId)";

            using (SqlCommand insertCommand = new SqlCommand(insertMinionQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@name", minionName);
                insertCommand.Parameters.AddWithValue("@age", minionAge);
                insertCommand.Parameters.AddWithValue("@townId", townId);

                insertCommand.ExecuteNonQuery();
            }
        }

        private static int? GetTownByName(string minionCity, SqlConnection connection)
        {
            string selectTownQuery = "SELECT Id FROM Towns WHERE Name = @townName";

            using (SqlCommand selectTownCommand = new SqlCommand(selectTownQuery, connection))
            {
                selectTownCommand.Parameters.AddWithValue("@townName", minionCity);
                return (int?)selectTownCommand.ExecuteScalar();
            }
        }

        private static void AddTown(SqlConnection connection, string minionCity)
        {
            string insertTownQuery =
                "INSERT INTO Towns (Name) VALUES (@townName)";
            using (SqlCommand insertCommand = new SqlCommand(insertTownQuery, connection))
            {
                insertCommand.Parameters.AddWithValue("@townName", minionCity);
                insertCommand.ExecuteNonQuery();
                Console.WriteLine($"Town {minionCity} was added to the database.");
            }

            Console.WriteLine($"Town {minionCity} was added to the database.");
        }
    }
}
