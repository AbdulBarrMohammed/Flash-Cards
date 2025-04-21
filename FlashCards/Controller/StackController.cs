using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FlashCards.Data;
using FlashCards.Models;

namespace FlashCards.Controller
{
    public class StackController
    {

        public void InsertToStack()
        {

        }

        public void DeleteFromStack()
        {

        }

        public void UpdateStack()
        {

        }

        public void DisplayAllStacks()
        {

            using (var connection = new SqlConnection(MockDatabase.GetConnectionString()))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT Name FROM Stack";

                using (var reader = selectCmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No stacks found.");
                        return;
                    }

                    Console.WriteLine("Stacks:");
                    while (reader.Read())
                    {
                        string name = reader.GetString(0);
                        Console.WriteLine($"- {name}");
                    }
                }

                connection.Close();
            }
        }
    }
}
