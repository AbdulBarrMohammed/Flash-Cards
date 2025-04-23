using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FlashCards.Data;
using FlashCards.Models;

namespace FlashCards.Controller
{
    public class StudySessionController
    {
        public void InsertSession()
        {

        }

        public void DeleteSession()
        {

        }

        public void DisplayAllSessions()
        {

        }

        public void StartStudySession(int stackId)
        {

            using (var connection = new SqlConnection(MockDatabase.GetConnectionString()))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT Question, Answer FROM FlashCard WHERE StackId = @stackId";
                selectCmd.Parameters.AddWithValue("@stackId", stackId);
                var flashCardStack = new List<FlashCard>();

                using (var reader = selectCmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No Flash Cards found.");
                        return;
                    }

                    Console.WriteLine("Flash Cards:");
                    while (reader.Read())
                    {
                        flashCardStack.Add(
                            new FlashCard(1, stackId, reader.GetString(0), reader.GetString(1))
                        );

                        // insert reader params to new FlashCard obkject
                        string question = reader.GetString(0);
                        string answer = reader.GetString(1);
                        Console.WriteLine($"- {question}: {answer}");
                    }
                }

                connection.Close();
            }

        }
    }
}
