using System.IO;
using Spectre.Console;
using Coding_Tracker.Input;
using System;

namespace Coding_Tracker;

class Program
{
    static void Main(string[] args)
    {
        if (!File.Exists("Tracker.db"))
        {
            AnsiConsole.MarkupLine("[underline red]No database found. Would you like to create one?[/]");

            var input = AnsiConsole.Prompt
            (
                new SelectionPrompt<string>()
                .AddChoices("Yes", "No")
            );

            if (input == "Yes")
            {
                File.Create("Tracker.db");

                AnsiConsole.MarkupLine("[bold green]Database created! Please restart program to access the database[/]");
                Console.ReadLine();

                Environment.Exit(0);
            }

            else
            {
                AnsiConsole.MarkupLine("[underline red]Can't continue without a database[/]");
                return;
            }
        }

        UserInput userInput = new UserInput();

        userInput.InputLoop();
    }
}