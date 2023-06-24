using System;
using System.Collections.Generic;

// Base class for all goals
abstract class Goal
{
    public string Name { get; }
    public int Value { get; }
    public bool IsCompleted { get; protected set; }

    public Goal(string name, int value)
    {
        Name = name;
        Value = value;
        IsCompleted = false;
    }

    public abstract void Complete();
    public abstract string GetStatus();
}

// Simple goal that can be marked complete
class SimpleGoal : Goal
{
    public SimpleGoal(string name, int value) : base(name, value)
    {
    }

    public override void Complete()
    {
        IsCompleted = true;
    }

    public override string GetStatus()
    {
        return IsCompleted ? "[X]" : "[ ]";
    }
}

// Eternal goal that can be recorded multiple times
class EternalGoal : Goal
{
    public int Count { get; private set; }

    public EternalGoal(string name, int value) : base(name, value)
    {
        Count = 0;
    }

    public override void Complete()
    {
        Count++;
    }

    public override string GetStatus()
    {
        return $"Completed {Count} times";
    }
}

// Checklist goal that requires a certain number of completions
class ChecklistGoal : Goal
{
    public int TargetCount { get; }
    public int Bonus { get; }
    public int Count { get; private set; }

    public ChecklistGoal(string name, int value, int targetCount, int bonus) : base(name, value)
    {
        TargetCount = targetCount;
        Bonus = bonus;
        Count = 0;
    }

    public override void Complete()
    {
        Count++;
        if (Count >= TargetCount)
            IsCompleted = true;
    }

    public override string GetStatus()
    {
        return $"Completed {Count}/{TargetCount} times";
    }
}

class Program
{
    static int score = 0;
    static List<Goal> goals = new List<Goal>();

    static void Main(string[] args)
    {
        LoadGoals();

        while (true)
        {
            Console.WriteLine("==== Goal Tracker ====");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Show Score");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            int choice = int.Parse(Console.ReadLine());
            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    CreateGoal();
                    break;
                case 2:
                    RecordEvent();
                    break;
                case 3:
                    ShowGoals();
                    break;
                case 4:
                    ShowScore();
                    break;
                case 5:
                    SaveGoals();
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void CreateGoal()
    {
        Console.WriteLine("==== Create New Goal ====");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");
        Console.Write("Enter goal type: ");

        int type = int.Parse(Console.ReadLine());
        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal value: ");
        int value = int.Parse(Console.ReadLine());

        switch (type)
        {
            case 1:
                goals.Add(new SimpleGoal(name, value));
                break;
            case 2:
                goals.Add(new EternalGoal(name, value));
                break;
            case 3:
                Console.Write("Enter target count: ");
                int targetCount = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus value: ");
                int bonus = int.Parse(Console.ReadLine());
                goals.Add(new ChecklistGoal(name, value, targetCount, bonus));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }

        Console.WriteLine("Goal created successfully!");
    }

    static void RecordEvent()
    {
        Console.WriteLine("==== Record Event ====");
        Console.WriteLine("Select a goal to record:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name}");
        }
        Console.Write("Enter goal number: ");

        int goalNumber = int.Parse(Console.ReadLine());
        if (goalNumber >= 1 && goalNumber <= goals.Count)
        {
            Goal goal = goals[goalNumber - 1];
            goal.Complete();
            score += goal.Value;

            Console.WriteLine($"Event recorded for goal '{goal.Name}'. You earned {goal.Value} points!");
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    static void ShowGoals()
    {
        Console.WriteLine("==== Goals ====");
        for (int i = 0; i < goals.Count; i++)
        {
            Goal goal = goals[i];
            Console.WriteLine($"{i + 1}. {goal.Name} {goal.GetStatus()}");
        }
    }

    static void ShowScore()
    {
        Console.WriteLine($"Current Score: {score} points");
    }

    static void LoadGoals()
    {
        // Load goals from a saved file or database
        // Sample data for demonstration purposes
        goals.Add(new SimpleGoal("Run a Marathon", 1000));
        goals.Add(new EternalGoal("Read Scriptures", 100));
        goals.Add(new ChecklistGoal("Attend Temple", 50, 10, 500));
    }

    static void SaveGoals()
    {
        // Save goals to a file or database for future use
        // Implement save logic here
    }
}
