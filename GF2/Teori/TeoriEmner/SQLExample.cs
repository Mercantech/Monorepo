using System;
using Npgsql;

namespace Teori.SQL
{
    public class SQLExample
    {
        public static void Run()
        {
            Console.WriteLine("\n=== PostgreSQL Eksempel ===");
            Console.WriteLine(
                "SQL i .NET har følgende format: Host=localhost;Username=postgres;Password=postgres;Database=postgres"
            );
            Console.Write("Indtast connection string (eller tryk Enter for at bruge standard): ");
            string connString = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(connString))
            {
                // Standard connection string (tilpas til din database)
                connString = "Host=localhost;Username=postgres;Password=postgres;Database=postgres";
            }

            try
            {
                using (var conn = new NpgsqlConnection(connString))
                {
                    conn.Open();
                    Console.WriteLine("Forbundet til databasen!");

                    // Simpel forespørgsel
                    using (var cmd = new NpgsqlCommand("SELECT version();", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"PostgreSQL version: {reader.GetString(0)}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fejl ved forbindelse eller forespørgsel: " + ex.Message);
            }

            Console.WriteLine("Tryk på en tast for at vende tilbage til hovedmenuen...");
            Console.ReadKey();
        }
    }
}
