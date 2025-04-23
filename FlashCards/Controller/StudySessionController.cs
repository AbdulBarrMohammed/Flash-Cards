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
        public void InsertSession(int score, int stackId)
        {
            using (var connection = new SqlConnection(MockDatabase.GetConnectionString()))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = @"
                    INSERT INTO StudySession (StackId, Date, Score)
                    VALUES (@StackId, @Date, @Score);";

                string date = DateTime.Now.ToString("MMMM dd, yyyy");
                selectCmd.Parameters.AddWithValue("@Date", date);
                selectCmd.Parameters.AddWithValue("@Score", score);
                selectCmd.Parameters.AddWithValue("@StackId", stackId);

                selectCmd.ExecuteNonQuery();

                connection.Close();

            }



        }

        public void DeleteSession()
        {

        }

        public void DisplayAllSessions(int Id)
        {

            using (var connection = new SqlConnection(MockDatabase.GetConnectionString()))
            {
                connection.Open();

                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT Id, StackId, Date, Score FROM StudySession WHERE Id = @Id";
                selectCmd.Parameters.AddWithValue("@Id", Id);

                var studySessionDataList = new List<StudySession>();

                using (var reader = selectCmd.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No Study Sessions found.");
                        return;
                    }

                    Console.WriteLine("Study Sessions:");
                    while (reader.Read())
                    {
                        // Insert new flashcard object to flashcards stack list
                        studySessionDataList.Add(
                            new StudySession(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3))
                        );

                    }


                    // Loop through flashcards stack list
                        Console.WriteLine("\n Study Session Cards: \n");
                        foreach(var s in studySessionDataList)
                        {
                            Console.WriteLine("\n-------------------------------\n");
                            Console.WriteLine($"\nDate: {s.Date}\n");
                            Console.WriteLine($"\nScore: {s.Score}\n");
                            Console.WriteLine("\n-------------------------------\n");
                            Console.WriteLine("\n\nInput your answer to this card\n\n");

                        }

                }

                connection.Close();
            }

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

                    }


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

                    // Insert study session into database
                    InsertSession(currentScore, stackId);
                    Console.WriteLine($"Your score is {currentScore}/{flashCardStack.Count}");
                }

                connection.Close();
            }

        }
    }
}
