using System;
using System.Diagnostics;

public static class Helpers
{
    public static bool GetuserInput()
    {
        if (Console.ReadKey().Key == ConsoleKey.Enter)
        {
            return true;
        }

        return false;
    }
}