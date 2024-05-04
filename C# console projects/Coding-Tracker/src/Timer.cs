using System;
using System.Diagnostics;
using Spectre.Console;

namespace Coding_Tracker.Timer;

public class CodingTimer
{
    private string? SessionLength = null;
    private bool StopTimer = false;

    public void Start()
    {
        Stopwatch timer = new Stopwatch();
        TimeSpan timeElapsed = new TimeSpan();

        timer.Start();

        while (!StopTimer)
        {
            timeElapsed = timer.Elapsed;

            AnsiConsole.Write("{0:00}:{1:00}:{2:00}\r", timeElapsed.Hours, timeElapsed.Minutes, timeElapsed.Seconds);
        }

        SessionLength = $"{timeElapsed.Hours:00}:{timeElapsed.Minutes:00}:{timeElapsed.Seconds:00}";

        return;
    }

    public void Stop()
    {
        StopTimer = true;
    }

    public string GetSessionLength() => SessionLength ?? "Error no recorded time to display";
}