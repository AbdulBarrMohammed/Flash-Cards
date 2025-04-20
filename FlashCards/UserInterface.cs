using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;
using static FlashCards.Enums.Enums;

namespace FlashCards
{
    public class UserInterface
    {
        internal void MainMenu()
        {
            while (true)
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
                        Console.WriteLine("Exit was chosen");
                        //AddCodeItems();
                        break;
                    case MenuAction.Manage_Stacks:
                        Console.WriteLine("Manage Stacks was chosen");
                        //ViewCodeItems();
                        break;
                    case MenuAction.Manage_Flashcards:
                        Console.WriteLine("Manage Flashcards was chosen");
                        //RemoveCodeItem();
                        break;
                    case MenuAction.view_study_session_data:
                        Console.WriteLine("View study session was chosen");
                        break;
                }

                // Wait for user input before continuing
                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
            }
        }
    }
}
