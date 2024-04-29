using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using Coding_Tracker.Controller;
using Coding_Tracker.Timer;
using Dapper;

namespace Coding_Tracker;

class Program
{
    private static IConfigurationBuilder builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile(@"..\appconfig.json", optional: true, reloadOnChange: true);

    private static IConfigurationRoot Configuration = builder.Build();
    private static readonly string? ConnectionString = Configuration.GetConnectionString("ConnectionString");
    private readonly static TrackerController Controller = new(ConnectionString!);

    static void Main(string[] args)
    {
        CodingTimer t = new();

        t.StartSession();

        Console.WriteLine(t.GetSessionLength());
    }
}