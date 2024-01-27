using Spectre.Console;
using System;

namespace Habit_Tracker;

public class User
{
    public void InputLoop()
    {
        HabitManager hm = new();

        var table = new Table();

        table.Border = TableBorder.SimpleHeavy;
        table.Title(new TableTitle("[lightsteelblue1]Welcome to my habit tracker! This is where I gather information about you.[/]"));

        table.AddColumn("Select one of the options below to manage your habit.");
        table.AddRow("1: Create Habit");
        table.AddRow("2: Print all habit entries");
        table.AddRow("3: Insert habit entry");
        table.AddRow("4: Update habit entry");
        table.AddRow("5: Delete habit entry");
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
                    hm.CreateHabit();
                    break;

                case "2":
                    hm.PrintAllRecords();
                    Console.ReadLine();
                    break;

                case "3":
                    hm.InsertHabitInfo();
                    break;

                case "4":
                    AnsiConsole.MarkupLine("[underline red]Still working on it![/]");
                    Console.ReadLine();
                    break;

                case "5":
                    hm.DeleteRecord();
                    break;

                case "0":
                    shouldExit = true;
                    break;

                default:
                    AnsiConsole.Markup("[underline red]Invalid entry please enter one of the options above[/]");
                    break;
            }
        }
    }
}