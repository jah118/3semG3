﻿using DbUp;
using System;
using System.Linq;
using System.Reflection;

namespace RestaurantUpdater
{
    class Program
    {
        //Code Snippet taken from DbUp documentation with alterations to EnsureDatabase find docs at: https://dbup.readthedocs.io/en/latest/
        private static void Main(string[] args)
        {
            var connectionString =
                args.FirstOrDefault()
                ?? @"Data Source=(localdb)\mssqllocaldb; Initial Catalog=Restaurant; Integrated Security=true";
            //"Server=(local)\\SqlExpress; Database=Restaurant; Trusted_connection=true";
            UpdateDatabase(connectionString);
        }

        public static void UpdateDatabase(string connectionString)
        {
            DropDatabase.For.SqlDatabase(connectionString);
            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Success!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
#if DEBUG
                Console.ReadLine();
#endif
            }
        }
    }
}
