using System;
using System.Diagnostics;
using Spectre.Console;

namespace Coding_Tracker.Timer;

public class CodingTimer
{
    public string? SessionLength { get; private set; }

    public void Start()
    {
        Stopwatch timer = new Stopwatch();
        TimeSpan timeElapsed = new TimeSpan();

        AnsiConsole.MarkupLine("[underline blue]Press enter to stop session[/]");

        timer.Start();

        while (!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter))
        {
            timeElapsed = timer.Elapsed;

            AnsiConsole.Write("{0:00}:{1:00}:{2:00}\r", timeElapsed.Hours, timeElapsed.Minutes, timeElapsed.Seconds);
        }

        SessionLength = $"{timeElapsed.Hours:00}:{timeElapsed.Minutes:00}:{timeElapsed.Seconds:00}";
    }
}