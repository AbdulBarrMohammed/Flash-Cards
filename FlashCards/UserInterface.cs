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
                        Console.WriteLine("Exit was clicked");
                        isOn = false;
                        break;
                    case MenuAction.Manage_Stacks:
                        ManageStacks();
                        break;
                    case MenuAction.Manage_Flashcards:
                        //ManageFlashCards();
                        ViewFlashCards();
                        break;
                    case MenuAction.Delete_Stack:
                        DeleteStack();
                        break;
                    case MenuAction.Edit_Stack:
                        EditStack();
                        break;
                    case MenuAction.Add_Stack:
                        AddStack();
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
                else {
                    break;
                }

            }
        }


        public void SelectStackItem(string stackName)
        {
            // Get stack id from stackName
            int stackId = stackController.GetStackId(stackName);

            // if stackId is returened to be -1 then ask user to input another stack name
            if (stackId == -1) ManageStacks();
            else
            {

                Console.WriteLine("------------------------------------");
                Console.WriteLine($"Current working stack: {stackName}\n\n");
                Console.WriteLine("O to return to main menu");
                Console.WriteLine("X to change current stack");
                Console.WriteLine("V to view all Flashcards in stack");
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
                    case "C":
                        InsertFlashCard(stackId);
                        break;
                    case "E":
                        EditFlashCard();
                        break;
                    case "D":
                        RemoveFlashCard();
                        break;
                    default:
                        Console.WriteLine("\nTheir is no option for letter input\n");
                        break;
                }
            }


        }

        public void ManageStacks()
        {
            // Display different stacks to user
            stackController.DisplayAllStacks();
            Console.WriteLine("\n---------------------------");
            Console.WriteLine("Input a current stack name");
            Console.WriteLine("Or input 0 to exit input");
            Console.WriteLine("---------------------------\n");
            var stackName = Console.ReadLine();

            // If user input is 0 return to main menu
            if (stackName == "0")
            {
                return;
            }

            else
            {
                Study(stackName);

                // Pass stack name to function to display stack
                SelectStackItem(stackName);
            }


        }

        public void DisplayAllFlashCardsInStack(int stackId)
        {
            stackController.DisplayAllStackCards(stackId);
            
        }

        public void InsertFlashCard(int stackId)
        {

            flashCardController.CreateFlashCard(stackId);

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

            // If user input is 0 return to main menu
            if (stackName == "0") return;

            else Study(stackName);
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
            Console.WriteLine("\nInput an Id of a flashcard\n");
            flashCardController.DisplayAllFlashCards();
            var id = Console.ReadLine();
            int cardId;
            if (!Int32.TryParse(id, out cardId)) {
                Console.WriteLine("\nPlease enter a number\n");
                ViewFlashCards();
            }
            else {
                SelectFlashCard(cardId);
            }

        }

        public void SelectFlashCard(int id)
        {
            flashCardController.DisplayFlashCard(id);
        }

        public void DeleteStack()
        {

            stackController.DeleteFromStack();

        }

        public void EditStack()
        {
            stackController.UpdateStack();
        }

        public void AddStack()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Add name of new stack: ");
            Console.WriteLine("Or input 0 to exit input");
            Console.WriteLine("---------------------------");
            var stackName = Console.ReadLine();

            // If user input is 0 return to main menu
            if (stackName == "0") return;
            else stackController.InsertToStack(stackName);


        }
    }
}
