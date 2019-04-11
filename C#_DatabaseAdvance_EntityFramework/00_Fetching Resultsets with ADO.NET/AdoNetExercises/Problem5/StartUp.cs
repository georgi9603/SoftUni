using AdoNetExercises;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Problem5
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string country = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string updateCommandSql = @"UPDATE Towns
   SET Name = UPPER(Name)
 WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

                int affectedTowns = -1;

                using (SqlCommand command = new SqlCommand(updateCommandSql, connection))
                {
                    command.Parameters.AddWithValue("@countryName", country);
                    
                    affectedTowns = command.ExecuteNonQuery();
                    
                }

                string selectCommandSql = @"SELECT t.Name 
   FROM Towns as t
   JOIN Countries AS c ON c.Id = t.CountryCode
  WHERE c.Name = @countryName";


                using (SqlCommand command = new SqlCommand(selectCommandSql, connection))
                {

                    List<string> townsList = new List<string>();

                    command.Parameters.AddWithValue("@countryName", country);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            townsList.Add((string)reader[0]);
                        }
                    }
                    if (affectedTowns < 1)
                    {
                        Console.WriteLine("No town names were affected.");
                    }
                    else
                    {
                        Console.WriteLine($"{affectedTowns} town names were affected.");
                        Console.WriteLine("[" + string.Join(", ", townsList) + "]");
                    }
                }
            }
        }
    }
}
