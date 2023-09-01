Random rand = new Random();

Console.WriteLine("Welcome to rock paper scissors! Press 'R', 'S', or 'P' to choice choose your move.");

int playerScore = 3;
int enemyScore = 3;

int enemyChoice = 0;
string? playerChoice;

bool shouldQuit = false;

while (!shouldQuit)
{
replay:
    PlayGame();

    if (ReplayOption())
        goto replay;

    else
        shouldQuit = true;
}

void PlayGame()
{
    while (playerScore != 0 && enemyScore != 0)
    {
        Console.WriteLine($"Player score: {playerScore}");
        Console.WriteLine($"Enemy score: {enemyScore}\n");

        Console.ForegroundColor = ConsoleColor.White;

        playerChoice = Console.ReadLine();

        if (playerChoice != null)
        {
            enemyChoice = rand.Next(0, 3);

            switch (enemyChoice)
            {
                case 0:
                    switch (playerChoice)
                    {
                        case "r":
                            Console.WriteLine("The enemy chose rock. It's a tie!");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "p":
                            Console.WriteLine("The enemy chose rock. The enemy lost a point!");
                            Console.ForegroundColor = ConsoleColor.Green;

                            --enemyScore;
                            break;

                        case "s":
                            Console.WriteLine("The enemy chose rock. You lost a point!");
                            Console.ForegroundColor = ConsoleColor.Red;

                            --playerScore;
                            break;

                        default:
                            Console.WriteLine("Invalid input, press 'r', 'p' or 's' to play.");
                            break;
                    }
                    break;
                case 1:
                    switch (playerChoice)
                    {
                        case "r":
                            Console.WriteLine("The enemy chose paper. You lost a point!");
                            Console.ForegroundColor = ConsoleColor.Red;

                            --playerScore;
                            break;

                        case "p":
                            Console.WriteLine("The chose paper. It's a tie!");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        case "s":
                            Console.WriteLine("The enemy chose paper. The enemy lost a point!");
                            Console.ForegroundColor = ConsoleColor.Green;

                            --enemyScore;
                            break;

                        default:
                            Console.WriteLine("Invalid input, press 'r', 'p' or 's' to play.");
                            break;
                    }
                    break;

                case 2:
                    switch (playerChoice)
                    {
                        case "r":
                            Console.WriteLine("The enemy chose scissors. The enemy lost a point!");
                            Console.ForegroundColor = ConsoleColor.Green;

                            --enemyScore;
                            break;

                        case "p":
                            Console.WriteLine("The enemy chose scissors. You lost a point!");
                            Console.ForegroundColor = ConsoleColor.Red;

                            --playerScore;
                            break;

                        case "s":
                            Console.WriteLine("The eenmy chose scissors. It's a tie!");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                        default:
                            Console.WriteLine("Invalid input, press 'r', 'p' or 's' to play.");
                            break;
                    }
                    break;
            }
        }
    }

    if (playerScore == 0)
        Console.WriteLine("The enemy won!");

    else if (enemyScore == 0)
        Console.WriteLine("You won!");

    Console.ForegroundColor = ConsoleColor.White;

    Console.WriteLine("\nDo you wish to replay? Press 'y' or 'n'.");

    playerScore = 3;
    enemyScore = 3;
}

bool ReplayOption()
{
    playerChoice = Console.ReadLine();

    if (playerChoice == "y")
        return true;

    else
        return false;
}