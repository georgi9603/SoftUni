using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using AdoNetExercises;

namespace Problem7
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string selectSql = "SELECT Name FROM Minions";

                using (SqlCommand command = new SqlCommand(selectSql,connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<string> minionNames = new List<string>();
                        while (reader.Read())
                        {
                            if ((string)reader[0] == null || (string)reader[0] == "")
                            {
                                continue;
                            }
                            minionNames.Add((string)reader[0]);
                        }

                        for (int i = 0; i < minionNames.Count / 2; i++)
                        {
                            Console.WriteLine(minionNames[i]);
                            Console.WriteLine(minionNames[minionNames.Count - i - 1]);
                        }

                        if (minionNames.Count % 2 != 0)
                        {
                            Console.WriteLine(minionNames[minionNames.Count/2]);
                        }

                    }
                }
            }



        }
    }
}
