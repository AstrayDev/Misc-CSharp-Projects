using System;
using System.ComponentModel;
using System.Threading;
using Coding_Tracker.Controller;
using Spectre.Console;

namespace Coding_Tracker.Input;

public class UserInput
{
    public void InputLoop()
    {
        TrackerController controller = new TrackerController("Data Source = Tracker.db");

        string[] greetings =
        {
            "Welcome back!",
            "Time to bash your head against a wall?",
            "Let's Fucking go? Let's go...I guess.",
            "What a good day to stare at a screen",
            "Ready for your existential crisis?"
        };

        string[] goodbye =
        {
            "See ya!",
            "You give up?",
            "I guess that was productive",
            "Yeah I was getting tired of that shit as well",
            "A-am I...free?"
        };

        Random randGreeting = new Random();
        Random randGoodbye = new Random();

        int greetingIndex = Convert.ToInt32(randGreeting.Next(0, greetings.Length));
        int goodbyeIndex = Convert.ToInt32(randGoodbye.Next(0, goodbye.Length));

        var table = new Table();

        table.Title = new TableTitle($"[lightsteelblue1]{greetings[greetingIndex]}[/]");
        table.Border = TableBorder.SimpleHeavy;

        table.AddColumn("Select one of the options below:");
        table.AddRow("1: Create tracking table");
        table.AddRow("2: Print all records");
        table.AddRow("3: Start a new session");
        table.AddRow("4: Delete a session");
        table.AddRow("5: Delete all records");
        table.AddRow("0: Exit");


        bool shouldQuit = false;

        while (!shouldQuit)
        {
            Console.Clear();

            AnsiConsole.Write(table);

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    controller.CreateTable();
                    break;

                case "2":
                    controller.PrintAllRecords();
                    Console.ReadLine();
                    break;

                case "3":
                    controller.StartSession();
                    break;

                case "4":
                    controller.DeleteSession();
                    break;

                case "5":
                    controller.DeleteAllRecords();
                    break;

                case "0":
                    AnsiConsole.MarkupLine($"[bold deepskyblue4_1]{goodbye[goodbyeIndex]}[/]");
                    Thread.Sleep(100);
                    shouldQuit = true;
                    break;

                default:
                    AnsiConsole.Markup("[underline red]Invalid input. Try Again[/]");
                    Console.ReadLine();
                    break;
            }
        }
    }
}