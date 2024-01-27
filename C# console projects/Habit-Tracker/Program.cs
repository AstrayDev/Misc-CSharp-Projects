namespace Habit_Tracker;

class Program
{
    static void Main(string[] args)
    {
        HabitManager hm = new();
        User user = new();

        user.InputLoop();
    }
}