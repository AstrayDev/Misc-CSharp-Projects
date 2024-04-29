using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Primitives;
using Spectre.Console;

namespace Coding_Tracker.Timer;

public class CodingTimer
{
    protected string? SessionLength { get; private set; } = null;
    private bool StopTimer = false;

    private void StartTimer()
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

    public void StartSession()
    {
        new Thread(new ThreadStart(StartTimer)).Start();

        AnsiConsole.MarkupLine("\n\n[underline blue]Stop Session?[/]");

        var input = AnsiConsole.Prompt
        (
            new SelectionPrompt<string>()
            .AddChoices("Yes", "No")
        );

        if (input == "Yes")
        {
            StopTimer = true;
        }
    }

    public string GetSessionLength() => SessionLength ?? "No recorded time";
}