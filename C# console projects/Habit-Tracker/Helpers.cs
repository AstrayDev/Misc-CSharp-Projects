using System;
using Microsoft.Data.Sqlite;

namespace Habit_Tracker.Utility;

public static class Helpers
{
    public static int StringToInt()
    {
        string? stringInput = Console.ReadLine();
        int intOutput;
        bool intConvert = int.TryParse(stringInput, out intOutput);

        if (!intConvert)
        {
            Console.WriteLine("Input was invalid. Please enter a number");
            StringToInt();
        }

        return intOutput;
    }

    public static string GetTableName()
    {
        SqliteConnection con = new("Data Source = Habit.db");
        SqliteCommand com;

        con.Open();

        // The quary is for finding any tables in the db so I can assign table names to vars for later quaries
        string? tableName = null;
        string quary = "SELECT name FROM sqlite_master WHERE type = 'table'";

        com = new(quary, con);
        SqliteDataReader reader = com.ExecuteReader();

        // Finds the first table in the db for atm I'm only tracking a single habit
        if (reader.Read())
        {
            tableName = reader.GetString(0);
        }

        con.Close();

        if (tableName != null)
        {
            return tableName;
        }

        else
        {
            return "Null";
        }
    }
}