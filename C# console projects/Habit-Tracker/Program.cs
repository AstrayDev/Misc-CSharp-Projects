using System;
using Habit_Tracker.Utility;

namespace Habit_Tracker;

class Program
{
    static void Main(string[] args)
    {
        HabitManager hm = new();

        // hm.InsertHabitInfo();
        hm.PrintAllRecords();
        Console.WriteLine(Helpers.GetTableName());
    }
}