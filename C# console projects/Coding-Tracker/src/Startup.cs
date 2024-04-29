using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Spectre.Console;
using Coding_Tracker.Controller;
using Dapper;

public class Startup
{
    public void CheckForDB()
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
                SqliteConnection con = new("Data Source = Tracker.db");
                con.Open();

                string quary = "CREATE TABLE Tracker (ID INTEGER, Duration TEXT, Date TEXT)";

                con.Execute(quary);

                AnsiConsole.MarkupLine("[bold green]Database and table created![/]");

                return;
            }
        }

        if (!CheckForTable())
        {
            var input = AnsiConsole.Prompt
            (
                new SelectionPrompt<string>()
                .AddChoices("Yes", "No")
            );

            AnsiConsole.MarkupLine("[underline red]No table found in database. Would you like to create one?[/]");

            if (input == "Yes")
            {
                SqliteConnection con = new("Data Source = Tracker.db");
                string quary = "CREATE TABLE Tracker (ID INTEGER, Duration TEXT, Date TEXT)";
                con.Execute(quary);
            }

            AnsiConsole.MarkupLine("[bold green]Table created![/]");
        }

        else
        {
            AnsiConsole.MarkupLine("[underline red]Can't continue without a database[/]");
            return;
        }
    }

    private bool CheckForTable()
    {
        SqliteConnection con = new("Data Source = Tracker.db");
        con.Open();

        string sql = "SELECT name FROM sqlite_master WHERE type = 'table'";

        if (con.ExecuteReader(sql).Read())
        {
            return true;
        }

        return false;
    }
}