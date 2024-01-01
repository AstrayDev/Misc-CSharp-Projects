using System;
using System.Collections.Generic;
using System.Linq;

namespace C_;

interface IMathGame
{
    static void StartGame() { }
    static abstract void Addition();
    static abstract void Subtraction();
    static abstract void Multiplication();
    static abstract void Division();

}

class GameLoop : Games, IMathGame
{
    public static void StartGame()
    {
        bool gameOver = false;

        while (!gameOver)
        {
            Console.Clear();

            CorrectAnswer = 0;
            WrongAnswer = 0;

            Console.WriteLine("Welcome to the MATH GAAAAAAAAME!!!");
            Console.WriteLine("Please choose one of the following operations:\n");
            Console.WriteLine("1: Addition");
            Console.WriteLine("2: Subtraction");
            Console.WriteLine("3: Multiplication");
            Console.WriteLine("4: Division");
            Console.WriteLine("5: Show past games");
            Console.WriteLine("6: Exit\n");

            try
            {
                int choiceInput = Helpers.StringToIntConverter();

                Console.WriteLine();

                switch (choiceInput)
                {
                    case 1:
                        Addition();
                        break;

                    case 2:
                        Subtraction();
                        break;

                    case 3:
                        Multiplication();
                        break;

                    case 4:
                        Division();
                        break;

                    case 5:
                        if (PastGames.Any())
                        {
                            Console.WriteLine("Your past games were:");

                            foreach (string pastGame in PastGames)
                            {
                                Console.WriteLine(pastGame);
                            }
                        }

                        else
                        {
                            Console.WriteLine("Currently no past games to view");
                        }
                        break;

                    case 6:
                        gameOver = true;
                        break;
                }

            }
            catch (InvalidCastException)
            {
                Console.WriteLine("Invalid input. Returning to menu");
            }

            if (!gameOver)
            {
                Console.WriteLine();
                GameOutcome();
                Console.ReadLine();
            }
        }
    }

    private static void GameOutcome()
    {
        if (CorrectAnswer + WrongAnswer == 10)
        {

            if (CorrectAnswer > WrongAnswer)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You Won! You understand arithmetic!");
                Console.ForegroundColor = ConsoleColor.White;
            }

            else if (CorrectAnswer < WrongAnswer)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Lost! You don't understand arithmetic!");
                Console.ForegroundColor = ConsoleColor.White;
            }

            else if (CorrectAnswer != 0 && WrongAnswer != 0 && CorrectAnswer == WrongAnswer)
            {
                Console.WriteLine("It's a tie!");
            }

            if (CorrectAnswer != 0 || WrongAnswer != 0)
            {
                Console.WriteLine($"You got {CorrectAnswer} right and {WrongAnswer} wrong.");
            }
        }
    }
}

class MathGame
{
    static void Main(string[] args)
    {
        GameLoop.StartGame();
    }
}