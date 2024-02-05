using System;
using Microsoft.Data.Sqlite;
using Habit_Tracker.Models;
using Habit_Tracker.Utility;
using Spectre.Console;

namespace Habit_Tracker;

public abstract class HabitManager
{
    private Habit habit = new();

    private readonly SqliteConnection con = new("Data Source = Habit.db");
    private SqliteCommand? com;

    protected void CreateHabit()
    {
        if (Helpers.GetTableName() != null)
        {
            AnsiConsole.MarkupLine("[underline red]A habit already exists and you can only track one[/]\n");
            return;
        }

        AnsiConsole.Markup("[underline blue]Please enter the name of the habit you wish to track.[/]\n");

        habit.Name = Console.ReadLine();
        // SqliteConnection con = new("Data Source = Habit.db");

        string createTable = @$"CREATE TABLE {habit.Name} (ID STRING, Frequancy INTEGER, Date DATETIME)";

        AnsiConsole.MarkupLine("[bold green]Database and table created![/]");

        con.Open();
        com = new(createTable, con);
        com.ExecuteNonQuery();

        con.Close();
    }

    protected void DeleteTable()
    {
        habit.Name = Helpers.GetTableName();

        if (habit.Name == null)
        {
            AnsiConsole.MarkupLine("[underline red]Table doesn't exist please create one[/]\n");
            return;
        }

        AnsiConsole.MarkupLine("[bold red]This will delete ALL of you habit entries. Are you sure?[/]\n");

        var input = AnsiConsole.Prompt
        (
            new SelectionPrompt<string>()
            .AddChoices("Yes", "No")
        );

        if (input == "Yes")
        {
            string query = $"DROP TABLE {habit.Name}";

            con.Open();

            com = new(query, con);
            com.ExecuteNonQuery();

            con.Close();

            AnsiConsole.MarkupLine("[bold green]Table deleted successfully![/]\n");
        }

        else if (input == "No")
        {
            return;
        }
    }

    protected void InsertHabitInfo()
    {
        habit.Name = Helpers.GetTableName();

        if (habit.Name == null)
        {
            AnsiConsole.MarkupLine("[underline red]Table doesn't exist please create one[/]\n");
            return;
        }

        AnsiConsole.MarkupLine("[underline blue]Enter the amount of times you've impulsivley done your habit today[/]\n");

        var table = new Table();

        table.Border = TableBorder.Ascii2;
        table.AddColumns("ID", "Frequancy", "Date");

        con.Open();

        habit.Date = DateTime.Now.ToString();
        habit.Frequancy = Helpers.StringToInt();
        habit.ID = Helpers.GenerateID();

        string insertInfo = @$"INSERT INTO {habit.Name} (ID, Frequancy, Date) VALUES ('{habit.ID}', '{habit.Frequancy}', '{habit.Date}')";

        com = new(insertInfo, con);
        com.ExecuteNonQuery();

        table.AddRow($"{habit.ID}", $"{habit.Frequancy}", $"{habit.Date}");

        AnsiConsole.MarkupLine("[bold green]Entry inserted successfully![/]\n");
        AnsiConsole.Write(table);

        con.Close();
    }

    protected void UpdateRecord()
    {
        habit.Name = Helpers.GetTableName();

        if (habit.Name == null)
        {
            AnsiConsole.MarkupLine("[underline red]Table doesn't exist please create one[/]\n");
            return;
        }

        con.Open();

        AnsiConsole.MarkupLine("[underline blue]Please enter the ID of the entry you wish to update[/]\n");

        PrintAllRecords();

        int idSelection = Helpers.StringToInt();

        AnsiConsole.MarkupLine("[underline blue]Now the new frequancy you wish to set[/]\n");

        int newFrequancy = Helpers.StringToInt();

        string query = $"UPDATE {habit.Name} SET Frequancy = @newFrequancy WHERE ID = {idSelection}";

        SqliteCommand com = new(query, con);


        com.Parameters.AddWithValue("@newFrequancy", $"{newFrequancy}");
        com.ExecuteNonQuery();

        AnsiConsole.MarkupLine("[bold green]Record updated successfully![/]\n");

        con.Close();
    }

    protected void DeleteRecord()
    {
        habit.Name = Helpers.GetTableName();

        if (habit.Name == null)
        {
            AnsiConsole.MarkupLine("[underline red]Table doesn't exist please create one[/]\n");
            return;
        }

        AnsiConsole.MarkupLine("[underline blue]Enter the ID of the entry you wish to delete[/]\n");

        PrintAllRecords();

        con.Open();


        int idSelection = Helpers.StringToInt();

        string deleteQuery = $"DELETE FROM {habit.Name} WHERE ID = {idSelection}";

        com = new(deleteQuery, con);
        com.ExecuteNonQuery();

        AnsiConsole.MarkupLine("[bold green]Entry deleted successfully![/]\n");

        con.Close();
    }

    protected void PrintAllRecords()
    {
        habit.Name = Helpers.GetTableName();

        if (habit.Name == null)
        {
            AnsiConsole.MarkupLine("[underline red]Table doesn't exist please create one[/]\n");
            return;
        }

        // Checks if there are any current entries
        else if (Helpers.TableRowCount() == 0)
        {
            AnsiConsole.MarkupLine("[underline red]No entries submitted![/]\n");
            return;
        }

        con.Open();

        string readCom = $"SELECT * FROM {habit.Name}";

        com = new(readCom, con);
        SqliteDataReader reader = com.ExecuteReader();

        var table = new Table();
        table.Border = TableBorder.Ascii2;

        table.AddColumns("ID", "Frequancy", "Date");

        while (reader.Read())
        {
            table.AddRow($"{reader["ID"]}", $"{reader["Frequancy"]}", $"{reader["Date"]}");
        }

        AnsiConsole.Write(table);

        con.Close();
    }
}