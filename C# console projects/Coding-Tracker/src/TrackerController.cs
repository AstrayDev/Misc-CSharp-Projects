using System;
using Spectre.Console;
using Microsoft.Data.Sqlite;
using Dapper;
using Dapper.Contrib.Extensions;
using Coding_Tracker.Model;
using Coding_Tracker.Timer;
using System.Linq;
using System.Threading.Tasks;

namespace Coding_Tracker.Controller;

public class TrackerController
{
    private static string? ConnectionString;

    public TrackerController(string connectionString)
    {
        ConnectionString = connectionString;
    }

    private readonly SqliteConnection Con = new SqliteConnection(ConnectionString);
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
            }

            else
            {
                AnsiConsole.MarkupLine("[underline red]\nCan't continue without a table[/]");
            }
        }
    }

    public void DeleteAllRecords()
    {
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
        }

        else
        {
            return;
        }
    }

    public void StartSession()
    {
        CodingTimer timer = new CodingTimer();

        Console.CursorVisible = false;
        Console.Clear();

        Parallel.Invoke
      (
          () =>
          {
              timer.Start();
              Session = new CodingSessionModel() { Duration = timer.GetSessionLength() };
          },

          () =>
          {
              AnsiConsole.MarkupLine("[underline blue]Press enter to quit session.\n[/]");
          },

          () =>
          {
              if (Helpers.IsEnterPressed())
              {
                  timer.Stop();

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
          }
      );
    }

    private void AddSessionToDB()
    {
        var table = new Table();

        Con.Open();
        string name = Helpers.GetTableName();

        table.Border = TableBorder.Ascii2;
        table.AddColumns("ID", "Duration", "Date");

        Session = new CodingSessionModel()
        {
            ID = Helpers.GenerateID(),
            // Duration = Timer.GetSessionLength(),
            Date = DateTime.Now
        };

        Con.Insert(Session);
        Con.Close();

        table.AddRow($"{Session.ID}", $"{Session.Duration}", $"{Session.Date}");
        AnsiConsole.Write(table);
    }

    public void DeleteSession()
    {
        AnsiConsole.MarkupLine("[underline blue]Enter the ID for the entry you wish to delete[/]");

        var table = new Table();

        string? idInput = Console.ReadLine();

        int currentEntryCount = Helpers.DBEntryCount();
        int newEntryCount = currentEntryCount - 1;

        bool wasEntryDeleted = currentEntryCount > newEntryCount;

        Con.Open();

        if (wasEntryDeleted)
        {
            AnsiConsole.MarkupLine("[underline red]Could not find an entry with that ID. Try again[/]");
        }

        else
        {
            Con.Delete(idInput);
            AnsiConsole.MarkupLine("[bold green]Entry deleted successfully![/]");
        }

        Con.Close();
    }

    public void PrintAllRecords()
    {
        Con.Open();

        var entries = Con.GetAll<CodingSessionModel>().ToList();
        var table = new Table();

        table.AddColumns("ID", "Duration", "Date");
        Console.Write(entries);
    }
}