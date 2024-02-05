using System.IO;
using System.Threading;
using Spectre.Console;

namespace Habit_Tracker;

class Program
{
    static void Main(string[] args)
    {
        if (!File.Exists("Habit.db"))
        {
            AnsiConsole.MarkupLine("[underline blue]Database not found. Would you like to create one[/]\n");

            var input = AnsiConsole.Prompt
            (
                new SelectionPrompt<string>()
                .AddChoices("Yes", "No")
            );

            if (input == "Yes")
            {
                File.Create("Habit.db");

                AnsiConsole.MarkupLine("[bold green]Database created![/]");
            }

            else
            {
                AnsiConsole.MarkupLine("[underline red]Can not continue without a database. Now exiting[/]");
                Thread.Sleep(1000);
                return;
            }
        }

        User user = new();

        user.InputLoop();
    }
}