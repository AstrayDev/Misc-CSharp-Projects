using System;
using System.IO;
using Spectre.Console;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Dapper;
using Coding_Tracker.Model;
using System.Diagnostics;

namespace Coding_Tracker.Controller;

public class TrackerController
{
    private static string? ConnectionString;

    public TrackerController(string connectionString)
    {
        ConnectionString = connectionString;
    }

    private readonly SqliteConnection con = new(ConnectionString);

    public void AddSessionToDB()
    {
        var table = new Table();
        con.Open();

        table.Border = TableBorder.Ascii2;
        table.AddColumns("ID", "Duration", "Date");

        string quary = "INSERT INTO Tracker (ID, Duration, Date) VALUES ('1', '3 hours', '2/2/2222')";
        con.Execute(quary);

        table.AddRow("1", "3hours", "2/22/2222");
        AnsiConsole.Write(table);
    }

    public void UpdateSession()
    {

    }

    public void DeleteSession()
    {

    }
}