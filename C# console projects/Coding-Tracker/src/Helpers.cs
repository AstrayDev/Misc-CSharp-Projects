using System;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;
using Spectre.Console;

public static class Helpers
{
    public static bool IsEnterPressed()
    {
        if (Console.ReadKey().Key == ConsoleKey.Enter)
        {
            return true;
        }

        return false;
    }

    public static string GenerateID()
    {
        string prefix;
        string suffix;

        string month = DateTime.Now.Date.ToString("MM");
        string[] monthArr = month.Split();

        if (monthArr.Contains("0"))
        {
            prefix = monthArr[1];
        }

        else
        {
            prefix = month;
        }

        suffix = DBEntryCount().ToString();

        return $"{prefix}-{suffix}";
    }

    public static bool CheckForTable()
    {
        SqliteConnection con = new("Data Source = Tracker.db");
        con.Open();

        string sql = "SELECT name FROM sqlite_master WHERE type = 'table'";

        if (con.ExecuteReader(sql).Read())
        {
            con.Close();
            return true;
        }

        con.Close();
        return false;
    }

    public static string GetTableName()
    {
        SqliteConnection con = new SqliteConnection("Data Source = Tracker.db");

        con.Open();

        string query = "SELECT name FROM sqlite_master WHERE type = 'table'";
        IDataReader reader = con.ExecuteReader(query);

        if (reader.Read())
        {
            string name = reader.GetString(0);

            con.Close();
            return name;
        }

        con.Close();
        return "No table found";
    }

    public static int DBEntryCount()
    {
        string name = GetTableName();
        SqliteConnection con = new SqliteConnection("Data Source = Tracker.db");

        con.Open();

        string query = $"SELECT COUNT(*) FROM {name}";
        int entryCount = Convert.ToInt32(con.ExecuteScalar(query));

        con.Close();

        return entryCount;
    }
}