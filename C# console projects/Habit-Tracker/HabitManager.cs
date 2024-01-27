using System;
using System.IO;
using Microsoft.Data.Sqlite;
using Habit_Tracker.Models;
using Habit_Tracker.Utility;
using Spectre.Console;

namespace Habit_Tracker;

public class HabitManager
{
    private Habit habit = new();

    private SqliteCommand? com;
    public void CreateHabit()
    {
        if (File.Exists("Habit.db"))
        {
            AnsiConsole.MarkupLine("[underline red]A habit already exists and you can only track one[/]");
            Console.ReadLine();
            return;
        }

        AnsiConsole.Markup("[underline blue]Please enter the name of the habit you wish to track.[/]");

        habit.Name = Console.ReadLine();

        SqliteConnection con = new("Data Source = Habit.db");

        string createTable = @$"CREATE TABLE {habit.Name} (ID STRING, Frequancy STRING, Date DATETIME)";

        AnsiConsole.MarkupLine("[bold green]Database and table created![/]");

        con.Open();
        com = new(createTable, con);
        com.ExecuteNonQuery();

        Console.ReadLine();

        con.Close();
    }

    public void InsertHabitInfo()
    {
        AnsiConsole.MarkupLine("[underline blue]Enter the amount of times you've impulsivley done your habit today[/]");

        if (!File.Exists("Habit.db"))
        {
            AnsiConsole.MarkupLine("[underline red]Database doesn't exist please create one[/]");
        }

        SqliteConnection con = new("Data Source = Habit.db");
        con.Open();

        habit.Name = Helpers.GetTableName();

        string submitDate = DateTime.Now.ToString();

        habit.Frequancy = Helpers.StringToInt();
        habit.ID = Helpers.GenerateID();

        string insertInfo = @$"INSERT INTO {habit.Name} (ID, Frequancy, Date) VALUES ('{habit.ID}', '{habit.Frequancy}', '{submitDate}')";

        com = new(insertInfo, con);
        com.ExecuteNonQuery();

        AnsiConsole.MarkupLine("[underline blue]Entry inserted successfully![/]");

        Console.ReadLine();

        con.Close();
    }

    public void DeleteRecord()
    {
        if (!File.Exists("Habit.db"))
        {
            AnsiConsole.MarkupLine("[underline red]Database doesn't exist please create one[/]");
        }

        AnsiConsole.MarkupLine("[underline blue]Enter the ID of the entry you wish to delete[/]");

        PrintAllRecords();

        SqliteConnection con = new("Data Source = Habit.db");
        con.Open();

        habit.Name = Helpers.GetTableName();

        string? idSelection = Console.ReadLine();

        string deleteQuery = $"DELETE FROM {habit.Name} WHERE ID = {idSelection}";

        com = new(deleteQuery, con);
        com.ExecuteNonQuery();

        AnsiConsole.MarkupLine("[underline blue]Entry deleted successfully![/]");
        Console.ReadLine();

        con.Close();
    }

    public void PrintAllRecords()
    {
        if (!File.Exists("Habit.db"))
        {
            AnsiConsole.MarkupLine("[underline red]Database doesn't exist please create one[/]");
        }

        SqliteConnection con = new("Data Source = Habit.db");
        con.Open();

        habit.Name = Helpers.GetTableName();

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