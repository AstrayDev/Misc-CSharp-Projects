using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using Coding_Tracker.Controller;
using Coding_Tracker.Timer;
using Dapper;
using System.Threading.Tasks;
using Spectre.Console;

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
        CodingTimer sessionTimer = new CodingTimer();

        sessionTimer.StartSession();
    }
}