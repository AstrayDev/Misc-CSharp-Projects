using Spectre.Console;
using System;
using System.Threading;

namespace Habit_Tracker;

public class User : HabitManager
{
    public void InputLoop()
    {
        var table = new Table();

        table.Border = TableBorder.SimpleHeavy;
        table.Title(new TableTitle("[lightsteelblue1]Welcome to my habit tracker! This is where I gather information about you.[/]"));

        table.AddColumn("Select one of the options below to manage your habit.");
        table.AddRow("1: Create Habit");
        table.AddRow("2: Print all habit entries");
        table.AddRow("3: Insert habit entry");
        table.AddRow("4: Update habit entry");
        table.AddRow("5: Delete habit entry");
        table.AddRow("6: Delete database");
        table.AddRow("0: Exit");

        bool shouldExit = false;

        while (!shouldExit)
        {
            Console.Clear();

            AnsiConsole.Write(table);

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateHabit();
                    Console.ReadLine();
                    break;

                case "2":
                    PrintAllRecords();
                    Console.ReadLine();
                    break;

                case "3":
                    InsertHabitInfo();
                    Console.ReadLine();
                    break;

                case "4":
                    UpdateRecord();
                    break;

                case "5":
                    DeleteRecord();
                    Console.ReadLine();
                    break;

                case "6":
                    DeleteTable();
                    Console.ReadLine();
                    break;

                case "0":
                    AnsiConsole.MarkupLine("[bold deepskyblue4_1]Goodbye[/]");
                    Thread.Sleep(1000);
                    shouldExit = true;
                    break;

                default:
                    AnsiConsole.Markup("[underline red]Invalid entry please enter one of the options above[/]");
                    break;
            }
        }
    }
}