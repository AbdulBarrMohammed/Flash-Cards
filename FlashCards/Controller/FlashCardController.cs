using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FlashCards.Data;

namespace FlashCards.Controller
{
    public class FlashCardController
    {

        public void DeleteFlashCard()
        {

        }

        public void CreateFlashCard(int stackId, string question, string answer)
        {

            using (var connection = new SqlConnection(MockDatabase.GetConnectionString()))
            {
                connection.Open();

                //Provide stackId of which flash card will belong to as well as question and answer
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"INSERT INTO FlashCard (StackId, Question, Answer) VALUES (@stackId, @question, @answer);";
                selectCmd.Parameters.AddWithValue("@question", question);
                selectCmd.Parameters.AddWithValue("@answer", answer);
                selectCmd.Parameters.AddWithValue("@stackId", stackId);

                // Runs the insert command
                selectCmd.ExecuteNonQuery();

                Console.WriteLine("Successfully created flashcard");

                connection.Close();
            }

        }

        public void UpdateFlashCard()
        {

        }

        public void DisplayAllFlashCards()
        {

        }
    }
}
