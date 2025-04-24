using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlashCards.Controller;
using Spectre.Console;
using static FlashCards.Enums.Enums;

namespace FlashCards
{
    public class UserInterface
    {
        private StackController stackController = new();
        private FlashCardController flashCardController = new();
        private StudySessionController studySessionController = new();

        internal void MainMenu()
        {
            bool isOn = true;
            while (isOn)
            {
                Console.Clear();
                var actionChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuAction>().Title("Select an action:")
                .PageSize(10)
                .UseConverter(action =>
                    System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                        action.ToString().Replace("_", " ").ToLower()))
                .AddChoices(Enum.GetValues<MenuAction>()));


                switch(actionChoice)
                {
                    case MenuAction.Exit:
                        isOn = false;
                        break;
                    case MenuAction.Manage_Stacks:
                        ManageStacks();
                        break;
                    case MenuAction.Manage_Flashcards:
                        ViewFlashCards();
                        break;
                    case MenuAction.Study:
                        StudyStackMenu();
                        break;
                    case MenuAction.view_study_session_data:
                        ViewStudySessionData();
                        break;
                }

                // If program is still on let user enter any key to continue program to main menu
                if (isOn)
                {
                    // Wait for user input before continuing
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }

            }
        }

        public void ManageStacks()
        {
            // Display different stacks to user
            stackController.DisplayAllStacks();
            Console.WriteLine("---------------------------");
            Console.WriteLine("Input a current stack name");
            Console.WriteLine("Or input 0 to exit input");
            Console.WriteLine("---------------------------");
            var stackName = Console.ReadLine();


            // Pass stack name to function to display stack
            SelectStackItem(stackName);

        }


        public void SelectStackItem(string stackName)
        {
            // Get stack id from stackName
            int stackId = stackController.GetStackId(stackName);

            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Current working stack: {stackName}\n\n");

            Console.WriteLine("O to return to main menu");
            Console.WriteLine("X to change current stack");
            Console.WriteLine("V to view all Flashcards in stack");
            Console.WriteLine("A to view X amount of cards in stack");
            Console.WriteLine("C to Create a Flashcard in current stack");
            Console.WriteLine("E to Edit a Flashcard");
            Console.WriteLine("D to Delete a Flashcard");
            Console.WriteLine("------------------------------------\n");

            var option = Console.ReadLine();
            switch(option)
            {
                case "O":
                    MainMenu();
                    break;
                case "X":
                    ManageStacks();
                    break;
                case "V":
                    DisplayAllFlashCardsInStack(stackId);
                    break;
                case "A":
                    break;
                case "C":
                    InsertFlashCard(stackId);
                    break;
                case "E":
                    EditFlashCard();
                    break;
                case "D":
                    RemoveFlashCard();
                    break;
            }

        }

        public void DisplayAllFlashCardsInStack(int stackId)
        {
            stackController.DisplayAllStackCards(stackId);
            Console.WriteLine("Select card id to interact with card");
            Console.WriteLine("press 0 to exit");

        }

        public void InsertFlashCard(int stackId)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Enter flashcard question: \n");
            var question = Console.ReadLine();

            Console.WriteLine("Enter flashcard answer");
            var answer = Console.ReadLine();
            Console.WriteLine("------------------------------------");

            flashCardController.CreateFlashCard(stackId, question, answer);

        }

        public void RemoveFlashCard()
        {

            flashCardController.DeleteFlashCard();
        }

        public void EditFlashCard()
        {
            flashCardController.UpdateFlashCard();
        }

        public void StudyStackMenu()
        {
            // Display different stacks to user
            stackController.DisplayAllStacks();
            Console.WriteLine("---------------------------");
            Console.WriteLine("Choose a stack of flashcards to interact with: ");
            Console.WriteLine("Or input 0 to exit input");
            Console.WriteLine("---------------------------");
            var stackName = Console.ReadLine();
            Study(stackName);
        }

        public void Study(string stackName)
        {
            // Get stack id from stackName
            int stackId = stackController.GetStackId(stackName);
            studySessionController.StartStudySession(stackId);
        }

        public void ViewStudySessionData()
        {
            studySessionController.DisplayAllSessions();
        }

        public void ViewFlashCards()
        {
            Console.WriteLine("Input an Id of a flashcard");
            flashCardController.DisplayAllFlashCards();
        }
    }
}
