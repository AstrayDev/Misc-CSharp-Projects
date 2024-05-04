using System.IO;
using Microsoft.Extensions.Configuration;
using Coding_Tracker.Controller;
using Coding_Tracker.Timer;
using Spectre.Console;
using Coding_Tracker.Input;

namespace Coding_Tracker;

class Program
{
    private static IConfigurationBuilder builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile(@"..\appconfig.json", optional: true, reloadOnChange: true);

    private static IConfigurationRoot Configuration = builder.Build();
    private static readonly string? ConnectionString = Configuration.GetConnectionString("ConnectionString");
    private readonly static TrackerController Controller = new TrackerController("Data Source = Tracker.db");

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

                AnsiConsole.MarkupLine("[bold green]Database created![/]");
            }

            else
            {
                AnsiConsole.MarkupLine("[underline red]Can't continue without a database[/]");
                return;
            }
        }

        CodingTimer sessionTimer = new CodingTimer();
        UserInput userInput = new UserInput();

        userInput.InputLoop();
    }
}