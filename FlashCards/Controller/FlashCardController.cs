using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using FlashCards.Data;
using FlashCards.Dto;
using FlashCards.Models;

namespace FlashCards.Controller
{
    public class FlashCardController
    {

        public void DeleteFlashCard()
        {
            // Get flashcard id
            Console.WriteLine($"\nEnter flashcard id to delete: \n");
            string id = Console.ReadLine();
            int cardId;
            Int32.TryParse(id, out cardId);


            using (var connection = new SqlConnection(MockDatabase.GetConnectionString())) {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"DELETE from FlashCard WHERE Id = @cardId";
                tableCmd.Parameters.AddWithValue("@cardId", cardId);

                int rowCount = tableCmd.ExecuteNonQuery();
                if (rowCount == 0)
                {
                    System.Console.WriteLine($"\n\nRecord with Id {cardId} doesn't exist. \n\n");
                    DeleteFlashCard();
                }

                else
                {
                    Console.WriteLine("\n Flashcard was successfully deleted");
                    Console.WriteLine("\n Press any key to return to main menu\n");
                }


            }

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

            // Get flashcard id
            Console.WriteLine($"\nEnter flashcard id to delete: \n");
            string id = Console.ReadLine();
            int cardId;
            Int32.TryParse(id, out cardId);

            // Get new flashcard questions and answers
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"\nEnter new flashcard question: \n");
            var question = Console.ReadLine();

            Console.WriteLine("\nEnter new flashcard answer: \n");
            Console.WriteLine("------------------------------------\n");
            var answer = Console.ReadLine();

            using (var connection = new SqlConnection(MockDatabase.GetConnectionString()))
            {

                connection.Open();
                //Get new input for flashCards

                var selectCmd = connection.CreateCommand();
                // Insert updated code item properties to database
                selectCmd.CommandText = @"UPDATE FlashCard SET Question = @question, answer = @answer WHERE Id = @Id";
                selectCmd.Parameters.AddWithValue("@question", question);
                selectCmd.Parameters.AddWithValue("@answer", answer);
                selectCmd.Parameters.AddWithValue("@id", cardId);

                // Runs the insert command
                selectCmd.ExecuteNonQuery();

                Console.WriteLine("Updated Successfully");

                connection.Close();

            }

        }

        public void DisplayFlashCard(int cardId)
        {
            using (var connection = new SqlConnection(MockDatabase.GetConnectionString()))
            {

                connection.Open();

                var selectCmd = connection.CreateCommand();

                // Select flash card from sql database
                selectCmd.CommandText = @"Select from FlashCard WHERE Id = @Id";
                selectCmd.Parameters.AddWithValue("@id", cardId);

                using (var reader = selectCmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var flashCard = new FlashCardDto(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                        Console.WriteLine($"\nHere is the card you selected: \n");
                        Console.WriteLine($"Question: {flashCard.Question} \n Answer: {flashCard.Answer}");
                    }
                }




                connection.Close();

            }
        }
    }
}
