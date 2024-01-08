using System;
using System.Collections.Generic;

namespace C_;

abstract class Games
{
    public static int numberOne;
    public static int numberTwo;
    public static int QuestionsCounterLocation = Console.WindowWidth - 26;
    public static int CorrectAnswer { get; set; } = 0;
    public static int WrongAnswer { get; set; } = 0;
    private static Random rand = new();
    public static int Answer { get; set; }
    protected static List<string> PastGames { get; set; } = new List<string>();

    public static void Addition()
    {
        int rounds = 10;

        Console.Clear();

        while (rounds > 0)
        {
            numberOne = rand.Next(0, 20);
            numberTwo = rand.Next(0, 20);

            Answer = numberOne + numberTwo;

            Console.WriteLine("Enter the ansewer to the question below\n");
            Console.WriteLine($"{numberOne} + {numberTwo}");

            Console.SetCursorPosition(QuestionsCounterLocation, 0);

            Console.WriteLine($"Questions Remaining: {rounds}/10");

            Console.SetCursorPosition(0, 4);

            int playerAnswer = Helpers.PlayerAnswer();

            Helpers.AddGameToHistory(numberOne, '+', numberTwo, playerAnswer);

            Console.ReadLine();
            Console.Clear();

            rounds--;
        }
    }

    public static void Subtraction()
    {
        int rounds = 10;

        Console.Clear();

        while (rounds > 0)
        {
            numberOne = rand.Next(0, 20);
            numberTwo = rand.Next(0, 20);

            Answer = numberOne - numberTwo;

            Console.WriteLine("Enter the ansewer to the question below\n");
            Console.WriteLine($"{numberOne} - {numberTwo}");

            Console.SetCursorPosition(QuestionsCounterLocation, 0);

            Console.WriteLine($"Questions Remaining: {rounds}/10");

            Console.SetCursorPosition(0, 4);

            int playerAnswer = Helpers.PlayerAnswer();

            Helpers.AddGameToHistory(numberOne, '-', numberTwo, playerAnswer);

            Console.ReadLine();
            Console.Clear();

            rounds--;
        }
    }

    public static void Multiplication()
    {
        int rounds = 10;

        Console.Clear();

        while (rounds > 0)
        {
            numberOne = rand.Next(0, 20);
            numberTwo = rand.Next(0, 20);

            Answer = numberOne * numberTwo;

            Console.WriteLine("Enter the ansewer to the question below\n");
            Console.WriteLine($"{numberOne} x {numberTwo}");

            Console.SetCursorPosition(QuestionsCounterLocation, 0);

            Console.WriteLine($"Questions Remaining: {rounds}/10");

            Console.SetCursorPosition(0, 4);

            int playerAnswer = Helpers.PlayerAnswer();

            Helpers.AddGameToHistory(numberOne, 'x', numberTwo, playerAnswer);

            Console.ReadLine();
            Console.Clear();

            rounds--;
        }
    }

    public static void Division()
    {
        int rounds = 10;

        Console.Clear();

        while (rounds > 0)
        {
            numberOne = 2 * rand.Next(2 / 2, 20 / 2);

            do
            {
                numberTwo = rand.Next(1, numberOne);

            } while (numberOne % numberTwo != 0);

            Answer = numberOne / numberTwo;

            Console.WriteLine("Enter the ansewer to the question below.\nTip: Round to the closest whole number\n");
            Console.WriteLine($"{numberOne} / {numberTwo}");

            Console.SetCursorPosition(QuestionsCounterLocation, 0);

            Console.WriteLine($"Questions Remaining: {rounds}/10");

            Console.SetCursorPosition(0, 4);

            int playerAnswer = Helpers.PlayerAnswer();

            Helpers.AddGameToHistory(numberOne, '/', numberTwo, playerAnswer);

            Console.ReadLine();
            Console.Clear();

            rounds--;
        }
    }
}