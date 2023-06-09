using System;
using System.Threading;

// Base class for activities
abstract class Activity
{
    protected string name;
    protected string description;
    protected int duration;

    public Activity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }
    public void Start()
    {
        Console.WriteLine($"Starting {name} activity...");
        Console.WriteLine(description);
        SetDuration();
        PrepareToBegin();
        PerformActivity();
        ConcludeActivity();
    }

    protected void SetDuration()
    {
        Console.Write("Enter the duration in seconds: ");
        duration = Convert.ToInt32(Console.ReadLine());
    }

    protected void PrepareToBegin()
    {
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);
    }

    protected abstract void PerformActivity();

    protected void ConcludeActivity()
    {
        Console.WriteLine("Great job!");
        Thread.Sleep(2000);
        Console.WriteLine($"You have completed the {name} activity for {duration} seconds.");
        Thread.Sleep(3000);
    }
}

// Breathing Activity
class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("Let's begin the breathing exercise...");
        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(1000);
            Console.WriteLine("Breathe out...");
            Thread.Sleep(1000);
        }
    }
}

// Reflection Activity
class ReflectionActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience. This will help you recognize the power you have and how you can use it in other aspects of your life.")
    {
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("Let's begin the reflection activity...");
        Random random = new Random();
        for (int i = 0; i < duration; i++)
        {
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine(prompt);
            Thread.Sleep(3000);

            foreach (string question in questions)
            {
                Console.WriteLine(question);
                Thread.Sleep(3000);
            }
        }
    }
}

// Listing Activity
class ListingActivity : Activity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void PerformActivity()
    {
        Console.WriteLine("Let's begin the listing activity...");
        Random random = new Random();
        string prompt = prompts[random.Next(prompts.Length)];
        Console.WriteLine(prompt);
        Thread.Sleep(5000);

        Console.WriteLine("Start listing items:");
        int itemCount = 0;
        while (duration > 0)
        {
            string item = Console.ReadLine();
            if (!string.IsNullOrEmpty(item))
                itemCount++;
            duration -= 5;
        }

        Console.WriteLine($"You listed {itemCount} items.");
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Activity activity;

            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity();
                    activity.Start();
                    break;
                case 2:
                    activity = new ReflectionActivity();
                    activity.Start();
                    break;
                case 3:
                    activity = new ListingActivity();
                    activity.Start();
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }
}
