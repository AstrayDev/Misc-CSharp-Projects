using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Spectre.Console;
using SQLitePCL;

namespace Coding_Tracker.Timer;

public class CodingTimer
{
    protected string? SessionLength { get; private set; } = null;
    private bool StopTimer = false;

    public void StartTimer()
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
        Console.CursorVisible = false;
        Console.Clear();

        Parallel.Invoke
      (
          () =>
          {
              StartTimer();
          },

          () =>
          {
              AnsiConsole.MarkupLine("[underline blue]Press enter to quit session.\n[/]");
          },

          () =>
          {
              if (Helpers.GetuserInput())
              {
                  StopTimer = true;
              }
          }
      );

        Console.CursorVisible = true;
    }

    public string GetSessionLength() => SessionLength ?? "No recorded time";
}