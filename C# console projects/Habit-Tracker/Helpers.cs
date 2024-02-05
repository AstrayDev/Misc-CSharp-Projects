using System;
using Microsoft.Data.Sqlite;
using Habit_Tracker.Models;

namespace Habit_Tracker.Utility;

public static class Helpers
{
    private static readonly SqliteConnection con = new("Data Source = Habit.db");
    private static SqliteCommand? com;

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

    public static string? GetTableName()
    {
        con.Open();

        // The quary is for finding any tables in the db so I can assign table names to vars for later quaries
        string? tableName = null;
        string query = "SELECT name FROM sqlite_master WHERE type = 'table'";

        com = new(query, con);
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
            return null;
        }
    }

    public static int TableRowCount()
    {
        Habit habit = new()
        {
            Name = GetTableName()
        };

        con.Open();

        string rowCountQuery = $"SELECT COUNT(*) FROM {habit.Name}";

        com = new(rowCountQuery, con);

        int rowCount = Convert.ToInt32(com.ExecuteScalar());

        con.Close();

        return rowCount;
    }

    public static string GenerateID()
    {
        /*
         <summary>
            Generates an id that contains the month of entry as the prefix
            and the amount of entries that month as the suffix
        </summary>
        */

        int suffixInt;

        // If there are no entries give the suffix for the id a starter value
        if (TableRowCount() == 0)
        {
            suffixInt = 1;
        }

        else
        {
            suffixInt = TableRowCount() + 1;
        }

        string idPrefix = DateTime.Now.ToString("MM");
        string idSuffix = suffixInt.ToString();

        string id = $"{idPrefix}{idSuffix}";

        return id;
    }
}