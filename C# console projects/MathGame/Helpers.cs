using System;

namespace C_;

class Helpers : Games
{
    public static int StringToIntConverter()
    {
        string? stringInput = Console.ReadLine();
        int numberInput;
        bool inputConversion = int.TryParse(stringInput, out numberInput);

        if (!inputConversion)
        {
            throw new InvalidCastException();
        }

        return numberInput;
    }

    public static int PlayerAnswer()
    {
        int playerAnswer = StringToIntConverter();

        if (playerAnswer == Answer)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Correct! You gained a point!");
            Console.ForegroundColor = ConsoleColor.White;
            CorrectAnswer++;
        }

        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Incorrect! Youe lost a point!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"The answer was {Answer}!");
            WrongAnswer++;
        }

        return playerAnswer;
    }

    public static void AddGameToHistory(int numberOne, char operation, int numberTwo, int answer)
    {
        string pastGame = $"{numberOne} {operation} {numberTwo} = {answer}";

        PastGames.Add(pastGame);
    }
}