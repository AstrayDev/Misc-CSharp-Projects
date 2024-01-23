using System;
using Microsoft.Data.Sqlite;
using Habit_Tracker.Models;
using Habit_Tracker.Utility;

namespace Habit_Tracker;

public class HabitManager
{
    public Habit habit = new();
    private SqliteConnection con = new("Data Source = Habit.db");
    private SqliteCommand? com;
    public void CreateHabit()
    {
        Console.WriteLine("Please enter the name of the habit you wish to track.");

        habit.Name = Console.ReadLine();

        string createTable = @$"CREATE TABLE {habit.Name} (Frequancy STRING, Date DATETIME)";

        con.Open();
        com = new(createTable, con);
        com.ExecuteNonQuery();

        con.Close();
    }

    public void InsertHabitInfo()
    {
        Console.WriteLine("Enter the amount of times you've impulsivley done your habit today");

        con.Open();
        habit.Name = Helpers.GetTableName();

        int frequancy = Helpers.StringToInt();
        string submitDate = DateTime.Now.ToString();

        string insertInfo = @$"INSERT INTO {habit.Name} (Frequancy, Date) VALUES ('{frequancy}', '{submitDate}')";

        com = new(insertInfo, con);
        com.ExecuteNonQuery();

        con.Close();
    }

    public void DeleteRecord()
    {
        // To be worked on
    }

    public void PrintAllRecords()
    {
        con.Open();
        habit.Name = Helpers.GetTableName();

        string readCom = $"SELECT * FROM {habit.Name}";

        com = new(readCom, con);
        SqliteDataReader reader = com.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine($"Frequancy: {reader["Frequancy"]} Date: {reader["Date"]}");
        }

        con.Close();
    }
}