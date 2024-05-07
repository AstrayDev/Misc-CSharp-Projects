using System;
using Spectre.Console;
using Microsoft.Data.Sqlite;
using Dapper;
using Dapper.Contrib.Extensions;
using Coding_Tracker.Model;
using Coding_Tracker.Timer;

namespace Coding_Tracker.Controller;

public class TrackerController
{
    private static string? ConnectionString;

    public TrackerController(string connectionString)
    {
        ConnectionString = connectionString;
    }

    private readonly SqliteConnection Con = new SqliteConnection("Data Source = Tracker.db");
    private CodingSessionModel Session;
    private CodingTimer Timer = new CodingTimer();

    public void CreateTable()
    {
        if (!Helpers.CheckForTable())
        {
            AnsiConsole.MarkupLine("[underline red]No table found in database. Would you like to create one?[/]");

            var input = AnsiConsole.Prompt
            (
                new SelectionPrompt<string>()
                .AddChoices("Yes", "No")
            );

            if (input == "Yes")
            {
                SqliteConnection con = new("Data Source = Tracker.db");
                string quary = "CREATE TABLE Tracker(ID TEXT, Duration TEXT, Date TEXT)";

                con.Open();
                con.Execute(quary);
                con.Close();

                AnsiConsole.MarkupLine("[bold green]Table created![/]");
                Console.ReadLine();
            }

            else
            {
                AnsiConsole.MarkupLine("[underline red]\nCan't continue without a table[/]");
                Console.ReadLine();
            }
        }

        else
        {
            AnsiConsole.MarkupLine("[underline red]Table already exists[/]");
            Console.ReadLine();
        }
    }

    public void DeleteAllRecords()
    {
        if (!Helpers.CheckForTable())
        {
            AnsiConsole.MarkupLine("[underline red]No table in database. Please create one[/]");
            Console.ReadLine();

            return;
        }

        else if (Helpers.DBEntryCount() == 0)
        {
            AnsiConsole.MarkupLine("[underline red]No entries to delete[/]");

            Console.ReadLine();
            return;
        }

        AnsiConsole.MarkupLine("[underline red]This will delete ALL records, are you sure?[/]");

        var input = AnsiConsole.Prompt
        (
            new SelectionPrompt<string>()
            .AddChoices("Yes", "No")
        );

        if (input == "Yes")
        {
            Con.DeleteAll<CodingSessionModel>();

            AnsiConsole.MarkupLine("[bold green]All records deleted successfully![/]");
            Console.ReadLine();
        }

        else
        {
            return;
        }
    }

    public void StartSession()
    {
        if (!Helpers.CheckForTable())
        {
            AnsiConsole.MarkupLine("[underline red]No table in database. Please create one[/]");
            Console.ReadLine();

            return;
        }

        CodingTimer timer = new CodingTimer();

        Console.CursorVisible = false;
        Console.Clear();

        Timer.Start();

        Console.Clear();

        AnsiConsole.Console.MarkupLine("[underline blue]Add session to database?[/]");

        var input = AnsiConsole.Prompt
        (
          new SelectionPrompt<string>()
          .AddChoices("Yes", "No")
        );

        if (input == "Yes")
        {
            AddSessionToDB();
        }

        else
        {
            Console.CursorVisible = true;
            return;
        }
    }

    private void AddSessionToDB()
    {
        var table = new Table();

        Con.Open();

        table.Border = TableBorder.Ascii2;
        table.AddColumns("ID", "Duration", "Date");


        Session = new CodingSessionModel
        {
            ID = Helpers.GenerateID(),
            Duration = Timer.SessionLength,
            Date = DateTime.Now.ToString()
        };

        string q = $"INSERT INTO Tracker (ID, Duration, Date) VALUES ('{Session.ID}', '{Session.Duration}', '{Session.Date}')";

        Con.Execute(q);
        Con.Close();

        table.AddRow($"{Session.ID}", $"{Session.Duration}", $"{Session.Date}");

        AnsiConsole.MarkupLine("[bold green]Entry inserted successfully![/]\n");
        AnsiConsole.Write(table);

        Console.ReadLine();
    }

    public void DeleteSession()
    {
        if (!Helpers.CheckForTable())
        {
            AnsiConsole.MarkupLine("[underline red]No table in database. Please create one[/]");
            Console.ReadLine();

            return;
        }

        else if (Helpers.DBEntryCount() == 0)
        {
            AnsiConsole.MarkupLine("[underline red]No entried to delete[/]");

            Console.ReadLine();
            return;
        }

        AnsiConsole.MarkupLine("[underline blue]Enter the ID for the entry you wish to delete[/]");

        var table = new Table();

        PrintAllRecords();

        string? idInput = Console.ReadLine();

        int currentEntryCount = Helpers.DBEntryCount();
        Con.Open();
        Con.Delete(new CodingSessionModel { ID = idInput });

        int newEntryCount = Helpers.DBEntryCount();

        if (currentEntryCount > newEntryCount)
        {
            AnsiConsole.MarkupLine("[bold green]Entry deleted successfully![/]");
            Console.ReadLine();
        }

        else
        {
            AnsiConsole.MarkupLine("[underline red]Could not find an entry with that ID. Try again[/]");
            Console.ReadLine();
        }

        Con.Close();
    }

    public void PrintAllRecords()
    {
        if (!Helpers.CheckForTable())
        {
            AnsiConsole.MarkupLine("[underline red]No table in database. Please create one[/]");
        }

        else if (Helpers.DBEntryCount() > 0)
        {
            Con.Open();

            var entries = Con.ExecuteReader("SELECT * FROM Tracker");
            var table = new Table();

            table.Border = TableBorder.Ascii2;
            table.AddColumns("ID", "Duration", "Date");

            while (entries.Read())
            {
                table.AddRow($"{entries["ID"]}", $"{entries["Duration"]}", $"{entries["Date"]}");
            }

            AnsiConsole.Write(table);
        }

        else
        {
            AnsiConsole.MarkupLine("[underline red]No records in database[/]");
        }

        Con.Close();
    }
}