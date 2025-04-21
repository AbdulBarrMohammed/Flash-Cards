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
                        Console.WriteLine("Manage Flashcards was chosen");
                        //ManageFlashCards();
                        break;
                    case MenuAction.view_study_session_data:
                        Console.WriteLine("View study session was chosen");
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
            Console.WriteLine(stackName);

            // Get stack id from stackName
            int stackId = stackController.GetStackId(stackName);

            // Pass id to function to display stack
            SelectStackItem(stackId);

        }

        public void SelectStackItem(int stackId)
        {
            Console.WriteLine($"The name you choose has an id of {stackId}");
        }
    }
}
