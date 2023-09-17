using System;

namespace C_;

class Test
{
    public class Account
    {
        private decimal balance;

        public bool WithDrawFunds(decimal amount, Account a)
        {
            if (balance < amount)
                return false;

            balance -= amount;
            return true;
        }

        public decimal AddFunds(decimal amount)
        {
            return balance += amount;
        }

        public decimal PrintBalance()
        {
            return balance;
        }
    };

    static void Main(string[] args)
    {
        Account a = new Account();
        bool shouldExit = false;

        while (!shouldExit)
        {
            Console.Clear();

            Console.WriteLine("Welcome to my trustworthy bank!");
            Console.WriteLine("Select one of the following options to manage your account.\n");
            Console.WriteLine("1: Add Funds");
            Console.WriteLine("2: Withdraw Funds");
            Console.WriteLine("3: Show balance");
            Console.WriteLine("4: To exit\n");

            string? input = Console.ReadLine();
            decimal amount;

            if (input != null)
            {
                switch (input)
                {
                    case "1":
                        Console.WriteLine("How much would you like to add?");

                        input = Console.ReadLine();
                        bool inputAmount = decimal.TryParse(input, out amount);

                        if (inputAmount)
                            a.AddFunds(amount);

                        else
                        {
                            Console.WriteLine("Unknown key entered. Please input a number");
                            Console.ReadLine();
                            break;
                        }

                        Console.WriteLine($"Balance updated, current balance is: {a.PrintBalance():C}. Thank you for your business!");
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.WriteLine("How much would you like to withdraw?");

                        input = Console.ReadLine();
                        inputAmount = decimal.TryParse(input, out amount);
        
                        if (inputAmount)
                        {
                            if (!a.WithDrawFunds(amount, a))
                                Console.WriteLine($"Insufficient funds, current balance is: {a.PrintBalance():C}");

                            else
                                Console.WriteLine($"Balance updated, current balance is: {a.PrintBalance():C}. Thank you for your business!");
                        }

                        else
                        {
                            Console.WriteLine("Unknown key entered. Please input a number");
                            Console.ReadLine();
                            break;
                        }

                        Console.ReadLine();
                        break;

                    case "3":
                        Console.WriteLine($"Your current balance is: {a.PrintBalance():C}");
                        Console.ReadLine();
                        break;

                    case "4":
                        shouldExit = true;
                        break;

                    default:
                        Console.WriteLine("Please enter one of options shown above.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
