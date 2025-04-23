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
                selectCmd.CommandText = "SELECT Id, StackId, Question, Answer FROM FlashCard WHERE StackId = @stackId";
                selectCmd.Parameters.AddWithValue("@stackId", stackId);
                var flashCardStack = new List<FlashCard>();

                using (var reader = selectCmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No Flash Cards found.");
                        return;
                    }

                    int currentScore = 0;

                    Console.WriteLine("Flash Cards:");
                    while (reader.Read())
                    {
                        // Insert new flashcard object to flashcards stack list
                        flashCardStack.Add(
                            new FlashCard(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetString(3))
                        );


                        // Loop through flashcards stack list
                        Console.WriteLine("\n Study Session Cards: \n");
                        foreach(var card in flashCardStack)
                        {
                            Console.WriteLine("\n-------------------------------\n");
                            Console.WriteLine($"\nQuestion: {card.Question}\n");
                            Console.WriteLine("\n-------------------------------\n");
                            Console.WriteLine("\n\nInput your answer to this card\n\n");
                            var userAnswer = Console.ReadLine();

                            if (userAnswer.ToLower() == card.Answer.ToLower()) {
                                Console.WriteLine("\nYour answer was correct !\n");
                                currentScore += 1;
                            }
                            else {
                                Console.WriteLine("\nYour answer was wrong !\n");
                            }
                        }

                        /*
                        // insert reader params to new FlashCard obkject
                        string question = reader.GetString(0);
                        string answer = reader.GetString(1);
                        Console.WriteLine($"- {question}: {answer}"); */
                    }

                    Console.WriteLine($"Your score is {currentScore}/{flashCardStack.Count}");
                }

                connection.Close();
            }

        }
    }
}
